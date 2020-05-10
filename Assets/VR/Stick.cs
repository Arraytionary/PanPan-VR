using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public float cdTime;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - time <= cdTime)
        {
            gameObject.tag = "non_stick";
        }
        else
        {
            gameObject.tag = "Stick";
        }
    }

    public void CoolDown(float _time)
    {
        time = _time;
    }
}
