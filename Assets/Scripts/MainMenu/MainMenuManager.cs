using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public MenuItem[] items;
    int index;
    MenuItem selected;
    DefaultControl inputAction;
    // Start is called before the first frame update
    void Awake()
    {
            inputAction = new DefaultControl();
            inputAction.Gameplay.rightInner.performed += ctx => Select();
            inputAction.Gameplay.rightOuter.performed += ctx => GoRight();
            //inputAction.Gameplay.leftInner.performed += ctx => Next();
            inputAction.Gameplay.leftOuter.performed += ctx => GoLeft();

            selected = items[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (MainValue.Instance.crrScene == "MainMenu" && MainValue.Instance.sceneToLoad == "")
        {
            Activate();
        }
        //else
        //{
        //    InActivate();
        //}
    }

    public void GoRight()
    {
        index++;
        if (index >= items.Length) index = 0;
        selected = items[index];
        if(selected != null) SetSelectedMenu(selected.holdedName);
    }

    public void GoLeft()
    {
        index--;
        if (index < 0) index = items.Length - 1;
        selected = items[index];
        if(selected != null) SetSelectedMenu(selected.holdedName);

    }

    void SetSelectedMenu(string select)
    {
        MainValue.Instance.selectedMenu = select;
    }

    void Select()
    {
        InActivate();
        if (selected != null) selected.Perform();
    }

    void Activate()
    {
        Utility.RequestBadge("Main menu");

        Enable();
        //listen to controller event
        Drum.rightInner += Select;
        Drum.rightOuter += GoRight;
        Drum.leftOuter += GoLeft;
        //Drum.leftInner += ;

    }
    void InActivate()
    {
        Disable();
        Drum.rightInner -= Select;
        Drum.rightOuter -= GoRight;
        Drum.leftOuter -= GoLeft;
        //Drum.leftInner -= ;
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
