using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private Button mainBtn;
    private Button resumeBtn;
    private PauseGameManager pause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {//Asignar UIElements a las variables
        mainBtn = uiDoc.rootVisualElement.Q<Button>("MainButton");
        mainBtn.clicked += MainReturn;
        resumeBtn = uiDoc.rootVisualElement.Q<Button>("ResumeButton");
        resumeBtn.clicked += ReturnGame;
        pause = Object.FindFirstObjectByType<PauseGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnGame()
    {//Volver a la Pantalla Game
        if (pause != null)
        {
            pause.TogglePause(); 
        }
        
    }

    public void MainReturn()
    {//Ir al Main Menu
        SceneManager.LoadScene("MainMenu"); 
    }
}
