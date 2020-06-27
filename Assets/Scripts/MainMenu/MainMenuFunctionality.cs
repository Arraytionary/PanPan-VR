using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFunctionality: MonoBehaviour
{
    public void AllSongs()
    {
        Utility.HideBadge();
        MainValue.Instance.previousScene.Push(MainValue.Instance.crrScene);
        MainValue.Instance.canDestroy = true;
        //show song list without filter
        MainValue.Instance.genresFilter = "";
        MainValue.Instance.sceneToLoad = "SongList";
    }

    public void AllGenre()
    {
        Utility.HideBadge();
        MainValue.Instance.previousScene.Push(MainValue.Instance.crrScene);
        MainValue.Instance.canDestroy = true;
        MainValue.Instance.sceneToLoad = "GenreList";
    }

    public void HowToPlay()
    {
        Utility.HideBadge();
        MainValue.Instance.previousScene.Push(MainValue.Instance.crrScene);
        MainValue.Instance.canDestroy = true;
        MainValue.Instance.sceneToLoad = "HowToPlay";
    }
}
