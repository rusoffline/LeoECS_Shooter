using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : IEcsRunSystem
{
    private EcsFilter<HealthComponent, DamageEvent> damageFilter;

    public void Run()
    {
        foreach(var dmg in damageFilter)
        {
            ref var health = ref damageFilter.Get1(dmg);
            ref var damage = ref damageFilter.Get2(dmg);

            health.currentHealth -= damage.damage;
            health.currentHealth = Mathf.Clamp(health.currentHealth, 0, int.MaxValue);
        }
    }
}
