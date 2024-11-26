using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeInitSystem : IEcsInitSystem
{
    private EcsWorld world;
    public void Init()
    {
        var bridgeds = GameObject.FindObjectsOfType<EntityBridge>();

        var bridgeEntity = world.NewEntity();
        bridgeEntity.Get<BridgeComponent>();

        foreach(var brg in bridgeds)
        {
            brg.entity = bridgeEntity;
        }
    }
}
public struct BridgeComponent : IEcsIgnoreInFilter { }
