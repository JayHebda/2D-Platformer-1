using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth = 100;

    [Header("Ustawienia Obra¿eñ")]
    public float invincibilityDuration = 0.2f; 

    private float currentHealth;
    private bool invincibility;

    public delegate void OnHealthChangedHandler(float newHealth, float amountChanged);
    public event OnHealthChangedHandler OnHealthChanged;

    public delegate void OnHealthInitializedHandler(float newHealth);
    public event OnHealthInitializedHandler OnHealthInitialized;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthInitialized?.Invoke(currentHealth);
    }

    public void ReceiveDamage(float amount)
    {
        if (!invincibility)
        {
            currentHealth -= amount;
            OnHealthChanged?.Invoke(currentHealth, amount);
            invincibility = true;

            StartCoroutine(ResetInvincibility(invincibilityDuration));
        }

        if (currentHealth <= 0)
        {
            PlayerPrefs.SetString("LastPlayedLevel", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("EndGameScene");
        }
    }

    IEnumerator ResetInvincibility(float resetTime)
    {
        yield return new WaitForSeconds(resetTime);
        invincibility = false;
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        OnHealthChanged?.Invoke(currentHealth, amount);
    }
}