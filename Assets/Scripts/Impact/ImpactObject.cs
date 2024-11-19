using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactObject : PoolableObject
{
    public ParticleSystem particle;
    public override void OnSpawn(string tagName)
    {
        base.OnSpawn(tagName);
        Invoke("ReturnToPool", 5f);
        particle.Emit(1);
    }
}
