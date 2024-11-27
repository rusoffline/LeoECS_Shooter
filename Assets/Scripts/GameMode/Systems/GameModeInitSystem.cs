using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeInitSystem : IEcsInitSystem
{
    private EcsWorld world;
    private GameModeService modeService;

    public void Init()
    {
        var entity = world.NewEntity();
        entity.Get<GameMode>();
        modeService.SetGameMode<PlayMode>(ref entity);
    }
}