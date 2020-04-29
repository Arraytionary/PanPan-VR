using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public ActionProcesser ap;
    public string side;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Stick")
        {
            switch (side)
            {
                case "right":
                    ap.HitRI();
                    break;
                case "left":
                    ap.HitLI();
                    break;
                case "oleft":
                    ap.HitLO();
                    break;
                case "oright":
                    ap.HitRO();
                    break;
                default:
                    break;
            }
        }
    }
}
