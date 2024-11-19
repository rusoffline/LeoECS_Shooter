using Leopotam.Ecs;
using UnityEngine;

public class InteractableInitSystem : IEcsInitSystem
{
    private EcsWorld world;

    public void Init()
    {
        var interactables = GameObject.FindObjectsOfType<BaseInteractable>();
        foreach(var interactable in interactables)
        {
            var entity = world.NewEntity();
            entity.Get<InteractableTag>();
            interactable.entity = entity;
        }
    }
}
