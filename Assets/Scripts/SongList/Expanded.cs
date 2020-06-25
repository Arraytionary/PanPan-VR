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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (song != null)
        {
            songName.text = song.songName;
            genre.text = "Genre: " + song.genre;
            artist.text = "Artist: " + song.artist;
            duration.text = "Duration: " + song.duration;
        }

        //TODO make high score look up
    }
}
