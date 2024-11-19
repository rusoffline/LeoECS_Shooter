using Leopotam.Ecs;

public class WeaponInputSystem : IEcsRunSystem
{
    private EcsFilter<HasWeapon> weaponFilter;
    private EcsFilter<InputComponent> inputFilter;

    public void Run()
    {
        foreach (var wpn in weaponFilter)
        {
            ref var has = ref weaponFilter.Get1(wpn);

            foreach (var inp in inputFilter)
            {
                ref var input = ref inputFilter.Get1(inp);
                if (input.fire && input.aim)
                {
                    has.weaponEntity.Get<TryFire>();
                }
                if (input.autoFire && input.aim)
                {
                    has.weaponEntity.Get<TryAutoFire>();
                }
                if (input.reload)
                {
                    has.weaponEntity.Get<TryReload>();
                }

            }
        }
    }
}
