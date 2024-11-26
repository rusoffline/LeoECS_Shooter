using Leopotam.Ecs;
using UnityEngine;

public class InventoryUseItemSystem : IEcsRunSystem
{
    private EcsFilter<InventoryComponent, UseItemEvent> useItemFilter;
    private EcsFilter<PlayerComponent, HealthComponent> healthFilter;
    private PlayerData playerData;

    public void Run()
    {
        foreach (var utm in useItemFilter)
        {
            ref var inventoryEntity = ref useItemFilter.GetEntity(utm);
            ref var inventoryComponent = ref useItemFilter.Get1(utm);
            ref var useItem = ref useItemFilter.Get2(utm);

            switch (useItem.item.itemData)
            {
                case WeaponData weaponData:
                    inventoryEntity.Replace(new WeaponEquipEvent(useItem.item));
                    break;
                case MedkitData mekitData:
                    bool isSuccess = TryUseAid(mekitData);
                    if (isSuccess)
                    {
                        useItem.item.count--;
                        if (useItem.item.count == 0)
                        {
                            inventoryComponent.items.Remove(useItem.item);
                        }

                        inventoryEntity.Get<InventorySyncEvent>();
                        inventoryEntity.Get<HealthUpdateEvent>();
                    }
                    else
                    {
                        inventoryEntity.Replace(new InteractNotifEvent("Здоровье полное (Аптечка не исползована)"));
                    }
                    break;
            }
        }
    }

    private bool TryUseAid(MedkitData medkitData)
    {
        foreach (var plr in healthFilter)
        {
            ref var healthComponent = ref healthFilter.Get2(plr);
            if (healthComponent.currentHealth >= playerData.maxHealth)
            {
                return false;
            }
            else
            {
                healthComponent.currentHealth = Mathf.Clamp(healthComponent.currentHealth + medkitData.healthRestoreAmount, 0, playerData.maxHealth);
            }
        }
        return true;
    }
}