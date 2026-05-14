using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 8f;

    [Header("Attack")]

    private float initialScaleX;

    private Rigidbody2D rb;
    private Animator animator;
    
    private float horizontalInput;
    private float verticalInput;

    private PlayerInput playerInput;
    private InputAction attackAction;

    private bool isEnemy = false;
    private BotController bot;

    private float elapsedTime = 0f;
    private int time = 300;
    [SerializeField] private UIDocument uiDoc;
    private Label timeText;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions.FindAction("Attack");
    }

    void Start()
    {
        // Guardamos la escala que le pusiste en el Inspector al empezar
        initialScaleX = transform.localScale.x;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timeText = uiDoc.rootVisualElement.Q<Label>("TimeLabel");
    }

        // Se activa con el componente Player Input (Action: Move)
    public void OnMove(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();
        horizontalInput = inputVec.x;
        verticalInput = inputVec.y;

        // Esto imprimirá los valores en la consola cada vez que presiones WASD
        //Debug.Log("Movimiento detectado: " + moveInput);
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
        timeText.text = time.ToString();
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isEnemy = true;
            bot = collision.gameObject.GetComponent<BotController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isEnemy = false;
            bot = null;
        }
    }

    private void OnFightPerformed(InputAction.CallbackContext context)
    {
        animator.SetBool("Fight", true);
        if (isEnemy && bot != null && animator.GetInteger("Food") == 0)
        {
            bot.Die();
        }
    }

    private void OnFightCanceled(InputAction.CallbackContext context)
    {
        animator.SetBool("Fight", false);
    }
}
