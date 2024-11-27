using UnityEngine;

public class DoorInteractable : UnlockeInteractable
{
    public Transform openDirectionReference;
    public bool autoClose = true;

    public BaseDoorAction doorAction;

    public override void Interact()
    {
        Debug.Log($"Interact with Door, name is {transform.name}");
        base.Interact();

        if (doorAction.IsOpen || isLocked) return;

        if (visitior != null)
        {
            Vector3 directionToPlayer = (visitior.position - openDirectionReference.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

            if (dotProduct > 0)
            {
                doorAction?.OpenForward();
            }
            else
            {
                doorAction?.OpenBackward();
            }
            HideIcon();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (autoClose && doorAction.IsOpen)
        {
            doorAction?.Close();
        }
    }
}
