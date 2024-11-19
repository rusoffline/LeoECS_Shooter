using Leopotam.Ecs;
using System.Linq;
using UnityEngine;

public class WeaponReloadSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, InventoryComponent>.Exclude<DamageState, DeathState> playerFilter;
    private EcsFilter<WeaponComponent, TryReload>.Exclude<ReloadProcess> tryReloadFilter;
    private EcsFilter<WeaponComponent, ReloadProcess> reloadProcessFilter;

    public void Run()
    {
        foreach (var plr in playerFilter)
        {
            ref var player = ref playerFilter.Get1(plr);
            ref var inventory = ref playerFilter.Get2(plr);

            foreach (var rld in tryReloadFilter)
            {
                ref var weaponComponent = ref tryReloadFilter.Get1(rld);

                if (weaponComponent.weaponData is FirearmWeaponData firearmData)
                {
                    ref var weaponEntity = ref tryReloadFilter.GetEntity(rld);
                    var weaponItem = weaponComponent.weaponItem;
                    if (weaponItem.count < firearmData.magazineCapacity)
                    {
                        var ammoData = firearmData.requiredAmmo;
                        var ammoItem = inventory.items.FirstOrDefault(item => item.itemData == ammoData);
                        Debug.Log($"try get ammo = {ammoItem}");

                        if (ammoItem != null && ammoItem.count > 0)
                        {
                            player.animator.Play("Weapon_Reload", 1);
                            var firearmObject = weaponComponent.weaponObject as FirearmObject;
                            firearmObject.audioSource.PlayOneShot(firearmData.reloadClip);
                            weaponEntity.Replace(new ReloadProcess(firearmData.reloadTime));
                        }
                    }
                }
            }

            foreach (var rlp in reloadProcessFilter)
            {
                ref var weaponComponent = ref reloadProcessFilter.Get1(rlp);
                ref var reload = ref reloadProcessFilter.Get2(rlp);
                reload.value -= Time.deltaTime;
                if (reload.value <= 0)
                {
                    var firearmData = weaponComponent.weaponData as FirearmWeaponData;
                    var requiredAmmo = firearmData.requiredAmmo;
                    var ammoItem = inventory.items.FirstOrDefault(item => item.itemData == requiredAmmo);
                    var weaponItem = weaponComponent.weaponItem;

                    int ammoToReload = firearmData.magazineCapacity - weaponItem.count;

                    int ammoLoaded = Mathf.Min(ammoToReload, ammoItem.count);

                    weaponItem.count += ammoLoaded;

                    ammoItem.count -= ammoLoaded;

                    if (ammoItem.count <= 0)
                    {
                        inventory.items.Remove(ammoItem);
                    }

                    ref var entity = ref reloadProcessFilter.GetEntity(rlp);

                    entity.Del<ReloadProcess>();
                    entity.Get<AmmoUpdateEvent>();
                }
            }
        }
    }
}
