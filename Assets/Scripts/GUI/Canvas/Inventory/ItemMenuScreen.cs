using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuScreen : MonoBehaviour
{
    private InventoryScreen inventoryScreen;
    public Button useButton;
    public Button inspectButton;
    public Button assignWeaponSlotButton;

    private InventoryItem selectedItem;

    public void OpenItemMenu(InventoryScreen inventoryScreen, InventoryItem inventoryItem)
    {
        this.inventoryScreen = inventoryScreen;
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);

        selectedItem = inventoryItem;

        useButton.gameObject.SetActive(inventoryItem.item.itemData is WeaponData || inventoryItem.item.itemData is MedkitData);
        inspectButton.gameObject.SetActive(true);
        assignWeaponSlotButton.gameObject.SetActive(inventoryItem.item.itemData is WeaponData);

        useButton.onClick.AddListener(UseItem);
        inspectButton.onClick.AddListener(InspectItem);
        assignWeaponSlotButton.onClick.AddListener(AssignToWeaponSlot);
    }

    public void CloseItemMenu()
    {
        gameObject.SetActive(false);
        useButton.onClick.RemoveAllListeners();
        inspectButton.onClick.RemoveAllListeners();
        assignWeaponSlotButton.onClick.RemoveAllListeners();
    }

    private void UseItem()
    {
        Debug.Log($"Using {selectedItem.itemName}");
        inventoryScreen.entity.Replace(new UseItemEvent(selectedItem.item));
        //switch (selectedItem.item.itemData)
        //{
        //    case WeaponData weaponData:
        //        inventoryScreen.entity.Replace(new WeaponEquipEvent(selectedItem.item));
        //        break;
        //    case MedkitData mekitData:
        //        break;
        //}
        CloseItemMenu();
    }

    private void InspectItem()
    {
        Debug.Log($"Inspecting {selectedItem.itemName}");
        CloseItemMenu();
    }

    private void AssignToWeaponSlot()
    {
        Debug.Log($"Assigning {selectedItem.itemName} to slot");
        CloseItemMenu();
    }
}
