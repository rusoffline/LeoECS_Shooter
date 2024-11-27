using Leopotam.Ecs;
using UnityEngine;

public class PickableItem : BaseInteractable
{
    public ItemData itemData;
    public int count;
    public AudioClip pickupClip;

    public override void Interact()
    {
        entity.Replace(new PickupItemEvent(itemData, count, () => OnPickupSuccess(), () => OnPickupFailed()));
    }

    private void OnPickupSuccess()
    {
        entity.Destroy();
        Destroy(gameObject);
        Debug.Log($"Pickable.OnPickupSuccess. Name = {transform.name}");


        if (pickupClip != null)
        {
            AudioSource.PlayClipAtPoint(pickupClip, transform.position);
        }
    }

    private void OnPickupFailed()
    {
        Debug.Log($"Pickable.OnPickupFailed. Name = {transform.name}");
    }
}
