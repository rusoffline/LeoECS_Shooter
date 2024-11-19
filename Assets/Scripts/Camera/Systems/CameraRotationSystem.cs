using Leopotam.Ecs;
using UnityEngine;

public class CameraRotationSystem : IEcsRunSystem
{
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<VirtualCameraComponent> virtualCameraFilter;

    private PlayerData playerData;

    public void Run()
    {
        foreach (var inp in inputFilter)
        {
            ref var input = ref inputFilter.Get1(inp);

            foreach (var vrt in virtualCameraFilter)
            {
                ref var camera = ref virtualCameraFilter.Get1(vrt);


                camera.rotation.y += input.lookInput.x * playerData.mouseSensitivity * Time.fixedDeltaTime;
                camera.rotation.x -= input.lookInput.y * playerData.mouseSensitivity * Time.fixedDeltaTime;

                camera.rotation.x = Mathf.Clamp(camera.rotation.x, -playerData.cameraClamp, playerData.cameraClamp);

                camera.target.rotation = Quaternion.Euler(camera.rotation);
            }
        }
    }
}