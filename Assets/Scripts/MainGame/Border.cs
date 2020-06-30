using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    // Start is called before the first frame update
    public ActionProcesser ap;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.gameObject);
        if(collision.gameObject.layer == 9)
        {
            ap.ReportMiss(collision.gameObject);
        }
        Destroy(collision.gameObject, 4f);
    }
}
