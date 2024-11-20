using System;
using TMPro;
using UnityEngine;

public class PickupNotification : MonoBehaviour
{
    public TMP_Text pickupText;

    private void Start()
    {
        HideNotification();
    }

    public void ShowNotification(string itemName, int count)
    {
        gameObject.SetActive(true);
        pickupText.text = $"{itemName} ({count})";
        CancelInvoke("HideNotification");
        Invoke("HideNotification", 2f);
    }

    public void HideNotification()
    {
        gameObject.SetActive(false);
    }
}
