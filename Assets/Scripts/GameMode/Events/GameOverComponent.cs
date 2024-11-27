using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameOverComponent
{
    public bool isWin;
    public float delay;

    public GameOverComponent(bool isWin, float delay = 0f)
    {
        this.isWin = isWin;
        this.delay = delay;
    }
}
