using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;

public class SongListManager : MonoBehaviour
{
    DefaultControl inputAction;

    List<Song> allSongList;
    List<Song> songList;

    public SongHolder[] songs;
    Vector3[] positions;
    Vector3[] oldPos;
    float lastPress;
    int rightEdge;
    int leftEdge;
    public float speed;

    public int center;

    int firstSong;
    int lastSong;
    int songsCount;
    bool isLock;

    public float songPosition;
    public float dspSongTime;

    AudioSource aS;

    bool holding;
    int called;

    public GameObject centerExpanded;
    Animator centerAnimator;
    Expanded expanded;

    bool active = false;
    void Awake()
    {
        Debug.Log("Awake");

        //Drum.rightInner += Select;
        //Drum.rightOuter += GoLeft;
        //Drum.leftOuter += GoRight;

        inputAction = new DefaultControl();
        inputAction.Gameplay.rightInner.performed += ctx => Select();
        inputAction.Gameplay.rightOuter.performed += ctx => GoLeft();
        inputAction.Gameplay.leftInner.performed += ctx => Back();
        inputAction.Gameplay.leftOuter.performed += ctx => GoRight();
        //inputAction.Gameplay.DoubleInner.performed += ctx => HitDI();
    }

    void Start()
    {
        //Load save file if exists
        Utility.Load();

        aS = GetComponent<AudioSource>();
        centerAnimator = centerExpanded.GetComponent<Animator>();
        expanded = centerExpanded.GetComponent<Expanded>();

        //set up default position for each song label
        positions = new Vector3[songs.Length];
        oldPos = new Vector3[songs.Length];
        for (int i = 0; i < songs.Length; i++)
        {
            positions[i] = songs[i].transform.position;
            oldPos[i] = positions[i];
        }
        leftEdge = 0;
        rightEdge = songs.Length - 1;

        //Load up song data
        string json = Resources.Load<TextAsset>("List").ToString();
        allSongList = JsonConvert.DeserializeObject<List<Song>>(json);

        //Load up score data
        SaveObject saveObject = new SaveObject(0, false);
        foreach(Song s in allSongList)
        {
            if (MainValue.Instance.saveObjects.TryGetValue(s.songName, out saveObject))
            {
                s.cleared = saveObject.cleared;
                s.highScore = saveObject.highScore;
            }
        }

        //assign genre list to main values
        allSongList.Sort((x, y) => string.Compare(x.genre, y.genre));
        MainValue.Instance.genres = new string[allSongList.Count];
        for ( int i = 0;  i < allSongList.Count; i++)
        {
            MainValue.Instance.genres[i] = allSongList[i].genre;
        }
        //get rid of duplicate genre
        MainValue.Instance.genres = MainValue.Instance.genres.Distinct().ToArray();

        //sort songs by song name
        allSongList.Sort((x, y) => string.Compare(x.songName, y.songName));
        //songList.Sort((x, y) => string.Compare(x.songName, y.songName));
        //firstSong = songList.Count - 1;
        //lastSong = songs.Length/2;
        //songsCount = songList.Count;

        //preload each song
        foreach (Song s in allSongList)
        {
            Resources.LoadAsync<AudioClip>(s.fileName);
        }
        //for (int i=center; i < songs.Length; i++)
        //{
        //    songs[i].song = songList[i-center];
        //}
        //for (int i = 0; i < center; i++)
        //{
        //    songs[i].song = songList[songsCount - center + i];
        //}

    }
    void CalculateSongsValue()
    {
        firstSong = songList.Count - 1;
        lastSong = songs.Length / 2;
        songsCount = songList.Count;
    }

    void GenerateSonglist()
    {
        if (!MainValue.Instance.fromBack)
        {
            songList = allSongList;
            if (MainValue.Instance.genresFilter == "")
            {
                FilledList(songList);
                CalculateSongsValue();
                for (int i = center; i < songs.Length; i++)
                {
                    songs[i].song = songList[i - center];
                }
                for (int i = 0; i < center; i++)
                {
                    songs[i].song = songList[songsCount - center + i];
                }
            }
            else
            {
                songList = songList.Where(_ => _.genre == MainValue.Instance.genresFilter).ToList();
                FilledList(songList);
                CalculateSongsValue();
                for (int i = center; i < songs.Length; i++)
                {
                    songs[i].song = songList[i - center];
                }
                for (int i = 0; i < center; i++)
                {
                    songs[i].song = songList[songsCount - center + i];
                }
            }
        }
        MainValue.Instance.fromBack = false;
    }

    void FilledList(List<Song> l)
    {
        int idx = 0;
        while(l.Count < 9)
        {
            Debug.Log(l[idx].songName);
            if (idx >= l.Count) idx = 0;
            l.Add(l[idx++]);
        }
    }

    void Activate()
    {
        Debug.Log(MainValue.Instance.fromBack);
        //filter songlist
        GenerateSonglist();

        Enable();
        string badge = MainValue.Instance.genresFilter == "" ? "Select song" : MainValue.Instance.genresFilter;
        Utility.RequestBadge(badge);
        //listen to controller event
        Drum.rightInner += Select;
        Drum.rightOuter += GoLeft;
        Drum.leftOuter += GoRight;
        Drum.leftInner += Back;
        active = true;

        //play first song
        StartCoroutine(PlaySample(songs[center].song.fileName, songs[center].song.startAt));
        //Set initial last press
        expanded.song = songs[center].song;
        lastPress = Time.time;
    }

