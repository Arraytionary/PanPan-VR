using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeSound : MonoBehaviour
{
    AudioSource aS;
    public AudioClip[] clips;
    float wait;
    float lastPlay;
    // Start is called before the first frame update
    void Start()
    {
        aS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MainValue.Instance.crrScene == "MainGame")
        {
            aS.Stop();
        }
        else
        {
            if (Time.time - lastPlay > wait)
            {
                Debug.Log(wait);
                wait = PlaySfx();
                lastPlay = Time.time;
            }
        }
    }

    public float PlaySfx()
    {
        aS.Stop();
        aS.PlayOneShot(clips[(int)Random.Range(0, clips.Length)]);
        return Random.Range(3f, 10f);
    }
}
