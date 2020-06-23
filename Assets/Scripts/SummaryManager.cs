using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryManager : MonoBehaviour
{
    public TextMeshPro score;

    void Start()
    {
        Utility.RequestBadge("Results");
    }

    // Update is called once per frame
    void Update()
    {
        score.text = MainValue.Instance.score.ToString();
    }
}
