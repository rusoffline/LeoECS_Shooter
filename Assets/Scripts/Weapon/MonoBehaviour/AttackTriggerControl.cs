using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(EntityOwner))]
public class AttackTriggerControl : MonoBehaviour
{
    public EntityOwner owner;

    private void OnValidate()
    {
        owner = GetComponent<EntityOwner>();
    }

    public void OnAttackTrigger()
    {
        owner.entity.Get<AttackEvent>();
    }
}
