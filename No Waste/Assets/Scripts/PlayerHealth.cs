using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public HealthData stats;
    public event Action<float, float> OnHealthChanged;
    public static PlayerHealth Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        stats.currentHealth = stats.maxHealth;
        OnHealthChanged?.Invoke(stats.currentHealth, stats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        stats.currentHealth -= amount;
        OnHealthChanged?.Invoke(stats.currentHealth, stats.maxHealth);
        if (stats.currentHealth <= 0)
        {
            stats.currentHealth = 0;
            //Pasar a la escena de Game Over
        }
    }
}
