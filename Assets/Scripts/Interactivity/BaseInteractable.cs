using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class BaseInteractable : EntityOwner
{
    private SphereCollider triggerCollider;
    public IconInteractableView iconView;
    protected Transform visitior;

    public abstract void Interact();

    protected virtual void Start()
    {
        iconView = GetComponentInChildren<IconInteractableView>();  
        triggerCollider = gameObject.GetOrAddComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.radius = 4f;
        gameObject.layer = 8;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        visitior = other.transform;
        entity.Replace(new InteractableStay(this));
        iconView.gameObject.SetActive(true);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        visitior = null;
        entity.Del<InteractableStay>();
        iconView.gameObject.SetActive(false);
    }
}

