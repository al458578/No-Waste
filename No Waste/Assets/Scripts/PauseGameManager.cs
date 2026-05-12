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

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions.FindAction("Pause");
    }

    void Start()
    {

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
        if (isPaused)
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            camara.SetActive(false);
            mainCamera.SetActive(false);
        }
        else
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
            camara.SetActive(true);
            mainCamera.SetActive(true);
        }    
    }
}
