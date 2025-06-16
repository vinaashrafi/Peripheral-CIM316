using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Slider healthSlider; // Assign this in the inspector
    public TextMeshProUGUI healthText; // Optional: if you want to show text too

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} took {amount} damage!");

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"Health: {Mathf.RoundToInt(currentHealth)}";
        }
    }

    // private void Die()
    // {
    //     Debug.Log($"{gameObject.name} has died.");
    //     // Optional: replace with ragdoll, animation, respawn, etc.
    //     // Destroy(gameObject);
    // }

    private void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log($"{gameObject.name} (Enemy) has died.");
            // Optional: replace with ragdoll, animation, respawn, etc.
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"{gameObject.name} has died.");
        }
    }
    // Optional: For healing if needed later
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }
}