using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

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
}