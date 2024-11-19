using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotMenuScreen : MonoBehaviour, IScreen
{
    public Button slot01Button;
    public Button slot02Button;
    public Button slot03Button;
    public Button slot04Button;

    public void OpenMenu(InventoryItem item)
    {
        gameObject.SetActive(true);
        transform.position = Input.mousePosition;
    }

    public bool TryClose()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}
