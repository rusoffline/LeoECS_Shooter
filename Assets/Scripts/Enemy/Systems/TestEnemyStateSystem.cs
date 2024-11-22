using Leopotam.Ecs;
using static UnityEngine.EventSystems.EventTrigger;

public class TestEnemyStateSystem : IEcsRunSystem
{
    private EcsFilter<EnemyComponent, IdleState, EnterState> idleEnterState;
    private EcsFilter<EnemyComponent, PursueState, EnterState> pursueEnterState;
    private EcsFilter<EnemyComponent, ChaseState, EnterState> chaseEnterState;
    private EcsFilter<EnemyComponent, WalkState, EnterState> walkEnterState;
    private EcsFilter<EnemyComponent, AttackState, EnterState> attackEnterFilter;
    private EcsFilter<EnemyComponent, DamageState, EnterState> damageEnterFilter;
    private EcsFilter<EnemyComponent, DeathState, EnterState> deathEnterFilter;

    private EnemyData enemyData;

    public void Run()
    {
        EnterState(idleEnterState, enemyData.idleState);
        EnterState(pursueEnterState, enemyData.pursueState);
        EnterState(chaseEnterState, enemyData.chaseState);
        EnterState(walkEnterState, enemyData.walkState);
        EnterState(attackEnterFilter, enemyData.attackState);
        EnterState(damageEnterFilter, enemyData.damageState);
        EnterState(deathEnterFilter, enemyData.deathState);
    }

    private void EnterState(EcsFilter filter, EnemyStateData stateData)
    {
        foreach (var flt in filter)
        {
            ref var entity = ref filter.GetEntity(flt);
            ref var enemyComponent = ref entity.Get<EnemyComponent>();
            if (entity.Has<DeathState>())
            {
                enemyComponent.agent.enabled = false;
                enemyComponent.obstacleCollider.enabled = false;
            }
            else
            {
                enemyComponent.agent.isStopped = stateData.agentSpeed == 0;
                enemyComponent.agent.speed = stateData.agentSpeed;
            }
            if (stateData.hasAnimation)
            {
                enemyComponent.animator.CrossFade(stateData.AnimationName, .15f, stateData.animationLayer);
            }
        }
    }
}

