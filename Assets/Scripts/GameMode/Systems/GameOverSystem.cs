using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem : IEcsRunSystem
{
    private EcsFilter<GameMode> gameModeFilter;
    private EcsFilter<GameOverEvent> gameOverFilter;
    private UIManager uiManager;
    private GameModeService gameModeService;

    public void Run()
    {
        foreach (var gvr in gameOverFilter)
        {
            ref var gameOver = ref gameOverFilter.Get1(gvr);

            foreach (var gmd in gameModeFilter)
            {
                ref var gameEntity = ref gameModeFilter.GetEntity(gmd);
                gameModeService.SetGameMode<GameOver>(ref gameEntity);

                if(gameOver.isWin)
                {
                    Debug.Log("Player is Win");
                }
                else
                {
                    Debug.Log("Player is Lose");
                }
                uiManager.gameOverScreen.ShowGameOver(gameOver.isWin);
            }
        }
    }
}
