using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{   
    public ActionProcesser aP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        aP.OnTriggerEnter2D(collision);
        //Destroy(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        aP.OnTriggerExit2D(collision);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    aP.OnCollisionEnter2D(collision);
    //}
}
