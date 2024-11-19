using UnityEngine;
using UnityEngine.AI;

public class EnemyView : EntityOwner
{
    public Animator animator;
    public NavMeshAgent agent;

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
}