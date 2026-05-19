using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OverMenuManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private Button replayBtn;
    private Button mainBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {//Asignar UIElements a sus variables
        replayBtn = uiDoc.rootVisualElement.Q<Button>("ReplayButton");
        mainBtn = uiDoc.rootVisualElement.Q<Button>("MainButton");

        //Asignar cambio de escena según la función del botón
        replayBtn.clicked += ReplayGame;
        mainBtn.clicked += MainGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void MainGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
