using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrocityInitSystem : IEcsInitSystem
{
    private EcsWorld world;

    public void Init()
    {
        var electrocityEntity = world.NewEntity();
        electrocityEntity.Get<ElectricalPower>();

        var consumers = GameObject.FindObjectsOfType<ElectricalConsumer>();
        foreach (var consumer in consumers)
        {
            consumer.entity = electrocityEntity;
        }
    }
}
