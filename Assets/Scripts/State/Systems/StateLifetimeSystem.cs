using Leopotam.Ecs;
using UnityEngine;

public class StateLifetimeSystem:IEcsRunSystem
{
    private EcsFilter<StateLifetime> lifetimeFilter;
    private StateService stateService;

    public void Run()
    {
        foreach(var lft in lifetimeFilter)
        {
            ref var lifetim = ref lifetimeFilter.Get1(lft);
            lifetim.lifetime -= Time.deltaTime;

            if(lifetim.lifetime<=0)
            {
                ref var entity = ref lifetimeFilter.GetEntity(lft);
                stateService.RemoveAllState(ref entity);
            }
        }
    }
}