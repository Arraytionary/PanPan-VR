using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        aS = gameObject.GetComponent<AudioSource>();
    }

    void PlaySound()
    {
        aS.Play();
    }
}
