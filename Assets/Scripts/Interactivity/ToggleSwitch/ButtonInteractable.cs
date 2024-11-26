using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInteractable : BaseInteractable
{
    public AudioClip buttonClickClip;
    public UnityEvent OnActivate;

    [SerializeField]
    private bool isOneTime = false;
    private bool isActivated = false;

    public override void Interact()
    {
        if (isOneTime && isActivated)
        {
            return;
        }

        isActivated = true;

        OnActivate?.Invoke();

        if (isOneTime)
        {
            Debug.Log(" нопка больше неактивна, т.к. она одноразова€.");
        }

        if (buttonClickClip != null)
        {
            AudioSource.PlayClipAtPoint(buttonClickClip, iconView.transform.position);
        }

        if (isOneTime && isActivated)
        {
            HideIcon();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (isOneTime && isActivated)
        {
            return;
        }
        base.OnTriggerEnter(other);
    }

    public void SetOneTime(bool value)
    {
        isOneTime = value;
    }

    public void ResetButton()
    {
        isActivated = false;
        Debug.Log("ResetButton.");
    }
}
