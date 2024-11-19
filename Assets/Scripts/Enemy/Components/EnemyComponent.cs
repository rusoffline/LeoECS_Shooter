using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct EnemyComponent
{
    public Transform transform;
    public Transform headTransform;
    public Animator animator;
    public NavMeshAgent agent;
}
