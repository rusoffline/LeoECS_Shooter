using UnityEngine;

public class DoorInteractable : UnlockeInteractable
{
    public Transform openDirectionReference;
    public bool autoClose = true;

    public BaseDoorAction doorAction;
    private bool isDoorOpen = false;

    public AudioSource openClip;
    public AudioSource closeClip;

    public override void Interact()
    {
        Debug.Log($"Interact with Door, name is {transform.name}");
        base.Interact();

        if (isDoorOpen || isLocked) return;

        if (visitior != null)
        {
            Vector3 directionToPlayer = (visitior.position - openDirectionReference.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

            if (dotProduct > 0)
            {
                OpenDoorsForward();
            }
            else
            {
                OpenDoorsBackward();
            }
            isDoorOpen = true;
            iconView.gameObject.SetActive(false);
        }
    }

    private void OpenDoorsForward()
    {
        doorAction?.OpenForward();
    }

    private void OpenDoorsBackward()
    {
        doorAction?.OpenBackward();
    }

    public void CloseDoors()
    {
        doorAction?.Close();
        isDoorOpen = false;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (autoClose && isDoorOpen)
        {
            CloseDoors();
        }
    }
}
