using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, IdleState, EnterState> idleEnterFilter;
    private EcsFilter<PlayerComponent, CrouchState, EnterState> crouchEnterFilter;
    private EcsFilter<PlayerComponent, FallState, EnterState> fallEnterFilter;
    private EcsFilter<PlayerComponent, AttackState, EnterState> attackEnterFilter;
    private EcsFilter<PlayerComponent, FallState, EnterState> attackUpdateFilter;
    private EcsFilter<PlayerComponent, DamageState, EnterState> damageEnterFilter;
    private EcsFilter<PlayerComponent, DeathState, EnterState> deathEnterFilter;
    private EcsFilter<GameMode> gameModeFilter;

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
        foreach (var dmg in damageEnterFilter)
        {
            ref var player = ref damageEnterFilter.Get1(dmg);
            player.animator.CrossFade("HitReaction", .1f);
        }
        foreach (var dth in deathEnterFilter)
        {
            ref var player = ref deathEnterFilter.Get1(dth);
            player.animator.CrossFade("Death", .1f);

            ref var playerEntity = ref deathEnterFilter.GetEntity(dth);

            foreach (var gmd in gameModeFilter)
            {
                ref var gameEntity = ref gameModeFilter.GetEntity(gmd);
                gameEntity.Replace(new GameOverEvent(false));
                Debug.Log("Player is Dead");
            }
        }
    }
}
public class PlayerDeathSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, DeathState> playerDeathFilter;

    public void Run()
    {
        foreach(var dth in playerDeathFilter)
        {
            ref var playerEntity = ref playerDeathFilter.GetEntity(dth);
            playerEntity.Destroy();
        }
    }
}
public class PlayerVoiceSystem : IEcsRunSystem
{
    //private EcsFilter<PlayerComponent, IdleState, EnterState> idleEnterFilter;
    //private EcsFilter<PlayerComponent, CrouchState, EnterState> crouchEnterFilter;
    //private EcsFilter<PlayerComponent, FallState, EnterState> fallEnterFilter;
    //private EcsFilter<PlayerComponent, AttackState, EnterState> attackEnterFilter;
    //private EcsFilter<PlayerComponent, FallState, EnterState> attackUpdateFilter;
    private EcsFilter<PlayerComponent, DamageState, EnterState> damageEnterFilter;
    private EcsFilter<PlayerComponent, DeathState, EnterState> deathEnterFilter;

    private PlayerData playerData;

    public void Run()
    {
        PlayEnterStateVoice(damageEnterFilter, playerData.hurtClipContainer);
        PlayEnterStateVoice(deathEnterFilter, playerData.deathClipContainer);

    }

    private void PlayEnterStateVoice(EcsFilter filter, AudioClipContainer clipContainer)
    {
        foreach (var flt in filter)
        {
            ref var entity = ref filter.GetEntity(flt);
            if (entity.Has<PlayerComponent>() && clipContainer.HasClip)
            {
                ref var playerComponent = ref entity.Get<PlayerComponent>();
                playerComponent.topAudioSource.PlayOneShot(clipContainer.GetClip);
            }
        }
    }
}
