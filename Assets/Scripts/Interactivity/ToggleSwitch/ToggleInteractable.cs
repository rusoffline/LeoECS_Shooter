using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleInteractable : BaseInteractable
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    [SerializeField]
    private bool isOneTime = false;
    private bool isActivated = false;

    public override void Interact()
    {
        if (isOneTime && isActivated)
        {
            return;
        }

        isActivated = !isActivated;

        if (isActivated)
        {
            OnActivate?.Invoke();
        }
        else
        {
            OnDeactivate?.Invoke();
        }

        if (isOneTime && isActivated)
        {
            Debug.Log("Выключатель больше неактивен, т.к. он одноразовый.");
        }

        HideIcon();
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

    public void ResetSwitch()
    {
        isActivated = false;
    }
}
