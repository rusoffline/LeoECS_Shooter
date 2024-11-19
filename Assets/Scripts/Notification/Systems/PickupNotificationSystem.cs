using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNotificationSystem : IEcsRunSystem
{
    private EcsFilter<PickupNotifEvent> notifFilter;
    private UIManager uiManager;

    public void Run()
    {
        foreach (var ntf in notifFilter)
        {
            ref var notif = ref notifFilter.Get1(ntf);

            uiManager.notificationScreen.pickupNotification.ShowNotification(notif.itemName, notif.count);
        }
    }
}
