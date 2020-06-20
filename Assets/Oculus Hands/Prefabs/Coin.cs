using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    AudioSource aS;
    public AudioClip flip;
    // Start is called before the first frame update
    void Start()
    {
        aS = gameObject.GetComponent<AudioSource>();
        aS.PlayOneShot(flip);
    }

}
