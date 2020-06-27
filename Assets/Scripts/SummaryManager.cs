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
            Activate();
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
        MainValue.Instance.sceneToLoad = "SongList";
    }

    void Activate()
    {
        Utility.RequestBadge("Results");
        songName.text = MainValue.Instance.mainSong.songName.ToString();
        score.text = MainValue.Instance.score.ToString();
        good.text = MainValue.Instance.good.ToString();
        ok.text = MainValue.Instance.ok.ToString();
        bad.text = MainValue.Instance.bad.ToString();
        maxCombo.text = MainValue.Instance.maxCombo.ToString();

        Enable();
        //listen to controller event
        Drum.rightInner += Next;
        Drum.rightOuter += Next;
        Drum.leftOuter += Next;
        Drum.leftInner += Next;

    }
    void InActivate()
    {
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
