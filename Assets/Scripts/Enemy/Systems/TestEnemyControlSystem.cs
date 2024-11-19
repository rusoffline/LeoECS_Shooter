using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyControlSystem : IEcsRunSystem
{
    private EcsFilter<EnemyComponent, EnemyDetectionComponent>
        .Exclude<StateLifetime>
        .Exclude<AttackState>
        .Exclude<DamageState>
        enemyFilter;
    private StateService stateService;
    private EnemyData enemyData;

    public void Run()
    {
        foreach (var enm in enemyFilter)
        {
            ref var enemyEntity = ref enemyFilter.GetEntity(enm);
            ref var enemyComponent = ref enemyFilter.Get1(enm);
            ref var detectionComponent = ref enemyFilter.Get2(enm);

            var agent = enemyComponent.agent;
            var animator = enemyComponent.animator;
            var distance = detectionComponent.distanceToPlayer;

            if (detectionComponent.isPlayerDetected)
            {
                agent.SetDestination(detectionComponent.playerPosition);

                if (distance > enemyData.chaseState.detectionDistance)
                {
                    stateService.SwitchState<PursueState>(ref enemyEntity);
                }
                else if (distance > enemyData.attackState.detectionDistance)
                {
                    stateService.SwitchState<ChaseState>(ref enemyEntity);
                }
                else
                {
                    stateService.SwitchState<AttackState>(ref enemyEntity, 1f);
                }
            }
            else
            {
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    stateService.SwitchState<WalkState>(ref enemyEntity);
                }
                else
                {
                    stateService.SwitchState<IdleState>(ref enemyEntity);
                }
            }
        }
    }
}

