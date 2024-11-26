using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameOverEvent
{
    public bool isWin;

    public GameOverEvent(bool isWin)
    {
        this.isWin = isWin;
    }
}
