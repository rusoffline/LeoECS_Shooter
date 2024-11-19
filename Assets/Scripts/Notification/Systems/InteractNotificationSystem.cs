using Leopotam.Ecs;
using UnityEngine;

public class InteractNotificationSystem : IEcsRunSystem
{
    private EcsFilter<IteractNotifEvent> notifFilter;
    private UIManager uiManager;

    public void Run()
    {
        foreach (var ntf in notifFilter)
        {
            Debug.Log("InteractNotificationSystem. IteractNotifEvent");
            ref var notif = ref notifFilter.Get1(ntf);
            if (!string.IsNullOrWhiteSpace(notif.message))
            {
                Debug.Log($"uiManager.notificationScreen.interactionNotification.ShowNotification({notif.message})");
                uiManager.notificationScreen.interactionNotification.ShowNotification(notif.message);
            }
            else
            {
                Debug.Log($"message is null");
            }
        }
    }
}
