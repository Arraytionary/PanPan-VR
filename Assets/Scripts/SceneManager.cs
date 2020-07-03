using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startScene;
    public GameObject[] scenes;
    public Animator transition;
    GameObject crrScene;
    bool ready;

    public Camera mainCamera;
    private void Start()
    {
        //instantiate start scene
        insertCoin.inserted += AfterInserted;
        AfterInserted();

    }
    void AfterInserted()
    {
        Instantiate(startScene, transform.position, Quaternion.identity);
        //Load save file if exists
        //Utility.Load();
        //insertCoin.inserted -= AfterInserted;
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MainValue.Instance.sceneToLoad.Equals("") && ready)
        {
            transition.SetTrigger("fadeIn");
            while (!MainValue.Instance.canDestroy)
            {
                //spin wait
            }
            ready = false;
        }
    }

    //public void LoadScene()
    //{
    //    Destroy(crrScene);
    //    Debug.Log(MainValue.Instance.sceneToLoad);
    //    Debug.Log(MainValue.Instance.SceneIndex);
    //    MainValue.Instance.crrScene = MainValue.Instance.sceneToLoad;
    //    crrScene = Instantiate(scenes[MainValue.Instance.SceneIndex[MainValue.Instance.sceneToLoad]], transform.position, Quaternion.identity);
    //    MainValue.Instance.sceneToLoad = "";
    //    ready = true;
    //}
    public void LoadScene()
    {
        switch (MainValue.Instance.sceneToLoad)
        {
            case "MainGame":
                Debug.Log("main");
                MainValue.Instance.gameHasEnded = false;
                Instantiate(scenes[MainValue.Instance.SceneIndex[MainValue.Instance.sceneToLoad]], transform.TransformPoint(new Vector3(0, 10, 0)), Quaternion.identity);
                break;
            default:
                break;
        }
        Debug.Log(mainCamera.transform.position);
        mainCamera.transform.localPosition = MainValue.Instance.CameraPostion[MainValue.Instance.sceneToLoad];
        //MainValue.Instance.previousScene.Push(MainValue.Instance.crrScene);
        MainValue.Instance.crrScene = MainValue.Instance.sceneToLoad;
        MainValue.Instance.sceneToLoad = "";
        Debug.Log(MainValue.Instance);
        ready = true;
    }
}
