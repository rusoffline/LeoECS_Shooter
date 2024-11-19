using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInitSystem : IEcsInitSystem
{
    private EcsFilter<PlayerComponent> playerFilter;
    private UIManager uiManager;

    public void Init()
    {
        foreach (var plr in playerFilter)
        {
            ref var playerEntity = ref playerFilter.GetEntity(plr);
            var inventory = new InventoryComponent()
            {
                items = new List<Item>()
            };
            playerEntity.Replace(inventory);

            uiManager.inventoryScreen.entity = playerEntity;
        }
    }
}
