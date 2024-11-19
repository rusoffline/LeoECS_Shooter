using Leopotam.Ecs;
using UnityEngine;

public class InputSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld world;
    private EcsFilter<InputComponent> inputFilter;

    public void Init()
    {
        var entity = world.NewEntity();
        entity.Get<InputComponent>();
    }

    public void Run()
    {
        foreach (var inp in inputFilter)
        {
            ref var input = ref inputFilter.Get1(inp);

            input.lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            input.moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            input.fire = Input.GetButtonDown("Fire1");
            input.autoFire = Input.GetButton("Fire1");
            input.reload = Input.GetKeyDown(KeyCode.R);
            input.aim = Input.GetButton("Fire2");
            input.back = Input.GetButtonDown("Fire2");
            input.run = Input.GetKey(KeyCode.LeftShift);
            input.jump = Input.GetButtonDown("Jump");
            input.menu = Input.GetKeyDown(KeyCode.Escape);
            input.inventory = Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab);

            if (Input.GetKeyDown(KeyCode.Alpha0)) input.weaponIndex = 0;
            else if(Input.GetKeyDown(KeyCode.Alpha1)) input.weaponIndex = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha2)) input.weaponIndex = 2;
            else if (Input.GetKeyDown(KeyCode.Alpha3)) input.weaponIndex = 3;
            else if (Input.GetKeyDown(KeyCode.Alpha4)) input.weaponIndex = 4;
            else if (Input.GetKeyDown(KeyCode.Alpha5)) input.weaponIndex = 5;
        }
    }
}
