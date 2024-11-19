using UnityEngine;

[System.Serializable]
public class EnemyStateData
{
    public float agentSpeed;
    [SerializeField] private string[] animationClips;
    public float detectionDistance;
    public float duration;
    public string AnimationName
    {
        get
        {
            if (animationClips.Length == 0)
            {
                return animationClips[0];
            }
            else
            {
                return animationClips[Random.Range(0, animationClips.Length)];
            }
        }
    }
}