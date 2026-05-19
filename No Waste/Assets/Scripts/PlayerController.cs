using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 8f;

    [Header("Attack")]

    private float initialScaleX;

    //Componentes
    private Rigidbody2D rb;
    private Animator animator;
    
    //Ejes
    private float horizontalInput;
    private float verticalInput;

    //InputActions y otros Scripts
    private PlayerInput playerInput;
    private InputAction attackAction;
    private GameObject player;
    private PlayerCooking playerScore;

    private bool isEnemy = false;
    private BotController bot;

    //Tiempo de partida
    private float elapsedTime = 0f;
    private int time = 180;
    [SerializeField] private UIDocument uiDoc;
    private Label timeText;

    void Awake()
    {//Establecer variables
        time = 180;
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions.FindAction("Attack"); //Atacar con la tecla: F
        player = GameObject.Find("Player");
    }

    void Start()
    {
        time = 180;

        initialScaleX = transform.localScale.x; // Guardamos la escala del Inspector al empezar

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timeText = uiDoc.rootVisualElement.Q<Label>("TimeLabel");
        playerScore = player.GetComponent<PlayerCooking>();
    }

        // Se activa con el componente Player Input (Action: Move)
    public void OnMove(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();
        horizontalInput = inputVec.x;
        verticalInput = inputVec.y;
    }

    void FixedUpdate()
    {
        // Aplicar movimiento en ambos ejes
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);

        // Actualizar Animación (usamos Mathf.Abs para que sea siempre positivo)
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetFloat("VerticalSpeed",verticalInput); //sin Abs para diferenciar arriba y abajo

        // Girar el Sprite según la dirección
        if (horizontalInput < 0)
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, transform.localScale.z); // Mira a la derecha
        else if (horizontalInput > 0)
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);  // Mira a la izquierda


    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f && time > 0)
        {
            time--;
            elapsedTime = 0f;
        }
        timeText.text = time.ToString(); //Actualizar el tiempo de juego restante

        if (time <= 0 && playerScore.points <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        else if (time <= 0)
        {
            DontDestroyOnLoad(player);
            SceneManager.LoadScene("ScoreMenu");
        }
    }

    private void OnEnable()
    {
        attackAction.Enable();
        attackAction.performed += OnFightPerformed;
        attackAction.canceled += OnFightCanceled;
    }

    private void OnDisable()
    {
        attackAction.performed -= OnFightPerformed;
        attackAction.canceled -= OnFightCanceled;
        attackAction.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //Colisión con Objeto Enemigo
        {
            isEnemy = true;
            bot = collision.gameObject.GetComponent<BotController>(); //Guardar objeto en Bot
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //Alejarse del Objeto Enemigo
        {
            isEnemy = false;
            bot = null; //Vaciar variable Bot
        }
    }

    private void OnFightPerformed(InputAction.CallbackContext context)
    {
        animator.SetBool("Fight", true); //Activar aniamción de ataque
        if (isEnemy && bot != null && animator.GetInteger("Food") == 0) //Si hay un obejto Bot y el juagdor tiene las manos libres
        {
            bot.Die(); //Elimianr Objeto Bot
        }
    }

    private void OnFightCanceled(InputAction.CallbackContext context)
    {
        animator.SetBool("Fight", false); //Volver a la animación normal
    }
}
