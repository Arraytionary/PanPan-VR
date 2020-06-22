using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Badge : MonoBehaviour
{
    TextMeshPro label;
    Animator badgeAnim;
    void Start()
    {
        label = gameObject.GetComponentInChildren<TextMeshPro>();
        badgeAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        badgeAnim.SetBool("requestedBadge", MainValue.Instance.requestedBadge);
        label.text = MainValue.Instance.toDisplayOnBadge;
    }
}