    void InActivate()
    {
        Utility.HideBadge();

        //stop listening to controller event
        Drum.rightInner -= Select;
        Drum.rightOuter -= GoLeft;
        Drum.leftOuter -= GoRight;
        Drum.leftInner -= Back;

        Disable();
        aS.Stop();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainValue.Instance.crrScene == "SongList" && MainValue.Instance.sceneToLoad == "")
        {
            if (!active)
            {
                Activate();
            }
            songPosition = (float)(AudioSettings.dspTime - dspSongTime);
            if (songPosition < 3f)
            {
                aS.volume += 0.003f;
                //aS.volume += Mathf.Lerp(0, 1, songPosition / 3);
                //Debug.Log(Mathf.Lerp(0, 1, songPosition / 3));
            }
            if (songPosition > 8f)
            {
                aS.volume -= 0.003f;
            }
            float now = Time.time;
            int c = 0;
            for (int i = 0; i < songs.Length; i++)
            {
                if (songs[i].transform.position != positions[i])
                {
                    songs[i].transform.position = Vector3.Lerp(songs[i].transform.position, positions[i], Mathf.Min((now - lastPress) * speed, 1f));
                    c++;
                }
            }
            isLock = c != 0;

            //adjust time before enter expanding mode

            if (now - lastPress > 0.45f && !holding)
            {
                Expand();
                //Do label expand 
            }
            centerAnimator.SetBool("holding", holding);
        }
    }

    public void Expand()
    {
        holding = true;
        float centerPos = songs[center].transform.position.x;
        for (int i = 0; i < songs.Length; i++)
        {
            oldPos[i] = positions[i];
            if (i != center && i != leftEdge && i != rightEdge)
            {
                
                if (songs[i].transform.position.x < centerPos)
                {
                    positions[i].x -= 4f;
                }
                else
                {
                    positions[i].x += 4f;
                }
            }
            //Debug.Log(string.Format("pos {0} i {1}", positions[i], i));
            //Debug.Log(positions[i]);
        }
        expanded.song = songs[center].song;
        //expanded.Expand();
        //Debug.Log(called++);
    }

    public void Repositioning()
    {
        if (holding)
        {
            for (int i = 0; i < songs.Length; i++)
            {
                songs[i].transform.position = oldPos[i];
            }
            holding = false;
        }
    }

    public void GoLeft()
    {
        if (!isLock)
        {
            Repositioning();
            center = center + 1 < songs.Length ? center + 1 : 0;
            //aS.Stop();
            //dspSongTime = (float)AudioSettings.dspTime;
            //aS.PlayOneShot(Resources.Load<AudioClip>(songs[center].song.fileName));
            //aS.volume = 0;
            StopAllCoroutines();
            StartCoroutine(PlaySample(songs[center].song.fileName, songs[center].song.startAt));
            Vector3 pos = songs[rightEdge].transform.position;

            for (int i = 0; i < songs.Length; i++)
            {
                if (i != leftEdge)
                {
                    oldPos[i] = positions[i];
                    positions[i] = songs[i - 1 >= 0 ? i - 1 : songs.Length - 1].transform.position;
                    if (positions[i] == positions[leftEdge])
                    {
                        songs[leftEdge].transform.position = pos;
                        songs[leftEdge].song = songList[(lastSong + 1) % songsCount];
                        lastSong++;
                        positions[leftEdge] = pos;
                        oldPos[leftEdge] = pos;
                        rightEdge = leftEdge;
                        leftEdge = i;
                    }
                }
            }

            lastPress = Time.time;
        }
    }

    public void Select()
    {
        InActivate();
        //assign song;
        MainValue.Instance.mainClip = aS.clip;
        MainValue.Instance.mainSong = songs[center].song;
        MainValue.Instance.canDestroy = true;
        MainValue.Instance.sceneToLoad = "MainGame";
    }

    public void Back()
    {
        InActivate();
        MainValue.Instance.canDestroy = true;
        MainValue.Instance.sceneToLoad = MainValue.Instance.previousScene.Pop();
    }

    public void GoRight()
    {
        Debug.Log("right");
        if (!isLock)
        {
            Repositioning();
            center = center - 1 >= 0 ? center - 1 : songs.Length - 1;
            StopAllCoroutines();
            StartCoroutine(PlaySample(songs[center].song.fileName, songs[center].song.startAt));
            Vector3 pos = songs[leftEdge].transform.position;

            for (int i = 0; i < songs.Length; i++)
            {
                if (i != rightEdge)
                {
                    oldPos[i] = positions[i];
                    positions[i] = songs[i + 1 <= songs.Length - 1 ? i + 1 : 0].transform.position;
                    if (positions[i] == positions[rightEdge])
                    {
                        songs[rightEdge].transform.position = pos;
                        firstSong = firstSong - 1 >= 0 ? firstSong - 1 : songsCount - 1;
                        songs[rightEdge].song = songList[firstSong];
                        positions[rightEdge] = pos;
                        oldPos[rightEdge] = pos;
                        leftEdge = rightEdge;
                        rightEdge = i;
                    }
                }
            }
            lastPress = Time.time;
        }
    }

    IEnumerator PlaySample(string song, double startAt)
    {
        yield return new WaitForSeconds(0.1f);
        MainValue.Instance.canDestroy = false;
        //stop what is currently playing
        //aS.Stop();
        dspSongTime = (float)AudioSettings.dspTime;
        
        //load audio
        aS.clip = Resources.Load<AudioClip>(song);
        //play audio at given position
        aS.PlayScheduled(startAt);
        //aS.PlayOneShot(Resources.Load<AudioClip>(song));
        aS.time = (float)startAt;
        aS.volume = 0;
    }

    private void Enable()
    {
        Debug.Log("enabled");
        inputAction.Enable();
    }

    private void Disable()
    {
        Debug.Log("disabled");
        inputAction.Disable();
    }
}