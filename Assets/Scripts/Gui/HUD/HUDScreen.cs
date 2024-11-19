using UnityEngine;

public class HUDScreen : MonoBehaviour, IScreen
{
    public AmmoCounter ammoCounter;

    public bool TryClose()
    {
        return false;
    }
}

