using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Scriptable Objects/HealthData")]
public class HealthData : ScriptableObject //Establecer vida máxima y vida inicial
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    
}
