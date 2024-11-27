using Leopotam.Ecs;
using UnityEngine;

public class PlayerDeathSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, DeathState> playerDeathFilter;
    private EcsFilter<GameMode> gameModeFilter;

    public void Run()
    {
        foreach (var dth in playerDeathFilter)
        {
            ref var playerEntity = ref playerDeathFilter.GetEntity(dth);
            playerEntity.Destroy();

            foreach (var gmd in gameModeFilter)
            {
                ref var gameEntity = ref gameModeFilter.GetEntity(gmd);
                Debug.Log("Player is Dead");
                gameEntity.Replace(new GameOverComponent(false, 3f));
            }
        }
    }
}
