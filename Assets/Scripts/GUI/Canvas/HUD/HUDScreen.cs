using UnityEngine;

public class HUDScreen : MonoBehaviour
{
    public AmmoCounter ammoCounter;
    public HealthBar healthBar;
    
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

