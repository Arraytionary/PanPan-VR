using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SaveObject
{
    public int highScore;
    public bool cleared;

    public SaveObject(int highScore, bool cleared)
    {
        this.highScore = highScore;
        this.cleared = cleared;
    }
}
