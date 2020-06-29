using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Expanded : MonoBehaviour
{
    public Song song;

    public TextMeshPro songName;
    public TextMeshPro genre;
    public TextMeshPro artist;
    public TextMeshPro duration;
    public TextMeshPro highScore;
    public GameObject cleared;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (song != null)
        {
            string highScoreValue = "-";
            bool clearedValue = false;
            songName.text = song.songName;
            genre.text = "Genre: " + song.genre;
            artist.text = "Artist: " + song.artist;
            duration.text = "Duration: " + (int)song.duration / 60 + ":" + song.duration % 60;
            //load data
            SaveObject save = new SaveObject(0, false);
            if (MainValue.Instance.saveObjects.TryGetValue(song.songName, out save))
            {
                Debug.Log(save.highScore);
                highScoreValue = save.highScore.ToString();
                clearedValue = save.cleared;
            }
            highScore.text = "High score: " + highScoreValue;
            cleared.SetActive(clearedValue);

        }

        //TODO make high score look up
    }
}
