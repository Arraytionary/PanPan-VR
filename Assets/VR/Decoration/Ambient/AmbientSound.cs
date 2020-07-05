using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    AudioSource aS;
    // Update is called once per frame
    private void Start()
    {
        aS = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if(MainValue.Instance.crrScene == "MainGame")
        {
            aS.volume -= 0.02f;
        }
        else
        {
            aS.volume = Mathf.Min(aS.volume + 0.003f, 0.716f);
        }
    }
}
