using Leopotam.Ecs;
using UnityEngine;

public class EnemyHitSystem : IEcsRunSystem
{
    private EcsFilter<EnemyComponent, AttackEvent> attackFilter;
    private EcsFilter<PlayerComponent> playerFilter;
    private EnemyData enemyData;

    public void Run()
    {
        foreach (var atk in attackFilter)
        {
            ref var enemyEntity = ref attackFilter.GetEntity(atk);
            ref var enemyComponent = ref attackFilter.Get1(atk);

            foreach (var plr in playerFilter)
            {
                ref var playerComponent = ref playerFilter.Get1(plr);
                var distance = Vector3.Distance(enemyComponent.transform.position, playerComponent.transform.position);
                if (distance <= enemyData.attackState.detectionDistance)
                {
                    ref var playerEntity = ref playerFilter.GetEntity(plr);
                    ref var damageEvent = ref playerEntity.Get<DamageEvent>();
                    damageEvent.damage = enemyData.damage;
                    damageEvent.source = enemyComponent.transform.position;

                    var impactEvent = new ImpactEvent();
                    impactEvent.impactPoint = playerComponent.transform.position + Vector3.up * 1.5f;
                    enemyEntity.Replace(impactEvent);
                }
            }
        }
    }
}
