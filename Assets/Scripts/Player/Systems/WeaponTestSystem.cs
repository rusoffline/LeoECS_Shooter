using Leopotam.Ecs;

public class WeaponTestSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<PlayerComponent> playerFilter;
    private EcsFilter<PlayerComponent, CurrentWeaponIndex> playerWeaponFilter;
    private EcsWorld world;
    private StateService stateService;

    public void Init()
    {
        foreach (var plr in playerFilter)
        {
            playerFilter.GetEntity(plr).Get<CurrentWeaponIndex>();
        }
    }

    public void Run()
    {
        foreach (var inp in inputFilter)
        {
            ref var input = ref inputFilter.Get1(inp);
            foreach (var plr in playerFilter)
            {
                ref var playerEntity = ref playerFilter.GetEntity(plr);
                ref var player = ref playerWeaponFilter.Get1(plr);
                ref var weapon = ref playerWeaponFilter.Get2(plr);

                if (weapon.index != input.weaponIndex)
                {
                    weapon.index = input.weaponIndex;
                    player.animator.SetInteger("WeaponIndex", weapon.index);
                    player.animator.CrossFade("Weapon_Grab", .05f, 1);
                }
                if (input.reload)
                {
                    player.animator.CrossFade("Weapon_Reload", .05f, 1);
                }
                if (input.fire && input.aim)
                {
                    if (weapon.index == 0)
                    {
                        //player.animator.CrossFade("Light_Melee_Attack", .25f, 3);
                        stateService.SwitchState<AttackState>(ref playerEntity);
                    }
                    else
                    {
                        player.animator.CrossFade("Shoot", .05f, 2);
                    }
                }
            }
        }
    }
}
