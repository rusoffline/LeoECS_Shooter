using Leopotam.Ecs;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityOwner))]
public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private EntityOwner owner;

    private void OnValidate()
    {
        owner = GetComponent<EntityOwner>();
    }

    public void ApplyDamage(DamageEvent damage)
    {
        if (owner.entity != EcsEntity.Null)
        {
            ref var damageEvent = ref owner.entity.Get<DamageEvent>();
            damageEvent.damage = damage.damage;
            damageEvent.source = damage.source;
        }
    }
}