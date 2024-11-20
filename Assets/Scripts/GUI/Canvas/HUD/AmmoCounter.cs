using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    public TMP_Text counterText;

    public void UpdateAmmo(int current, int total)
    {
        gameObject.SetActive(true);
        counterText.text = $"{current} | {total}";
    }
    public void HideAmmo()
    {
        gameObject.SetActive(false);
    }
}

