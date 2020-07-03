using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    public static readonly string SONG_FOLDER = SONG_FOLDER = Application.dataPath + "/Songs/";

    public static void Init()
    {
        //check existence of save folder
        if (!Directory.Exists(SAVE_FOLDER))
        {
            //create save folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    public static void SaveGame(string saveString)
    {
        Init();
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

    public static string LoadGame()
    {
        if (File.Exists(SAVE_FOLDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
            return saveString;
        }
        else return null;
    }

    public static string LoadSongList()
    {
        if (!Directory.Exists(SONG_FOLDER))
        {
            //create save folder
            Directory.CreateDirectory(SONG_FOLDER);
        }
        string songJson = "";
        if (File.Exists(SONG_FOLDER + "/List.json"))
        {
            songJson = File.ReadAllText(SONG_FOLDER + "List.json");
        }
        return songJson;
    }

    public static IEnumerator LoadSong(string fileName, AudioSource audioScource)
    {
        UnityWebRequest file = UnityWebRequestMultimedia.GetAudioClip(SONG_FOLDER + fileName, AudioType.WAV);
        yield return file.SendWebRequest();
        audioScource.clip = DownloadHandlerAudioClip.GetContent(file);
    }
}