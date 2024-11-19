public struct PickupNotifEvent
{
    public string itemName;
    public int count;

    public PickupNotifEvent(string itemName, int count)
    {
        this.itemName = itemName;
        this.count = count;
    }
}
