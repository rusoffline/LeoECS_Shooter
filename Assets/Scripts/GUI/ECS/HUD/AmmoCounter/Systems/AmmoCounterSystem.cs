using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoCounterSystem : IEcsRunSystem
{
    private EcsFilter<AmmoUpdateEvent> ammoUpdateFilter;
    private EcsFilter<PlayerComponent, InventoryComponent> playerInventoryFilter;

    private UIManager uiManager;

    public void Run()
    {
        foreach (var upd in ammoUpdateFilter)
        {
            foreach (var plr in playerInventoryFilter)
            {
                ref var entity = ref playerInventoryFilter.GetEntity(plr);
                ref var inventory = ref playerInventoryFilter.Get2(plr);

                if (entity.Has<HasWeapon>())
                {
                    ref var hasWeapn = ref entity.Get<HasWeapon>();
                    ref var weaponEntity = ref hasWeapn.weaponEntity;

                    if (weaponEntity.Has<WeaponComponent>())
                    {
                        ref var weaponComponent = ref weaponEntity.Get<WeaponComponent>();

                        switch (weaponComponent.weaponData)
                        {
                            case FirearmWeaponData firearmData:

                                var ammoData = firearmData.requiredAmmo;
                                var weaponItem = inventory.items.FirstOrDefault(item => item.itemData == firearmData);
                                var ammoItem = inventory.items.FirstOrDefault(item => item.itemData == ammoData);
                                var current = weaponItem.count;
                                var total = ammoItem == null ? 0 : ammoItem.count;

                                uiManager.hudScreen.ammoCounter.UpdateAmmo(current, total);
                                break;
                            case MeleeWeaponData meleeData:
                                break;
                        }

                    }
                    else
                    {
                        Debug.LogError($"dont have Weapon Component");
                    }
                }
            }
        }
    }
}
