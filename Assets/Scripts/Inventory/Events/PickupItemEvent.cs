using UnityEngine.Events;

public struct PickupItemEvent
{
    public ItemData itemData;
    public int count;
    public UnityAction OnSuccess;
    public UnityAction OnFailure;

    public PickupItemEvent(ItemData itemData, int count, UnityAction OnSuccess, UnityAction OnFailure)
    {
        this.itemData = itemData;
        this.count = count;
        this.OnSuccess = OnSuccess;
        this.OnFailure = OnFailure;
    }
}
