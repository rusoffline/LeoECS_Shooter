using Leopotam.Ecs;
using UnityEngine;

public class UnlockeInteractable : BaseInteractable
{
    public ItemData requiredItem;
    public bool isLocked;
    [SerializeField] private AudioClip failedClip;
    [SerializeField] private AudioClip unlcokClip;
    [SerializeField][TextArea(2, 5)] public string lockedText;
    [SerializeField][TextArea(2, 5)] public string unlockedText;

    public override void Interact()
    {
        if (isLocked)
        {
            entity.Replace(new UseItemEvent(requiredItem, 1, () => Unlock(), () => Failed()));
            return;
        }
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log($"{name} unlocked!");
        entity.Replace(new IteractNotifEvent(unlockedText));
    }

    public void Failed()
    {
        Debug.Log($"{name} unlock failed!");
        entity.Replace(new IteractNotifEvent(lockedText));
    }
}
