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

    public void ApplyDamage(int damage)
    {
        if (owner.entity != EcsEntity.Null)
        {
            owner.entity.Replace(new DamageEvent(damage));
        }
    }
}