using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuScreen : MonoBehaviour, IScreen
{
    private InventoryScreen inventoryScreen;
    public Button useButton;
    public Button inspectButton;
    public Button assignWeaponSlotButton;

    private InventoryItem selectedItem;


    public bool TryClose()
    {
        if (gameObject.activeSelf)
        {
            CloseMenu();
            return false;
        }
        return true;
    }

    public void OpenMenu(InventoryScreen inventoryScreen, InventoryItem inventoryItem)
    {
        this.inventoryScreen = inventoryScreen;
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);

        selectedItem = inventoryItem;

        useButton.gameObject.SetActive(inventoryItem.item.itemData is WeaponData);
        inspectButton.gameObject.SetActive(true);
        assignWeaponSlotButton.gameObject.SetActive(inventoryItem.item.itemData is WeaponData);

        useButton.onClick.AddListener(UseItem);
        inspectButton.onClick.AddListener(InspectItem);
        assignWeaponSlotButton.onClick.AddListener(AssignToWeaponSlot);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        useButton.onClick.RemoveAllListeners();
        inspectButton.onClick.RemoveAllListeners();
        assignWeaponSlotButton.onClick.RemoveAllListeners();
    }

    private void UseItem()
    {
        Debug.Log($"Using {selectedItem.itemName}");
        if(selectedItem.item.itemData is WeaponData weaponData)
        {
            inventoryScreen.entity.Replace(new WeaponEquipEvent(selectedItem.item));
        }
        CloseMenu();
    }

    private void InspectItem()
    {
        Debug.Log($"Inspecting {selectedItem.itemName}");
        CloseMenu();
    }

    private void AssignToWeaponSlot()
    {
        Debug.Log($"Assigning {selectedItem.itemName} to slot");
        CloseMenu();
    }
}
