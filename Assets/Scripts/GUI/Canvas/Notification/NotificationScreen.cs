using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationScreen : MonoBehaviour, IScreen
{

    public PickupNotification pickupNotification;
    public InteractionNotification interactionNotification;

    public bool TryClose()
    {
        return false;
    }

}
