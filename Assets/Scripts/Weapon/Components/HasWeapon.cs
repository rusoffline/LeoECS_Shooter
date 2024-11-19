using Leopotam.Ecs;
using UnityEngine;

public struct HasWeapon
{
    public EcsEntity weaponEntity;
    public HasWeapon(EcsEntity weaponEntity)
    {
        this.weaponEntity = weaponEntity;
    }
}
