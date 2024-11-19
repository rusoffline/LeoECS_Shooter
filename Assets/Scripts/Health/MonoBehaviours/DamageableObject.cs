using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(EntityOwner))]
public class DamageableObject : MonoBehaviour
{
    private EntityOwner owner;

    private void OnValidate()
    {
        owner = GetComponent<EntityOwner>();
    }

    public void TakeDamage(int damage)
    {
        owner.entity.Replace(new DamageEvent(damage));
    }
}