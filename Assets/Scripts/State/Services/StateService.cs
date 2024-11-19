using Leopotam.Ecs;
using UnityEngine;

public class StateService
{
    public void SwitchState<TState>(ref EcsEntity entity, float lifetime = 0) where TState : struct
    {
        if (entity.Has<TState>())
            return;

        RemoveAllState(ref entity);

        entity.Get<TState>();
        entity.Get<EnterState>();

        if(lifetime > 0)
        {
            entity.Replace(new StateLifetime(lifetime));
        }
    }
   
    public void RemoveAllState(ref EcsEntity entity)
    {
        entity.Del<StateLifetime>();
        entity.Del<IdleState>();
        entity.Del<WalkState>();
        entity.Del<FallState>();
        entity.Del<LocomotionState>();
        entity.Del<CrouchState>();
        entity.Del<AttackState>();
        entity.Del<PursueState>();
        entity.Del<ChaseState>();
    }
}
