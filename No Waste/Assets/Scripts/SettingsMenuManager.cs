using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SettingsMenuManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private Button returnBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        returnBtn = uiDoc.rootVisualElement.Q<Button>("ReturnButton");
        returnBtn.clicked += ReturnGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
