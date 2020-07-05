using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public float cdTime;
    public float time;
    public HandPresence hP;

    public FlameController flameController;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (MainValue.Instance.combo > 20)
        {
            flameController.SetLevel(1);
        }

        else if (MainValue.Instance.combo > 10)
        {
            flameController.SetLevel(0);
        }

        else if (MainValue.Instance.combo < 10)
        {
            flameController.SetLevel(-1);
        }

        if (Time.time - time <= cdTime)
        {
            gameObject.tag = "non_stick";
        }
        else
        {
            gameObject.tag = "Stick";
        }
    }

    public void Hitted(float sensitivity, float duration)
    {
        if (hP != null)
        {
            hP.RequestHaptic(0, sensitivity, duration);
        }
        time = Time.time;
    }
}
