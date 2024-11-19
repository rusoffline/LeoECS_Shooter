using Leopotam.Ecs;
using UnityEngine;
using Vertx.Debugging;

public class EnemyDetectionSystem : IEcsRunSystem
{
    private EcsFilter<EnemyComponent> enemyFilter;
    private EcsFilter<PlayerComponent> playerFilter;
    private EnemyData enemyData;

    public void Run()
    {
        foreach (var plr in playerFilter)
        {
            ref var playerComponent = ref playerFilter.Get1(plr);

            foreach (var enm in enemyFilter)
            {
                ref var enemyEntity = ref enemyFilter.GetEntity(enm);
                ref var enemyComponent = ref enemyFilter.Get1(enm);
                ref var detectionComponent = ref enemyEntity.Get<EnemyDetectionComponent>();

                if (CanSeePlayer(ref enemyComponent, ref detectionComponent, enemyComponent.headTransform.position, playerComponent.headTransform.position))
                {
                    detectionComponent.playerPosition = playerComponent.transform.position;
                    detectionComponent.isPlayerDetected = true;
                    DrawLine(enemyComponent.headTransform.position, playerComponent.headTransform.position, ref detectionComponent);
                    //DrawCone(enemyComponent.headTransform.position, enemyComponent.transform.forward, enemyData.detectionAngle, true);
                }
                else
                {
                    detectionComponent.isPlayerDetected = false;
                    DrawLine(enemyComponent.headTransform.position, playerComponent.headTransform.position, ref detectionComponent);
                    //DrawCone(enemyComponent.headTransform.position, enemyComponent.transform.forward, enemyData.detectionAngle, false);
                }
                DrawFOV(enemyComponent.headTransform.position, enemyComponent.transform.forward, enemyData.detectionAngle);
            }
        }
    }

    private bool CanSeePlayer(ref EnemyComponent enemyComponent, ref EnemyDetectionComponent detectionComponent, Vector3 enemyHeadPosition, Vector3 playerHeadPosition)
    {
        detectionComponent.directionToPlayer = playerHeadPosition - enemyHeadPosition;
        detectionComponent.distanceToPlayer = detectionComponent.directionToPlayer.magnitude;

        if (detectionComponent.distanceToPlayer > enemyData.detectionRadius)
            return false;

        float angle = Vector3.Angle(detectionComponent.directionToPlayer, enemyComponent.transform.forward);
        if (angle > enemyData.detectionAngle / 2f)
            return false;

        if (Physics.Raycast(enemyHeadPosition, detectionComponent.directionToPlayer.normalized, detectionComponent.distanceToPlayer, enemyData.obstaclesLayer))
        {
            return false;
        }

        return true;
    }

    private void DrawLine(Vector3 enemyHeadPosition, Vector3 playerHeadPosition, ref EnemyDetectionComponent detectionComponent)
    {
        bool isDetected = detectionComponent.isPlayerDetected;
        bool inRadius = detectionComponent.distanceToPlayer < enemyData.detectionRadius;
        Color lineColor = isDetected ? Color.red : inRadius ? Color.yellow : Color.green;
        Debug.DrawLine(enemyHeadPosition, playerHeadPosition, lineColor);
    }
    private void DrawFOV(Vector3 point, Vector3 direction, float angle)
    {
        DebugExtensions.DrawFOV(point, direction, angle);
    }
}
