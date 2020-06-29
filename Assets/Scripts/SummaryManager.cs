using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class SummaryManager : MonoBehaviour
{
    DefaultControl inputAction;

    public TextMeshPro score;
    public TextMeshPro good;
    public TextMeshPro ok;
    public TextMeshPro bad;
    public TextMeshPro maxCombo;
    public TextMeshPro songName;
    public GameObject cleared;
    public GameObject failed;
    public GameObject highScore;

    bool active = false;

    void Awake()
    {
        inputAction = new DefaultControl();
        inputAction.Gameplay.rightInner.performed += ctx => Next();
        inputAction.Gameplay.rightOuter.performed += ctx => Next();
        inputAction.Gameplay.leftInner.performed += ctx => Next();
        inputAction.Gameplay.leftOuter.performed += ctx => Next();
    }

    // Update is called once per frame
    void Update()
    {
        if (MainValue.Instance.crrScene == "ScoreSummary")
        {
            //make it run only once
            if(!active) Activate();
        }
        else
        {
            InActivate();
        }
    }

    void Next()
    {
        Utility.HideBadge();
        MainValue.Instance.canDestroy = true;
        MainValue.Instance.fromBack = true;
        MainValue.Instance.sceneToLoad = "SongList";
    }

    void Activate()
    {
        active = true;
        //check high score
        bool isHighScore = true;

        Utility.RequestBadge("Results");
        songName.text = MainValue.Instance.mainSong.songName.ToString();
        score.text = MainValue.Instance.score.ToString();
        good.text = MainValue.Instance.good.ToString();
        ok.text = MainValue.Instance.ok.ToString();
        bad.text = MainValue.Instance.bad.ToString();
        maxCombo.text = MainValue.Instance.maxCombo.ToString();

        cleared.SetActive(MainValue.Instance.cleared);
        failed.SetActive(!MainValue.Instance.cleared);

        //save data
        SaveObject toSave = new SaveObject(MainValue.Instance.score, MainValue.Instance.cleared);
        SaveObject previousSave = new SaveObject(0, false);
        if (MainValue.Instance.saveObjects.TryGetValue(MainValue.Instance.mainSong.songName.ToString(), out previousSave))
        {
            isHighScore = previousSave.highScore < MainValue.Instance.score;
            if (isHighScore) toSave.highScore = MainValue.Instance.score;
            toSave.cleared = previousSave.cleared || MainValue.Instance.cleared;
        }
        else
        {
            toSave.highScore = MainValue.Instance.score;
            toSave.cleared = MainValue.Instance.cleared;
        }
        MainValue.Instance.saveObjects[songName.text] = toSave;
        Utility.Save();


        //TODO High score
        Debug.Log(isHighScore);
        highScore.SetActive(isHighScore);

        Enable();
        //listen to controller event
        Drum.rightInner += Next;
        Drum.rightOuter += Next;
        Drum.leftOuter += Next;
        Drum.leftInner += Next;

    }
    void InActivate()
    {
        active = false;
        Drum.rightInner -= Next;
        Drum.rightOuter -= Next;
        Drum.leftOuter -= Next;
        Drum.leftInner -= Next;
        Disable();
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
