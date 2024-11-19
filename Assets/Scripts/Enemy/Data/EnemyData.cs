using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "MyAssets/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float detectionRadius = 20f;
    public float detectionAngle = 80f;
    public LayerMask obstaclesLayer;
    [Header("Attack Animations")]
    public string[] attackAnimations;

    [Header("State Data:")]
    public EnemyStateData idleState;
    public EnemyStateData walkState;
    public EnemyStateData pursueState;
    public EnemyStateData chaseState;
    public EnemyStateData attackState;
}
