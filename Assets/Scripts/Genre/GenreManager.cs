using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenreManager : MonoBehaviour
{
    DefaultControl inputAction;
    string[] genres;
    int idx;
    public TextMeshPro selectedGenre;
    bool active = false;

    void Awake()
    {
        inputAction = new DefaultControl();
        inputAction.Gameplay.rightInner.performed += ctx => Select();
        inputAction.Gameplay.rightOuter.performed += ctx => GoRight();
        inputAction.Gameplay.leftInner.performed += ctx => Back();
        inputAction.Gameplay.leftOuter.performed += ctx => GoLeft();
    }

    // Update is called once per frame
    void Update()
    {
        if (MainValue.Instance.crrScene == "GenreList" && MainValue.Instance.sceneToLoad == "")
        {
            if(!active) Activate();
        }
        //else
        //{
        //    InActivate();
        //}
    }

    void Select()
    {
        InActivate();
        //add this scene to stack
        MainValue.Instance.previousScene.Push(MainValue.Instance.crrScene);
        MainValue.Instance.genresFilter = genres[idx];
        MainValue.Instance.canDestroy = true;
        MainValue.Instance.sceneToLoad = "SongList";
    }

    void Back()
    {
        InActivate();
        MainValue.Instance.canDestroy = true;
        MainValue.Instance.sceneToLoad = MainValue.Instance.previousScene.Pop();
    }

    void GoRight()
    {
        if (++idx >= genres.Length) idx = 0;
        selectedGenre.text = genres[idx];
    }

    void GoLeft()
    {
        if (--idx < 0) idx = genres.Length-1;
        selectedGenre.text = genres[idx];
    }

    void Activate()
    {
        Utility.RequestBadge("Select genre");
        genres = MainValue.Instance.genres;
        //set first showning genre
        selectedGenre.text = genres[idx];
        Enable();
        //listen to controller event
        Drum.rightInner += Select;
        Drum.rightOuter += GoRight;
        Drum.leftOuter += GoLeft;
        Drum.leftInner += Back;
        active = true;

    }
    void InActivate()
    {
        Disable();
        Utility.HideBadge();
        Drum.rightInner -= Select;
        Drum.rightOuter -= GoRight;
        Drum.leftOuter -= GoLeft;
        Drum.leftInner -= Back;
        active = false;
    }

    private void Enable()
    {
        inputAction.Enable();
    }

    private void Disable()
    {
        inputAction.Disable();
    }
}
