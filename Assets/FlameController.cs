using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class FlameController : MonoBehaviour
{
    public FlameConfig[] fc;
    VisualEffect flame;

    public static readonly string FLAME_RATE = "Spawn Rate";
    public static readonly string SWARM_RATE = "Swarm Rate";
    public static readonly string SMOKE_RATE = "Smoke Rate";

    public static readonly string FLAME_COLOR = "Fire Colors";
    public static readonly string SWARM_COLOR = "Swarm Gradient";
    public static readonly string SMOKE_COLOR = "Color";
    // Start is called before the first frame update
    void Start()
    {
        flame = gameObject.GetComponent<VisualEffect>();  
    }

    public void SetLevel(int level)
    {
        if(level == -1)
        {
            flame.SetFloat(FLAME_RATE, 0f);
            flame.SetFloat(SWARM_RATE, 0f);
            flame.SetFloat(SMOKE_RATE, 0f);
        }
        else
        {
            Gradient swarm = fc[level].swarm;
            Gradient fire = fc[level].fire;
            Color smoke = fc[level].smoke;

            flame.SetGradient(FLAME_COLOR, fire);
            flame.SetGradient(SWARM_COLOR, swarm);
            //flame.
            StartCoroutine(LightUpSequence());
        }
    }

    IEnumerator LightUpSequence()
    {
        flame.SetFloat(SWARM_RATE, 10f);
        flame.SetFloat(FLAME_RATE, 1f);
        yield return new WaitForSeconds(0.4f);
        flame.SetFloat(SMOKE_RATE, 10f);
    }
}

[System.Serializable]
public class FlameConfig
{
    public Gradient swarm;
    public Gradient fire;
    public Color smoke;
}
