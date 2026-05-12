using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManagerScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private Button playBtn;
    private Button settingsBtn;
    private Button exitBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playBtn = uiDoc.rootVisualElement.Q<Button>("PlayButton");
        settingsBtn = uiDoc.rootVisualElement.Q<Button>("SettingsButton");
        exitBtn = uiDoc.rootVisualElement.Q<Button>("ExitButton");
        playBtn.clicked += PlayGame;
        settingsBtn.clicked += SettingsGame;
        exitBtn.clicked += ExitGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void SettingsGame()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
