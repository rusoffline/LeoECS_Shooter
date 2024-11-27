using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "MyAssets/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float detectionRadius = 20f;
    public float detectionAngle = 80f;
    public LayerMask obstaclesLayer;

    public int damage = 10;
    [Header("State Data:")]
    public EnemyStateData idleState;
    public EnemyStateData walkState;
    public EnemyStateData pursueState;
    public EnemyStateData chaseState;
    public EnemyStateData attackState;
    public EnemyStateData damageState;
    public EnemyStateData deathState;
    [Header("Voice Clips:")]
    public AudioClipContainer angryClipContainer;
    public AudioClipContainer attackClipContainer;
    public AudioClipContainer damageClipContainer;
    public AudioClipContainer screamClipContainer;
    public AudioClipContainer chaseClipContainer;
    public AudioClipContainer deathClipContainer;
    public AudioClipContainer attackHitClipContainer;
}
