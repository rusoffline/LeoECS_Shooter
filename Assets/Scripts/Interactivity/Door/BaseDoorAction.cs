using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDoorAction : MonoBehaviour
{
    public abstract void OpenForward();
    public abstract void OpenBackward();
    public abstract void Close();
}
