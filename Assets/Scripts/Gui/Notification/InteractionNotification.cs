using System;
using TMPro;
using UnityEngine;

public class InteractionNotification: MonoBehaviour
{
    public TMP_Text interactionText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowNotification(string message)
    {
        gameObject.SetActive(true);
        interactionText.text = message;
        CancelInvoke("HideNotification");
        Invoke("HideNotification", 3f);
    }

    public void HideNotification()
    {
        gameObject.SetActive(false);
    }
}