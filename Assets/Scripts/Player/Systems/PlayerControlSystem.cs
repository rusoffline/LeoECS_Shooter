using Leopotam.Ecs;
using UnityEngine;

public class PlayerControlSystem : IEcsRunSystem
{
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<PlayerComponent, GroundedComponent>
        .Exclude<AttackState>
        .Exclude<DamageState>
        playerFilter;
    private StateService stateService;

    public void Run()
    {
        foreach(var inp in inputFilter)
        {
            ref var input = ref inputFilter.Get1(inp);

            foreach(var plr in playerFilter)
            {
                ref var entity = ref playerFilter.GetEntity(plr);
                ref var player = ref playerFilter.Get1(plr);
                ref var ground = ref playerFilter.Get2(plr);

                if(entity.Has<DamageState>() || entity.Has<LandState>() || entity.Has<AttackState>())
                {
                    continue;
                }

                if(ground.isGrounded)
                {
                    if (input.aim)
                    {
                        stateService.SwitchState<CrouchState>(ref entity);
                    }
                    else
                    {
                        stateService.SwitchState<IdleState>(ref entity);
                    }
                }
                else
                {
                    stateService.SwitchState<FallState>(ref entity);
                }
            }
        }
    }
}

