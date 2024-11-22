using Leopotam.Ecs;
using UnityEngine;

public class EnemyHitAudioSystem:IEcsRunSystem
{
    private EcsFilter<EnemyComponent, ImpactEvent> impactFilter;
    private EnemyData enemyData;

    public void Run()
    {
        foreach(var imp in impactFilter)
        {
            ref var impactEvent = ref impactFilter.Get2(imp);
            AudioSource.PlayClipAtPoint(enemyData.attackHitClipContainer.GetClip, impactEvent.impactPoint);
        }
    }
}