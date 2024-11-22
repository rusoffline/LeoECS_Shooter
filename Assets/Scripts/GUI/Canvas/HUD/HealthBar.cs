using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text healthText;

    public void Start()
    {
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<Slider>();
        }
        if (healthText == null)
        {
            healthText = GetComponentInChildren<TMP_Text>();
        }
    }

    public void UpdateHealth(float current, float maxHealth)
    {
        Debug.Log($"HealthBar. UpdateHealth(current = {current}, maxHelath = {maxHealth})");
        healthBar.value = current / maxHealth;
        if (healthText != null)
        {
            healthText.text = (current / maxHealth * 100).ToString();
        }
    }
}

