using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction pauseAction;
    public GameObject camara;
    public GameObject mainCamera;
    private bool isPaused = false;

    [SerializeField] private UIDocument uiDoc;
    private VisualElement marco;
    private VisualElement timer;
    private VisualElement points;
    private Label scoreText;
    private Label timeText;
    private ProgressBar healthBar;

    void Awake()
    {//Asignamos InputAction
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions.FindAction("Pause"); //Pausar el juego con la tecla: Esc
    }

    void Start()
    {//Asignar UIElements a las variables
        marco = uiDoc.rootVisualElement.Q<VisualElement>("Marco");
        scoreText = uiDoc.rootVisualElement.Q<Label>("ScoreLabel");
        timeText = uiDoc.rootVisualElement.Q<Label>("TimeLabel");
        healthBar = uiDoc.rootVisualElement.Q<ProgressBar>("HealthBar");
        timer = uiDoc.rootVisualElement.Q<VisualElement>("VisualTime");
        points = uiDoc.rootVisualElement.Q<VisualElement>("VisualPoints");
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        pauseAction.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        if (isPaused) //Ocultar los elementos de la Pantalla Game
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive); //Superponer la escena de Pausa
            camara.SetActive(false);
            mainCamera.SetActive(false);
            marco.style.display = DisplayStyle.None;
            scoreText.style.display = DisplayStyle.None;
            timeText.style.display = DisplayStyle.None;
            healthBar.style.display = DisplayStyle.None;
            timer.style.display = DisplayStyle.None;
            points.style.display = DisplayStyle.None;
        }

        else
        { //Activar elementos de la Pantalla Game
            SceneManager.UnloadSceneAsync("PauseMenu"); //Quitar la escena de Pausa
            camara.SetActive(true);
            mainCamera.SetActive(true);
            marco.style.display = DisplayStyle.Flex;
            scoreText.style.display = DisplayStyle.Flex;
            timeText.style.display = DisplayStyle.Flex;
            healthBar.style.display = DisplayStyle.Flex;
            timer.style.display = DisplayStyle.Flex;
            points.style.display = DisplayStyle.Flex;
        }  
    }
}
