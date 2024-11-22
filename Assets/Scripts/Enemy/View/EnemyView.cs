using UnityEngine;
using UnityEngine.AI;

public class EnemyView : EntityOwner
{
    public Animator animator;
    public NavMeshAgent agent;
    public CapsuleCollider obstacleCollider;
    [Header("AudioSource:")]
    public AudioSource topAudioSource;
    public AudioSource bottomAudioSource;

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
}