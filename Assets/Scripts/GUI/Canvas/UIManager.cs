using UnityEngine;

public class UIManager : MonoBehaviour, IScreen
{
    public InventoryScreen inventoryScreen;
    public HUDScreen hudScreen;
    public NotificationScreen notificationScreen;
    public GameOverScreen gameOverScreen;

    public bool TryClose()
    {
        return inventoryScreen.TryClose();
    }
}
