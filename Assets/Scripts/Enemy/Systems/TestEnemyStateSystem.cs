using Leopotam.Ecs;
using UnityEngine;
using Vertx.Debugging;

public class TestEnemyStateSystem : IEcsRunSystem
{
    private EcsFilter<EnemyComponent, IdleState, EnterState> idleEnterState;
    private EcsFilter<EnemyComponent, PursueState, EnterState> pursueEnterState;
    private EcsFilter<EnemyComponent, ChaseState, EnterState> chaseEnterState;
    private EcsFilter<EnemyComponent, WalkState, EnterState> walkEnterState;
    private EcsFilter<EnemyComponent, AttackState, EnterState> attackEnterFilter;
    private EnemyData enemyData;

    public void Run()
    {

        foreach (var idl in idleEnterState)
        {
            ref var enemyComponent = ref idleEnterState.Get1(idl);
            UpdateEnemyState(enemyComponent, enemyData.idleState);
        }
        foreach (var prs in pursueEnterState)
        {
            ref var enemyComponent = ref pursueEnterState.Get1(prs);
            UpdateEnemyState(enemyComponent, enemyData.pursueState);
        }
        foreach (var chs in chaseEnterState)
        {
            ref var enemyComponent = ref chaseEnterState.Get1(chs);
            UpdateEnemyState(enemyComponent, enemyData.chaseState);
        }
        foreach (var wlk in walkEnterState)
        {
            ref var enemyComponent = ref walkEnterState.Get1(wlk);
            UpdateEnemyState(enemyComponent, enemyData.walkState);
        }
        foreach (var atk in attackEnterFilter)
        {
            ref var enemyComponent = ref attackEnterFilter.Get1(atk);
            UpdateEnemyState(enemyComponent, enemyData.attackState);
        }
    }

    private void UpdateEnemyState(EnemyComponent enemyComponent, EnemyStateData stateData)
    {
        enemyComponent.agent.isStopped = stateData.agentSpeed == 0;
        enemyComponent.agent.speed = stateData.agentSpeed;
        enemyComponent.animator.CrossFade(stateData.AnimationName, .15f);
    }
}

