using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(EntityOwner))]
public class AttackEventHandler : MonoBehaviour
{
    [SerializeField] private EntityOwner owner;

    private void OnValidate()
    {
        owner = GetComponent<EntityOwner>();
    }

    public void OnAttackTrigger()
    {
        if (owner.entity != EcsEntity.Null)
        {
            owner.entity.Get<AttackEvent>();
        }
    }
}
