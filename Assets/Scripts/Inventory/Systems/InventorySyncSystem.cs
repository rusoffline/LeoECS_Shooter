using Leopotam.Ecs;

public class InventorySyncSystem : IEcsRunSystem
{
    private EcsFilter<InventorySyncEvent> eventFilter;
    private EcsFilter<InventoryComponent> inventoryFilter;
    private UIManager uiManager;

    public void Run()
    {
        foreach (var evt in eventFilter)
        {
            foreach (var inv in inventoryFilter)
            {
                ref var inventory = ref inventoryFilter.Get1(inv);
                uiManager.inventoryScreen.Sync(inventory.items);
            }
        }
    }
}
