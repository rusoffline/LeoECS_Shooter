using Leopotam.Ecs;
using UnityEngine;

public class PlayerControlSystem : IEcsRunSystem
{
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<PlayerComponent, HealthComponent, DamageEvent> damageFilter;
    private EcsFilter<PlayerComponent, GroundedComponent>
        .Exclude<StateLifetime>
        playerFilter;
    private StateService stateService;

    public void Run()
    {
        foreach (var inp in inputFilter)
        {
            ref var input = ref inputFilter.Get1(inp);

            foreach (var dmg in damageFilter)
            {
                ref var entity = ref damageFilter.GetEntity(dmg);
                ref var health = ref damageFilter.Get2(dmg);

                if (health.currentHealth <= 0)
                {
                    stateService.SwitchState<DeathState>(ref entity);
                }
                else
                {
                    stateService.SwitchState<DamageState>(ref entity, .5f);
                }
                entity.Get<HealthUpdateEvent>();
            }

            foreach (var plr in playerFilter)
            {
                ref var entity = ref playerFilter.GetEntity(plr);
                ref var player = ref playerFilter.Get1(plr);
                ref var ground = ref playerFilter.Get2(plr);

                if (ground.isGrounded)
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

