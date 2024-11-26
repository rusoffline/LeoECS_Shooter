using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameOverTrigger : EntityBridge
{
    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Trigger");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger!");
        entity.Replace(new GameOverEvent(true));
    }
}
