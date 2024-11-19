using Leopotam.Ecs;
using UnityEngine;

public class PlayerStateSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, IdleState, EnterState> idleEnterFilter;
    private EcsFilter<PlayerComponent, CrouchState, EnterState> crouchEnterFilter;
    private EcsFilter<PlayerComponent, FallState, EnterState> fallEnterFilter;
    private EcsFilter<PlayerComponent, AttackState, EnterState> attackEnterFilter;
    private EcsFilter<PlayerComponent, FallState, EnterState> attackUpdateFilter;

    private StateService stateService;

    public void Run()
    {
        foreach (var idl in idleEnterFilter)
        {
            ref var player = ref idleEnterFilter.Get1(idl);
            player.animator.CrossFade("Locomotion", .15f);
        }

        foreach (var crc in crouchEnterFilter)
        {
            ref var player = ref crouchEnterFilter.Get1(crc);
            player.animator.CrossFade("Crouching", .15f);

        }

        foreach (var fll in fallEnterFilter)
        {
            ref var player = ref fallEnterFilter.Get1(fll);
            player.animator.CrossFade("Falling", .15f);
        }

        foreach (var att in attackEnterFilter)
        {
            ref var player = ref attackEnterFilter.Get1(att);
        }

        foreach (var att in attackUpdateFilter)
        {
            ref var player = ref attackUpdateFilter.Get1(att);
            AnimatorStateInfo stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.normalizedTime >= 1f && !player.animator.IsInTransition(0))
            {
                ref var entity = ref attackUpdateFilter.GetEntity(att);
                stateService.SwitchState<IdleState>(ref entity);
            }
        }
    }
}

