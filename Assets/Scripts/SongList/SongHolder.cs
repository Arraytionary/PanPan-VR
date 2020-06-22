using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongHolder : MonoBehaviour
{
    public Song song;

    private void Update()
    {
        if (song != null)
        {
            string toDisplay = song.songName.Replace(" ", "\n\n");
            if(toDisplay.Length > 9) toDisplay = string.Concat(toDisplay.Substring(0, 9), ".");
            GetComponentInChildren<TextMeshPro>().text = toDisplay;
        }

    }
}
