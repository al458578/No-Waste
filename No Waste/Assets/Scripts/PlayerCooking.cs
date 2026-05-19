using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCooking : MonoBehaviour
{
    [Header("Cooking")]
    //Variables InputActions
    private Animator animator;
    private PlayerInput playerInput;
    private InputAction cookAction;
    private InputAction interactAction;
    private InputAction repairAction;

    //Variables de comprobación
    private bool isCooking = false;
    private bool isBin = false;
    private bool isFridge = false;
    private bool isRepair = false;
    private bool isTable = false;

    private TableController table;
    public int points = 0; //Establecer puntuación en 0
    [SerializeField] private UIDocument uiDoc;
    private Label scoreText;

    private GameObject personaje;

    void Awake()
    { //Asignar las acciones del InputAction
        playerInput = GetComponent<PlayerInput>();
        cookAction = playerInput.actions.FindAction("Cook_1"); //Cocinar comida con letra: G
        interactAction = playerInput.actions.FindAction("Interact"); //Interactuar con Objetos con letra: E
        repairAction = playerInput.actions.FindAction("Repair"); //Reparar mesas con letra: R
        personaje = GameObject.Find("Player");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        scoreText = uiDoc.rootVisualElement.Q<Label>("ScoreLabel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //Activar acciones
        cookAction.Enable();
        cookAction.performed += OnCooking;

        interactAction.Enable();
        interactAction.performed += OnInteract;

        repairAction.Enable();
        repairAction.performed += OnRepair;
    }

    private void OnDisable()
    {
        //Desactivar acciones
        interactAction.performed -= OnInteract;
        interactAction.Disable();

        cookAction.Disable();

        repairAction.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kitchen")) //Al colisionar con el objeto Kitchen (el horno)
        {
            isCooking = true;
        }

        else if (collision.gameObject.CompareTag("Bin")) //Al colisionar con el objeto Bin (papelera)
        {
            isBin = true;
        }

        else if (collision.gameObject.CompareTag("Fridge")) //Al colisionar con el objeto Fridge (nevera)
        {
            isFridge = true;
        }

        else if (collision.gameObject.CompareTag("Broken")) //Al colisionar con el obejto Broken (mesa rota)
        {
            isRepair = true;
            table = collision.gameObject.GetComponent<TableController>();
        }

        else if (collision.gameObject.CompareTag("Table")) //Al colisionar con el objeto Table (mesa con clientes)
        {
            isTable = true;
            table = collision.gameObject.GetComponent<TableController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kitchen")) //Alejarse del obejto Kitchen
        {
            isCooking = false;
        }

        else if (collision.gameObject.CompareTag("Bin")) //Alejarse del objeto Bin
        {
            isBin = false;
        }

        else if (collision.gameObject.CompareTag("Fridge")) //Alejarse del objeto Frdige
        {
            isFridge = false;
        }

        else if (collision.gameObject.CompareTag("Broken")) //Alejarse del objeto Broken
        {
            isRepair = false;
            table = null;
        }

        else if (collision.gameObject.CompareTag("Table")) //Alejarse del objeto Table
        {
            isTable = false;
            table = null;
        }
    }

    private void OnCooking(InputAction.CallbackContext context) //Aumentar valor de comida y activar la aniamción cocinar
    {
        if (isCooking && animator.GetInteger("Food") == 0) 
        {
            animator.SetInteger("Food", 1);
        }
    }

    private void OnInteract(InputAction.CallbackContext context) //Interactuar con distintos objetos
    {
        if (isBin && animator.GetInteger("Food") > 0) //Lanzar plato de comida a la basura
        {
            animator.SetInteger("Food", 0);
            points -= 60; //Restar puntos (acción prohibida)
            ShowScore();
        }

        else if (isFridge && animator.GetInteger("Food") > 0) //Guardar comida en la nevera
        {
            animator.SetInteger("Food", 0);
        }

        else if (isTable && animator.GetInteger("Food") > 0 && table.CheckFood(1)) //Entregar comida en la mesa
        {
            points += (table.time * 2);
            ShowScore();

            animator.SetInteger("Food", 0); //Volver al estado normal
            table.Reset(); //La mesa se vacía

            isTable = false;
            table = null;
        }
    }

    private void OnRepair(InputAction.CallbackContext context) //Reparar mesas rotas
    {
        if (isRepair && table != null && animator.GetInteger("Food") == 0)
        {
            PlayerHealth player = personaje.GetComponent<PlayerHealth>(); //Restaurar una pequeña porción de vida
            player.TakeHealth(5);
            animator.SetTrigger("Repaired");
            table.TableRepair(); //Repararla y convertirla en mesa vacía

            isRepair = false;
        }
    }

    public void ShowScore() //Actualizar puntuación
    {
        scoreText.text = points.ToString();
    }
}
