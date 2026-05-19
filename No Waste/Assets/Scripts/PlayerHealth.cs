using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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
    {//Estabkecer vida al máximo
        stats.currentHealth = stats.maxHealth;
        OnHealthChanged?.Invoke(stats.currentHealth, stats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.currentHealth <= 0) //Si la vida se agota = GameOver
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    public void TakeDamage(int amount) //Recibir daño
    {
        stats.currentHealth -= amount;
        OnHealthChanged?.Invoke(stats.currentHealth, stats.maxHealth);
        if (stats.currentHealth <= 0)
        {
            stats.currentHealth = 0;
        }
    }

    public void TakeHealth(int amount) //Recuperar vida
    {
        stats.currentHealth += amount;
        OnHealthChanged?.Invoke(stats.currentHealth, stats.maxHealth);
    }
}
