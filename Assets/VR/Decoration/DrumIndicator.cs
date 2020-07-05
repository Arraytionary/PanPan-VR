using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumIndicator : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MainValue.Instance.crrScene != "" && MainValue.Instance.crrScene != "MainGame")
        {
            animator.SetFloat("lastHit", Time.time - Mathf.Max(MainValue.Instance.FloatValue["right"], MainValue.Instance.FloatValue["left"]));
        }
    }
}
