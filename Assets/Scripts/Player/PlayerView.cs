using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class PlayerView : EntityOwner
{
    public CapsuleCollider capsule;
    public Rigidbody rigidBody;
    public Animator animator;
    
    public CinemachineVirtualCamera virtualCamera;
    public Transform cameraTarget;

    public Transform aimTarget;
    public Transform weaponHand;

    public Rig weaponRig;

    private void OnValidate()
    {
        animator = gameObject.GetOrAddComponent<Animator>();
        rigidBody = gameObject.GetOrAddComponent<Rigidbody>();
        capsule = gameObject.GetOrAddComponent<CapsuleCollider>();
    }

    private void Start()
    {
        weaponRig.weight = 0f;
    }
}
