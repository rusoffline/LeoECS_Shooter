using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, VirtualCameraComponent> aimFilter;
    private PlayerData playerData;
    public void Run()
    {
        foreach(var aim in aimFilter)
        {
            ref var player = ref aimFilter.Get1(aim);
            ref var camera = ref aimFilter.Get2(aim);

            var position = camera.target.position + camera.target.forward * 5f;
            player.aimTarget.position = position;
        }
    }
}
