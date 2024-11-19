using Leopotam.Ecs;
using System.Linq;
using UnityEngine;

public class InventoryPickupSystem : IEcsRunSystem
{
    private EcsFilter<PickupItemEvent> pickupFilter;
    private EcsFilter<InventoryComponent> inventoryFilter;

    public void Run()
    {
        foreach (var inv in inventoryFilter)
        {
            ref var inventory = ref inventoryFilter.Get1(inv);
            ref var entity = ref inventoryFilter.GetEntity(inv);

            foreach (var pck in pickupFilter)
            {
                ref var pickup = ref pickupFilter.Get1(pck);
                bool isSuccess = TryAddItemToInventory(ref entity, ref inventory, ref pickup);
                if (isSuccess)
                {
                    pickup.OnSuccess?.Invoke();
                    break;
                }
                else
                {
                    pickup.OnSuccess?.Invoke();
                    break;
                }
            }
        }
    }

    private bool TryAddItemToInventory(ref EcsEntity entity, ref InventoryComponent inventory, ref PickupItemEvent pickup)
    {
        bool isSuccess = false;

        var data = pickup.itemData;
        var item = inventory.items.FirstOrDefault(item => item.itemData == data);

        if (item != null)
        {
            item.count += pickup.count;
        }
        else
        {
            item = new Item(pickup.itemData, pickup.count);
            inventory.items.Add(item);
        }

        entity.Replace(new PickupNotifEvent(pickup.itemData.itemName, pickup.count));

        switch (item.itemData)
        {
            case WeaponData weaponData:
                entity.Replace(new WeaponEquipEvent(item));
                break;
            case AmmoData ammoData:
                entity.Get<AmmoUpdateEvent>();
                break;
            default:
                break;
        }

        isSuccess = true;
        return isSuccess;
    }
}
