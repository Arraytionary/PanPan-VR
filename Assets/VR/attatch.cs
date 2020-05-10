using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attatch : MonoBehaviour
{
    public GameObject o;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        o.transform.rotation = Quaternion.Euler(o.transform.rotation.x, Quaternion.identity.y, o.transform.rotation.z);
    }
}
