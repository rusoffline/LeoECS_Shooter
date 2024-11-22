using UnityEngine;

[System.Serializable]
public class EnemyStateData
{
    [Header("---StateData---")]
    public float agentSpeed;
    public float detectionDistance;
    public float duration;
    [SerializeField] private string[] animationClips;
    public int animationLayer;
    public bool hasAnimation => animationClips.Length > 0;
    public string AnimationName => (animationClips.Length == 0) ? animationClips[0] : animationClips[Random.Range(0, animationClips.Length)];
}