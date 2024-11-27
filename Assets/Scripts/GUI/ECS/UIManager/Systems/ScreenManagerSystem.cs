using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManagerSystem : IEcsRunSystem
{
    private EcsFilter<GameMode> gameModeFilter;
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<GameOverComponent> gameOverFilter;

    private UIManager uiManager;
    private GameModeService modeService;

    public void Run()
    {
        foreach (var gmd in gameModeFilter)
        {
            ref var gameEntity = ref gameModeFilter.GetEntity(gmd);

            foreach (var gvr in gameOverFilter)
            {
                ref var gameOver = ref gameOverFilter.Get1(gvr);
                gameOver.delay -= Time.deltaTime;
                if (gameOver.delay <= 0)
                {
                    Debug.Log(gameOver.isWin ? "Game Over Player is Win" : "Game Over Player is Lost");
                    ShowGameOoverScreen(ref gameEntity, ref gameOver);
                    ref var entity = ref gameOverFilter.GetEntity(gvr);
                    entity.Del<GameOverComponent>();
                }
            }

            foreach (var inp in inputFilter)
            {
                ref var input = ref inputFilter.Get1(inp);

                if (input.menu)
                {
                    if (gameEntity.Has<MenuMode>())
                    {
                        //hide menu
                        HideMenuScreen(ref gameEntity);
                        continue;
                    }
                    else
                    {
                        //show menu
                        ShowMenuScreen(ref gameEntity);
                    }
                    continue;
                }
                if (input.inventory)
                {
                    if (gameEntity.Has<InventoryMode>())
                    {
                        //hide inventory
                        HideInventoryScreen(ref gameEntity);
                        continue;
                    }
                    if (gameEntity.Has<PlayMode>())
                    {
                        //show inventory
                        ShowInventoryScreen(ref gameEntity);
                        continue;
                    }
                }
                if (input.back)
                {
                    if (gameEntity.Has<InventoryMode>())
                    {
                        //try hide invenotry
                        HideInventoryScreen(ref gameEntity);
                        continue;
                    }
                    if (gameEntity.Has<MenuMode>())
                    {
                        //hide menu
                        HideMenuScreen(ref gameEntity);
                        continue;
                    }
                }
            }
        }
    }
    private void ShowMenuScreen(ref EcsEntity gameEntity)
    {
        uiManager.menuScreen.ShowScreen();
        CheckModeStatus(ref gameEntity);
    }
    private void HideMenuScreen(ref EcsEntity gameEntity)
    {
        uiManager.menuScreen.TryHideScreen();
        CheckModeStatus(ref gameEntity);
    }
    private void ShowInventoryScreen(ref EcsEntity gameEntity)
    {
        uiManager.inventoryScreen.ShowScreen();
        gameEntity.Get<InventorySyncEvent>();
        CheckModeStatus(ref gameEntity);
    }

    private void HideInventoryScreen(ref EcsEntity gameEntity)
    {
        bool isSuccess = uiManager.inventoryScreen.TryHideScreen();
        if (isSuccess)
        {
            CheckModeStatus(ref gameEntity);
        }
    }

    private void ShowGameOoverScreen(ref EcsEntity gameEntity, ref GameOverComponent gameOverEvent)
    {
        uiManager.gameOverScreen.ShowGameOver(gameOverEvent.isWin);
        CheckModeStatus(ref gameEntity);
    }

    private void CheckModeStatus(ref EcsEntity gameEntity)
    {
        if (uiManager.gameOverScreen.IsActive)
        {
            modeService.SetGameMode<GameOverMode>(ref gameEntity);
            return;
        }
        if (uiManager.menuScreen.IsActive)
        {
            modeService.SetGameMode<MenuMode>(ref gameEntity);
            return;
        }
        if (uiManager.inventoryScreen.IsActive)
        {
            modeService.SetGameMode<InventoryMode>(ref gameEntity);
            return;
        }
        modeService.SetGameMode<PlayMode>(ref gameEntity);
    }
}
