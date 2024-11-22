using Leopotam.Ecs;

public class PlayerHealthBarSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<HealthUpdateEvent> updateHealthFilter;
    private EcsFilter<PlayerComponent, HealthComponent> playerFilter;
    private PlayerData playerData;
    private UIManager uiManager;

    public void Init()
    {
        foreach(var plr in playerFilter)
        {
            ref var healthComponent = ref playerFilter.Get2(plr);
            UpdateHealthBar(healthComponent);
        }
    }

    public void Run()
    {
        foreach(var upd in updateHealthFilter)
        {
            foreach(var plr in playerFilter)
            {
                ref var healthComponent = ref playerFilter.Get2(plr);
                UpdateHealthBar(healthComponent);
            }
        }
    }

    private void UpdateHealthBar(HealthComponent healthComponent)
    {
        uiManager.hudScreen.healthBar.UpdateHealth(healthComponent.currentHealth, playerData.maxHealth);
    }
}
