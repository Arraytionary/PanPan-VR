using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGround : MonoBehaviour
{
    public GameObject obj;
    void Start()
    {
        //Time.timeScale = 0.5f;
        //obj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -400);
        //obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -400));
        InvokeRepeating("Shoot", 2f, 3f);
    }

    void Shoot()
    {
        GameObject x;
        x = Instantiate(obj, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
