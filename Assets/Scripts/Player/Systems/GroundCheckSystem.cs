using Leopotam.Ecs;
using UnityEngine;

public class GroundCheckSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, GroundedComponent> playerGroundCheckFilter;
    private PlayerData playerData;

    public void Run()
    {
        foreach (var plr in playerGroundCheckFilter)
        {
            ref var player = ref playerGroundCheckFilter.Get1(plr);
            ref var grounded = ref playerGroundCheckFilter.Get2(plr);

            float radius = .3f;

            grounded.isGrounded = Physics.CheckSphere(
                 player.position,
                 radius,
                 playerData.groundMask
            );
        }
    }
}
