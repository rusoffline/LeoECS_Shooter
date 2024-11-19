using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeHitSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, AttackEvent> attackFilter;
    private WeaponService weaponService;
    private PlayerData playerData;

    public void Run()
    {
        foreach(var atk  in attackFilter)
        {
            Debug.Log("MeleeHitSystem. AttackFilter");
            ref var playerComponent = ref attackFilter.Get1(atk);
            ref var playerEntity = ref attackFilter.GetEntity(atk);

            if (playerEntity.Has<HasWeapon>())
            {
                Debug.Log("MeleeHitSystem. Has WeaponComponent");
                ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
                ref var weaponComponent = ref hasWeapon.weaponEntity.Get<WeaponComponent>();
                if (weaponComponent.weaponData is MeleeWeaponData meleeWeaponData)
                {
                    Debug.Log("MeleeHitSystem. Melee Hit!");
                    var origint = weaponComponent.weaponObject.transform.position;
                    var radius = weaponComponent.weaponData.distance;
                    var damage = weaponComponent.weaponData.damage;
                    var mask = playerData.weaponInteractableMask;
                    var isHitted = weaponService.MeleeCast(origint, radius, damage, mask);
                    if (isHitted)
                    {
                        var meleeData = weaponComponent.weaponData as MeleeWeaponData;
                        AudioSource.PlayClipAtPoint(meleeData.hitClip, origint);
                    }
                }
            }
        }
    }
}