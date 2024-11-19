using Leopotam.Ecs;
using UnityEngine;

public class EnemyInitSystem : IEcsInitSystem
{
    private EcsWorld world;

    public void Init()
    {
        var enemies = GameObject.FindObjectsOfType<EnemyView>();
        foreach (var enemy in enemies)
        {
            var enemyEntity = world.NewEntity();
            enemy.entity = enemyEntity;
            Debug.Log(enemy.name);

            var enemyComponent = new EnemyComponent();

            enemyComponent.transform = enemy.transform;
            enemyComponent.animator = enemy.animator;
            enemyComponent.headTransform = enemyComponent.animator.GetBoneTransform(HumanBodyBones.Head);
            enemyComponent.agent = enemy.agent;

            enemyEntity.Replace(enemyComponent);
        }
    }
}
