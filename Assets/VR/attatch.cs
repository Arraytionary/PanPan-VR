using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach : MonoBehaviour
{
    public GameObject o;
    public Stick stick;
    HandPresence hP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //o.transform.rotation = Quaternion.Euler(o.transform.rotation.x, Quaternion.identity.y, o.transform.rotation.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hand")
        {
            hP = other.gameObject.GetComponentInChildren<HandPresence>();
        }
    }

    public void AssignToHand()
    {
        stick.hP = hP;
    }

    public void Drop()
    {
        stick.hP = null;
    }

}
