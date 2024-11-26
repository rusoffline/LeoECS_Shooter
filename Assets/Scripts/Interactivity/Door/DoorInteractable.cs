using UnityEngine;

public class DoorInteractable : UnlockeInteractable
{
    public Transform openDirectionReference;
    public bool autoClose = true;

    public BaseDoorAction doorAction;
    private bool isDoorOpen = false;

    public AudioClip openClip;
    public AudioClip closeClip;

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
            HideIcon();
        }
    }

    private void OpenDoorsForward()
    {
        doorAction?.OpenForward();
        PlayClip(openClip);
    }

    private void OpenDoorsBackward()
    {
        doorAction?.OpenBackward();
        PlayClip(openClip);
    }

    public void CloseDoors()
    {
        doorAction?.Close();
        isDoorOpen = false;
        PlayClip(closeClip);
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
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
