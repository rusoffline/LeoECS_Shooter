using Leopotam.Ecs;
using UnityEngine;

public class PlayerRotationSystem : IEcsRunSystem
{
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<PlayerComponent, VirtualCameraComponent> playerFilter;
    private PlayerData playerData;

    public void Run()
    {
        foreach (var inp in inputFilter)
        {
            ref var input = ref inputFilter.Get1(inp);

            foreach (var plr in playerFilter)
            {
                ref var player = ref playerFilter.Get1(plr);
                ref var virtualCamera = ref playerFilter.Get2(plr);

                var targetDirection = Vector3.zero;
                if (input.aim)
                {
                    targetDirection = virtualCamera.mainCamera.transform.forward;
                    targetDirection.y = 0f;
                    targetDirection.Normalize();

                    RotatePlayer(player.transform, targetDirection);
                }
                else
                if (input.moveInput.magnitude > 0f)
                {
                    Vector3 cameraForward = virtualCamera.mainCamera.transform.forward;
                    Vector3 cameraRight = virtualCamera.mainCamera.transform.right;

                    cameraForward.y = 0;
                    cameraRight.y = 0;

                    cameraForward.Normalize();
                    cameraRight.Normalize();

                    targetDirection = cameraForward * input.moveInput.y + cameraRight * input.moveInput.x;

                    RotatePlayer(player.transform, targetDirection);
                }
            }
        }
    }

    private void RotatePlayer(Transform player, Vector3 targetDirection)
    {
        var targetRotation = Quaternion.LookRotation(targetDirection);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, playerData.playerRotatoinSpeed * Time.deltaTime);

    }
}
