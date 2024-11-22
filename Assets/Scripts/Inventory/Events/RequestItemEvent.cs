using System.Threading;
using UnityEngine.Events;

public struct RequestItemEvent
{
    public ItemData itemData;
    public int count;
    public UnityAction OnSuccess;
    public UnityAction OnFailure;

    public RequestItemEvent(ItemData itemData, int count, UnityAction onSuccess, UnityAction onFailure)
    {
        this.itemData = itemData;
        this.count = count;
        this.OnSuccess = onSuccess;
        this.OnFailure = onFailure;
    }
}
