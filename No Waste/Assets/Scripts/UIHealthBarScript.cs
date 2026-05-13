using UnityEngine;
using UnityEngine.UIElements;

public class UIHealthBarScript : MonoBehaviour
{
    [SerializeField] private Gradient healthGradient;
    private ProgressBar healthBar;

    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<ProgressBar>("HealthBar");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.highValue = maxHealth;
        healthBar.value = currentHealth;
        float pct = currentHealth / maxHealth;
        Color currentColor = healthGradient.Evaluate(pct);
        var barElement = healthBar.Q(className: "unity-progress-bar__progress");
        //Limpiar estilos previos
        barElement.style.backgroundColor = Color.clear;
        //Aplicamos nuevo color
        barElement.style.backgroundColor = new StyleColor(currentColor);
        healthBar.title = Mathf.RoundToInt(currentHealth).ToString();

    }

    public void OnEnable()
    {
        StartCoroutine(InitializeUI());
    }

    private async Awaitable InitializeUI()
    {
        await Awaitable.EndOfFrameAsync();
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(PlayerHealth.Instance.stats.currentHealth, PlayerHealth.Instance.stats.maxHealth);
        }
    }

    void OnDisable()
    {
        if (PlayerHealth.Instance != null)
            PlayerHealth.Instance.OnHealthChanged -= UpdateHealthBar;
    }
}
