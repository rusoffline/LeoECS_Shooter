using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;

public class EnemyVoiceSystem : IEcsRunSystem
{
    //private EcsFilter<EnemyComponent, IdleState, EnterState> idleEnterState;
    private EcsFilter<EnemyComponent, PursueState, EnterState> pursueEnterState;
    private EcsFilter<EnemyComponent, ChaseState, EnterState> chaseEnterState;
    //private EcsFilter<EnemyComponent, WalkState, EnterState> walkEnterState;
    private EcsFilter<EnemyComponent, AttackState, EnterState> attackEnterFilter;
    private EcsFilter<EnemyComponent, DamageState, EnterState> damageEnterFilter;
    private EcsFilter<EnemyComponent, DeathState, EnterState> deathEnterFilter;

    private EnemyData enemyData;

    public void Run()
    {
        PlayEnterStateVoice(pursueEnterState, enemyData.angryClipContainer);
        PlayEnterStateVoice(chaseEnterState, enemyData.chaseClipContainer);
        PlayEnterStateVoice(attackEnterFilter, enemyData.attackClipContainer);
        PlayEnterStateVoice(damageEnterFilter, enemyData.damageClipContainer);
        PlayEnterStateVoice(deathEnterFilter, enemyData.deathClipContainer);
    }

    private void PlayEnterStateVoice(EcsFilter filter, AudioClipContainer clipContainer)
    {
        foreach (var flt in filter)
        {
            ref var enemyEntity = ref filter.GetEntity(flt);
            if (enemyEntity.Has<EnemyComponent>() && clipContainer.HasClip)
            {
                ref var enemyComponent = ref enemyEntity.Get<EnemyComponent>();
                enemyComponent.topAudioSource.PlayOneShot(clipContainer.GetClip);
            }
        }
    }
}
