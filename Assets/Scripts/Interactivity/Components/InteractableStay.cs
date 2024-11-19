using UnityEngine;

public struct InteractableStay
{
    public BaseInteractable interactable;
    public IconInteractableView iconView => interactable.iconView;
    public Vector3 iconPosition => iconView.transform.position;
    public Transform transform => interactable.transform;
    public Vector3 position => transform.position;

    public InteractableStay(BaseInteractable interactable)
    {
        this.interactable = interactable;
    }
}

