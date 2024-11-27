using Leopotam.Ecs;

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
