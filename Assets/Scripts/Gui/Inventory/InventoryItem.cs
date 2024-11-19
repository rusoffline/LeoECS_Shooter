using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Button itemButton;
    public Image itemImage;
    public TMP_Text itemName;
    public TMP_Text itemCount;

    public Item item;

    internal void SetData(Item item)
    {
        this.item = item;
        itemImage.sprite = item.itemData.itemIcon;
        itemName.text = item.itemData.itemName;
        itemCount.text = item.count.ToString();
    }
}
