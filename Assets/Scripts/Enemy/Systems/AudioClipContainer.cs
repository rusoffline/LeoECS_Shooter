using UnityEngine;

[System.Serializable]
public class AudioClipContainer
{
    [SerializeField] private AudioClip[] clips;
    public bool HasClip => clips != null && clips.Length > 0;
    public AudioClip GetClip => HasClip ? clips[Random.Range(0, clips.Length)] : null;
}
