using Leopotam.Ecs;
using UnityEngine;

public class PlayerAnimatorInputSystem : IEcsRunSystem
{
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<PlayerComponent, VirtualCameraComponent> playerFilter;

    public void Run()
    {
        foreach (var inp in inputFilter)
        {
            ref var input = ref inputFilter.Get1(inp);

            foreach (var plr in playerFilter)
            {
                ref var player = ref playerFilter.Get1(plr);
                ref var virtualCamera = ref playerFilter.Get2(plr);

                Vector3 cameraForward = virtualCamera.mainCamera.transform.forward;
                Vector3 cameraRight = virtualCamera.mainCamera.transform.right;

                cameraForward.y = 0;
                cameraRight.y = 0;

                cameraForward.Normalize();
                cameraRight.Normalize();

                Vector3 moveDirection = cameraForward * input.moveInput.y + cameraRight * input.moveInput.x;

                moveDirection = player.transform.InverseTransformDirection(moveDirection);

                player.animator.SetFloat("X", moveDirection.x);
                player.animator.SetFloat("Z", moveDirection.z);
                player.animator.SetBool("IsRun", input.run && !input.aim);
                player.animator.SetBool("IsAim", input.aim);
            }
        }
    }
}
