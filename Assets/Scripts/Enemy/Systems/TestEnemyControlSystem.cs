using Leopotam.Ecs;
using UnityEngine;

public class TestEnemyControlSystem : IEcsRunSystem
{
    private EcsFilter<EnemyComponent, HealthComponent, DamageEvent> damageFilter;

    private EcsFilter<EnemyComponent, EnemyDetectionComponent>
        .Exclude<StateLifetime, DeathState>
        enemyFilter;

    private StateService stateService;
    private EnemyData enemyData;

    public void Run()
    {
        foreach (var dmg in damageFilter)
        {
            ref var enemyComponent = ref damageFilter.Get1(dmg);
            ref var enemyEntity = ref damageFilter.GetEntity(dmg);
            //test:
            ref var health = ref damageFilter.Get2(dmg);
            if (health.currentHealth > 0)
            {
                ref var damageEvent = ref damageFilter.Get3(dmg);
                enemyComponent.agent.SetDestination(damageEvent.source);
                stateService.SwitchState<DamageState>(ref enemyEntity, 1f);
            }
            else
            {
               stateService.SwitchState<DeathState>(ref enemyEntity);
            }
            //end test
        }

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
                    stateService.SwitchState<AttackState>(ref enemyEntity, 1.2f);
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


