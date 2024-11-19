using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Item Data:")]
    public Sprite itemIcon;
    public string itemName;
    [TextArea(3, 5)] public string description;
}
