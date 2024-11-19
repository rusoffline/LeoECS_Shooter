using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeService
{
    public void SetGameMode<TMode>(ref EcsEntity entity) where TMode : struct
    {
        if (entity.Has<TMode>())
            return;

        entity.Del<PlayMode>();
        entity.Del<MenuMode>();

        entity.Get<TMode>();

        if(entity.Has<PlayMode>())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
