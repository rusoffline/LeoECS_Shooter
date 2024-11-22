using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryScreen : EntityOwner, IScreen
{
    public List<InventoryItem> uiItems;
    public ItemMenuScreen itemMenuScreen;
    public List<Item> itemsForTest;

    private void Start()
    {
        uiItems = GetComponentsInChildren<InventoryItem>().ToList();
        foreach (var item in uiItems)
        {
            item.gameObject.SetActive(false);
        }
        itemMenuScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        itemMenuScreen.gameObject.SetActive(false);
    }

    public bool TryClose()
    {
        if(itemMenuScreen.gameObject.activeSelf)
        {
            itemMenuScreen.TryClose();
            return false;
        }
        return true;
    }

    public void Sync(List<Item> itemList)
    {
        foreach (InventoryItem item in uiItems)
        {
            item.gameObject.SetActive(false);
        }

        foreach(var item in itemList)
        {
            InventoryItem inventoryItem = GetFreeItem();
            inventoryItem.SetData(item);
            inventoryItem.gameObject.SetActive(true);

            inventoryItem.itemButton.onClick.RemoveAllListeners();
            inventoryItem.itemButton.onClick.AddListener(() => OnItemClick(inventoryItem));
        }

        //for test!
        itemsForTest = itemList;
    }

    public InventoryItem GetFreeItem()
    {
        foreach(InventoryItem item in uiItems)
        {
            if(!item.gameObject.activeSelf)
            {
                return item;
            }
        }
        InventoryItem newItem = Instantiate(uiItems[0], uiItems[0].transform.parent);
        uiItems.Add(newItem);
        return newItem;
    }

    public void OnItemClick(InventoryItem inventoryItem)
    {
        Debug.Log(inventoryItem);
        Debug.Log(itemMenuScreen);
        itemMenuScreen.OpenItemMenu(this, inventoryItem);
    }
}
