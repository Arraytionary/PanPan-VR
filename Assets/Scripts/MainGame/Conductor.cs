﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    Song song;

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    public float startOffset;

    public float offset;

    float lastPos;

    float lp2;

    public float db;

    public Spawner spawner;

    public GameObject register;

    bool spawned;

    //int[,] note;
    int[] note;

    int idx;

    int id2;

    //
    float noteDiff;// = 0.5f;
    //

    //distance btween spawner and register 
    public float distance;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        Utility.RequestBadge(MainValue.Instance.mainSong.songName);

        //Load selected song
        song = MainValue.Instance.mainSong;

        //Load selected song's bpm
        songBpm = song.bpm;

        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;
        MainValue.Instance.crrSecPerBeat = secPerBeat;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //load music clip from MainValue
        musicSource.clip = MainValue.Instance.mainClip;

        //Start the music
        musicSource.Play();

        lastPos = -offset;

        lp2 = -2*offset;

        spawned = false;

        //note = new int[,] { { 0,0}, { 1,0}, { 1,0}, { 1,2}, { 1,0} };
        note = MainValue.Instance.mainSong.note;
        startOffset = song.startOffset;

        noteDiff = 0.5f;

        distance = Mathf.Abs(register.transform.position.x - spawner.gameObject.transform.position.x);
        Invoke("ShowSummary", song.duration);
        t = Time.time;
    }


    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - startOffset);

        //determine how many beats since the song started
        //songPositionInBeats = (songPosition / secPerBeat) - offset;
        songPositionInBeats = (songPosition / secPerBeat);
        //if ((int)songPositionInBeats % 4 == 0 && (int)songPositionInBeats - lp2 > 1)
        //{
        //    spawner.SpawnNorm();
        //    lp2 = songPositionInBeats;
        //    //spawner.SpawnNote();
        //}

        if (songPositionInBeats >= -offset)
        {
            if (!spawned)
            {
                spawner.SpawnNote(note);
                spawned = true;
            }

            if (songPositionInBeats > lastPos + noteDiff)
            {
                //Debug.Log(songPositionInBeats - lastPos);
                if (idx%8 == 0) spawner.SpawnNorm();
                idx++;
                lastPos = songPositionInBeats;
                
            }

        }
    }
    public float GetVelocity()
    {
        Debug.Log(distance);
        Debug.Log(offset);
        Debug.Log(secPerBeat);

        return distance/(offset*secPerBeat);
    }

    void ShowSummary()
    {
        MainValue.Instance.gameHasEnded = true;
        musicSource.Stop();
        Utility.HideBadge();
        MainValue.Instance.canDestroy = true;
        MainValue.Instance.sceneToLoad = "ScoreSummary";
    }
}
