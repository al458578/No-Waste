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
    {
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
        finalScore.text = score.points.ToString();
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplayGame()
    {
        Destroy(player);
        SceneManager.LoadScene("InGame");
    }

    public void MainGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
