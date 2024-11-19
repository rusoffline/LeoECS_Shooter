using Leopotam.Ecs;

public class InventoryUISystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<GameMode> modeFilter;
    private UIManager uiManager;
    private GameModeService modeService;

    public void Init()
    {
        uiManager.inventoryScreen.gameObject.SetActive(false);
    }

    public void Run()
    {
        foreach (var inp in inputFilter)
        {
            ref var input = ref inputFilter.Get1(inp);
            if (input.inventory && !uiManager.inventoryScreen.gameObject.activeSelf)
            {
                ref var inventoryEntity = ref inputFilter.GetEntity(inp);
                ShowInventoryScreen(ref inventoryEntity);
                continue;
            }
            if ((input.inventory || input.back) && uiManager.inventoryScreen.gameObject.activeSelf)
            {
                HideInventoryScreen();
                continue;
            }
        }
    }

    private void ShowInventoryScreen(ref EcsEntity inventoryEntity)
    {
        uiManager.inventoryScreen.gameObject.SetActive(true);
        inventoryEntity.Get<InventorySyncEvent>();

        foreach(var mod in modeFilter)
        {
            ref var modeEntity = ref modeFilter.GetEntity(mod);
            modeService.SetGameMode<MenuMode>(ref modeEntity);
        }
    }

    private void HideInventoryScreen()
    {
        uiManager.inventoryScreen.gameObject.SetActive(false);
        foreach (var mod in modeFilter)
        {
            ref var modeEntity = ref modeFilter.GetEntity(mod);
            modeService.SetGameMode<PlayMode>(ref modeEntity);
        }
    }
}
