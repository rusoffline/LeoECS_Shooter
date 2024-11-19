[System.Serializable]
public class Item
{
    public ItemData itemData;
    public int count;

    public Item(ItemData itemData, int count = 1) 
    { 
        this.itemData = itemData; 
        this.count = count; 
    }
}
