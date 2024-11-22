using UnityEngine;

public class HUDScreen : MonoBehaviour, IScreen
{
    public AmmoCounter ammoCounter;
    public HealthBar healthBar;

    public bool TryClose()
    {
        return false;
    }
    
    private void Start()
    {
        if (ammoCounter == null)
        {
            ammoCounter = GetComponentInChildren<AmmoCounter>();
        }
        ammoCounter.HideAmmo();

        if(healthBar != null)
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
    }
}

