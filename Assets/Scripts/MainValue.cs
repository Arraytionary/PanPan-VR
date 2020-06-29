using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainValue : Singleton<MainValue>
{
    public IDictionary<string, Vector3> CameraPostion = new Dictionary<string, Vector3>()
    {
        { "StartScene",  new Vector3(0,0,-10)},
        { "MainMenu", new Vector3(-24, 0, -10)},
        {"GenreList", new Vector3(-48, 0, -10)},
        { "SongList",  new Vector3(24,0,-10)},
        {"MainGame",  new Vector3(0, 10, -10)},
        {"ScoreSummary",  new Vector3(48,0,-10)},
    };
    public IDictionary<string, float> FloatValue = new Dictionary<string, float>()
    {
        { "left", 0f},
        {"right", 0f }
    };
    public IDictionary<string, Object> SongListValue = new Dictionary<string, Object>()
    {
    };
    public IDictionary<string, int> SceneIndex = new Dictionary<string, int>()
    {
        { "StartScene", 0},
        { "SongList", 1},
        {"MainGame", 2},
        {"ScoreSummary", 3}
    };
    public string[] genres;

    public IDictionary<string, SaveObject> saveObjects;

    public string selectedMenu = "all song";
    public string crrScene = "StartScene";
    public Stack<string> previousScene = new Stack<string>();
    //check does songlist is called by back()
    public bool fromBack = false;
    public string genresFilter = "";
    public AudioClip mainClip;
    public bool canDestroy;
    public string sceneToLoad = "";
    public float crrSecPerBeat = 0f;

    public int combo;
    public int good;
    public int ok;
    public int bad;
    public int score;
    public int maxCombo;
    public bool cleared;
    public Song mainSong;

    public bool gameHasEnded = false;

    public string toDisplayOnBadge;
    public bool requestedBadge = false;
}
