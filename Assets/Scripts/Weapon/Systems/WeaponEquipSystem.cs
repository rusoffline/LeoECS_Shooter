using Leopotam.Ecs;
using System;
using UnityEngine;

public class WeaponEquipSystem : IEcsRunSystem
{
    private EcsFilter<WeaponEquipEvent> equipFilter;
    private EcsFilter<PlayerComponent> playerFilter;
    private EcsFilter<WeaponComponent> weaponFilter;

    private EcsWorld world;

    public void Run()
    {
        foreach (var eqp in equipFilter)
        {
            ref var equipWeaponComponent = ref equipFilter.Get1(eqp);
            ref var equipEntity = ref equipFilter.GetEntity(eqp);

            foreach (var plr in playerFilter)
            {
                ref var playerEntity = ref playerFilter.GetEntity(plr);
                ref var player = ref playerFilter.Get1(plr);
                UpdatePlayerWeaponVisual(ref equipWeaponComponent.weaponItem, ref playerEntity);

                equipEntity.Del<WeaponEquipEvent>();
                equipEntity.Get<AmmoUpdateEvent>();
            }
        }
    }

    private void UpdatePlayerWeaponVisual(ref Item item, ref EcsEntity playerEntity)
    {
        Transform weaponParent = playerEntity.Get<PlayerComponent>().weaponHand;

        //добработать логику:
        //  1. отключить оружия если это не текущее оружие
        //  2. включить оружие если есть среди неактивных
        //  3. инсталировать если нет оружия
        foreach (Transform child in weaponParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (item.itemData is WeaponData weaponData)
        {
            WeaponObject weaponInstance = GameObject.Instantiate(weaponData.weaponObject, weaponParent);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;

            //destroy weapon entity:
            foreach (var wpn in weaponFilter)
            {
                weaponFilter.GetEntity(wpn).Destroy();
            }
            //create new weapon entity:
            var weaponEntity = world.NewEntity();

            var weaponComponent = new WeaponComponent();
            weaponComponent.weaponItem = item;
            weaponComponent.weaponData = weaponData;
            weaponComponent.weaponObject = weaponInstance;
            weaponEntity.Replace(weaponComponent);

            playerEntity.Replace(new HasWeapon(weaponEntity));

            var playerComponent = playerEntity.Get<PlayerComponent>();

            playerComponent.animator.SetInteger("WeaponIndex", weaponData.weaponIndex);
            playerComponent.animator.Play("Weapon_Equip", 1);

            weaponEntity.Replace(new FireContdown(1.1f));
        }
    }
}
