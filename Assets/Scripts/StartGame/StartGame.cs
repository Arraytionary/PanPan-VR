using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    //Input action
    DefaultControl inputAction;

    void Awake()
    {
        inputAction = new DefaultControl();

        Drum.rightInner += EnterTheGame;
        Drum.rightOuter += EnterTheGame;
        Drum.leftInner += EnterTheGame;
        Drum.leftOuter += EnterTheGame;

        inputAction.Gameplay.rightInner.performed += ctx => EnterTheGame();
        inputAction.Gameplay.rightOuter.performed += ctx => EnterTheGame();
        inputAction.Gameplay.leftInner.performed += ctx => EnterTheGame();
        inputAction.Gameplay.leftOuter.performed += ctx => EnterTheGame();
    }

    public void EnterTheGame()
    {
        Debug.Log("press start");

        MainValue.Instance.canDestroy = true;
        MainValue.Instance.sceneToLoad = "MainMenu";
        Drum.rightInner -= EnterTheGame;
        Drum.rightOuter -= EnterTheGame;
        Drum.leftInner -= EnterTheGame;
        Drum.leftOuter -= EnterTheGame;

        OnDisable();
        //inputAction.Gameplay.rightInner.performed -= ctx => EnterTheGame();
        //inputAction.Gameplay.rightOuter.performed -= ctx => EnterTheGame();
        //inputAction.Gameplay.leftInner.performed -= ctx => EnterTheGame();
        //inputAction.Gameplay.leftOuter.performed -= ctx => EnterTheGame();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }
}
