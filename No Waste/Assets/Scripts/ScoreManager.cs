using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private Button replayBtn;
    private Button mainBtn;
    private Label finalScore;

    private GameObject player;
    private PlayerCooking score;

    void Awake()
    { //Asignar UIElements a las variables
        player = GameObject.Find("Player");
        finalScore = uiDoc.rootVisualElement.Q<Label>("FinalScore");
        replayBtn = uiDoc.rootVisualElement.Q<Button>("ReplayButton");
        mainBtn = uiDoc.rootVisualElement.Q<Button>("MainButton");
    }

    void Start()
    {
        replayBtn.clicked += ReplayGame;
        mainBtn.clicked += MainGame;
        score = player.GetComponent<PlayerCooking>();
        finalScore.text = score.points.ToString(); //Estavlecer puntuación final en el Label
        player.SetActive(false); //Ocultar jugador
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplayGame() //Jugar de nuevo el nivel
    {
        Destroy(player);
        SceneManager.LoadScene("InGame");
    }

    public void MainGame() //Volver al Main Menu
    {
        SceneManager.LoadScene("MainMenu");
    }
}
