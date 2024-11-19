using UnityEngine;

public struct PlayerComponent
{
    public Transform transform;
    public Transform headTransform;
    public Rigidbody rigidbody;
    public Animator animator;
    public Vector3 position => transform.position;
    public Transform aimTarget;
    public Transform weaponHand;
}
