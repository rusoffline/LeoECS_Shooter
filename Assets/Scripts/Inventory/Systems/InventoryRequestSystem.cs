﻿using Leopotam.Ecs;
using UnityEngine;

public class InventoryRequestSystem : IEcsRunSystem
{
    private EcsFilter<RequestItemEvent> useFilter;
    private EcsFilter<InventoryComponent> inventoryFilter;

    public void Run()
    {
        foreach (var use in useFilter)
        {
            ref var useItem = ref useFilter.Get1(use);
            var isSuccess = TryUseItem(ref inventoryFilter, ref useItem);
            if (isSuccess)
            {
                useItem.OnSuccess?.Invoke();
            }
            else
            {
                useItem.OnFailure?.Invoke();
            }
        }
    }

    public bool TryUseItem(ref EcsFilter<InventoryComponent> inventoryFilter, ref RequestItemEvent useItem)
    {
        foreach (var i in inventoryFilter)
        {
            ref var inventory = ref inventoryFilter.Get1(i);

            for (int j = 0; j < inventory.items.Count; j++)
            {
                if (inventory.items[j].itemData == useItem.itemData && inventory.items[j].count >= useItem.count)
                {
                    if (useItem.itemData is WeaponData)
                        return true;

                    var item = inventory.items[i];
                    item.count -= useItem.count;
                    inventory.items[i] = item;

                    if (item.count <= 0)
                    {
                        inventory.items.RemoveAt(j);
                    }
                    return true;
                }
            }
        }

        return false;
    }
}
