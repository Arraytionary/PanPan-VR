using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLight : MonoBehaviour
{
    Light light;
    Color baseColor;
    float baseIntensity;
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
        baseColor = light.color;
        baseIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.Max(light.intensity - 0.5f, baseIntensity);
        float r = Mathf.Lerp(light.color.r, baseColor.r, 3*Time.deltaTime);
        float g = Mathf.Lerp(light.color.g, baseColor.g, 3 * Time.deltaTime);
        float b = Mathf.Lerp(light.color.b, baseColor.b, 3 * Time.deltaTime);
        light.color = new Color(r, g, b);
    }
}
