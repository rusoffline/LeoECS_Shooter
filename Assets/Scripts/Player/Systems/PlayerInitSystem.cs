using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld world;
    private PlayerSpawner playerSpawner;

    public void Init()
    {
        var playerEntity = world.NewEntity();
        ref var playerComponent = ref playerEntity.Get<PlayerComponent>();

        PlayerView playerViewInit = GameObject.Instantiate(playerSpawner.playerPrefab, playerSpawner.spawnPoint.position, playerSpawner.spawnPoint.rotation);

        playerViewInit.entity = playerEntity;

        playerComponent.transform = playerViewInit.transform;
        playerComponent.rigidbody = playerViewInit.rigidBody;
        playerComponent.animator = playerViewInit.animator;
        playerComponent.aimTarget = playerViewInit.aimTarget;
        playerComponent.weaponHand = playerViewInit.weaponHand;
        playerComponent.headTransform = playerComponent.animator.GetBoneTransform(HumanBodyBones.Head);


        var virtualCamera = new VirtualCameraComponent();
        virtualCamera.mainCamera = Camera.main;
        virtualCamera.virtualCamera = playerViewInit.virtualCamera;
        virtualCamera.target = playerViewInit.cameraTarget;
        playerEntity.Replace(virtualCamera);

        playerEntity.Get<GroundedComponent>();
    }
}
