using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalConsumer : EntityOwner
{
    [Header("Electrical Consumer:")]
    [SerializeField] private int requiredPower = 1;
    [SerializeField, TextArea(3, 5)] private string failureNotif = "Требуется электропитание";

    protected void TryUsePower(int requiredPower, UnityAction OnSuccess, UnityAction OnFailure)
    {
        //var tryUseEvent = new TryUseElectropowerEvent();
        //tryUseEvent.requiredPower = requiredPower;
        //tryUseEvent.OnSuccess = OnSuccess;
        //tryUseEvent.OnFailure = OnFailure;

        //entity.Replace(tryUseEvent);
    }

    protected bool TryUsePower()
    {
        ref var electricalPower = ref entity.Get<ElectricalPower>();
        if(requiredPower<=electricalPower.power)
        {
            return true;
        }

        entity.Replace(new InteractNotifEvent(failureNotif));

        return false;
    }

    protected void AddPower(int power)
    {
        //var addPowerEvent = new AddElectropowerEvent();
        //addPowerEvent.power = power;

        //entity.Replace(addPowerEvent);

        ref var electricalPower = ref entity.Get<ElectricalPower>();
        electricalPower.power += power;
    }

    protected void RemovePower(int power)
    {
        ref var electricalPower = ref entity.Get<ElectricalPower>();
        electricalPower.power -= power;
    }
}
