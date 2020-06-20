using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class buster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Detatch()
    {
        gameObject.transform.parent = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if(other.gameObject.tag == "Hand")
        {
            other.gameObject.GetComponentInChildren<HandPresence>().jx("buster");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        //collision.gameObject.GetComponentInChildren<HandPresence>().RequestHaptic(0, 1, 0.2f);
    }
}
