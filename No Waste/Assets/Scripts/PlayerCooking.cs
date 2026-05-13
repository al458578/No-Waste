using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCooking : MonoBehaviour
{
    [Header("Cooking")]

    private Animator animator;
    private PlayerInput playerInput;
    private InputAction cookAction;
    private InputAction interactAction;
    private InputAction repairAction;

    private bool isCooking = false;
    private bool isBin = false;
    private bool isFridge = false;
    private bool isRepair = false;
    private bool isTable = false;

    private TableController table;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cookAction = playerInput.actions.FindAction("Cook_1");
        interactAction = playerInput.actions.FindAction("Interact");
        repairAction = playerInput.actions.FindAction("Repair");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        cookAction.Enable();
        cookAction.performed += OnCooking;

        interactAction.Enable();
        interactAction.performed += OnInteract;

        repairAction.Enable();
        repairAction.performed += OnRepair;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteract;
        interactAction.Disable();

        cookAction.Disable();

        repairAction.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kitchen"))
        {
            isCooking = true;
        }

        else if (collision.gameObject.CompareTag("Bin"))
        {
            isBin = true;
        }

        else if (collision.gameObject.CompareTag("Fridge"))
        {
            isFridge = true;
        }

        else if (collision.gameObject.CompareTag("Broken"))
        {
            isRepair = true;
            table = collision.gameObject.GetComponent<TableController>();
        }

        else if (collision.gameObject.CompareTag("Table"))
        {
            isTable = true;
            table = collision.gameObject.GetComponent<TableController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kitchen"))
        {
            isCooking = false;
        }

        else if (collision.gameObject.CompareTag("Bin"))
        {
            isBin = false;
        }

        else if (collision.gameObject.CompareTag("Fridge"))
        {
            isFridge = false;
        }

        else if (collision.gameObject.CompareTag("Broken"))
        {
            isRepair = false;
            table = null;
        }

        else if (collision.gameObject.CompareTag("Table"))
        {
            isTable = false;
            table = null;
        }
    }

    private void OnCooking(InputAction.CallbackContext context)
    {
        if (isCooking && animator.GetInteger("Food") == 0)
        {
            animator.SetInteger("Food", 1);
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (isBin && animator.GetInteger("Food") > 0)
        {
            animator.SetInteger("Food", 0);
        }

        else if (isFridge && animator.GetInteger("Food") > 0)
        {
            animator.SetInteger("Food", 0);
        }

        else if (isTable && animator.GetInteger("Food") > 0 && table.CheckFood(1))
        {
            animator.SetInteger("Food", 0);
            table.Reset();

            isTable = false;
            table = null;
        }
    }

    private void OnRepair(InputAction.CallbackContext context)
    {
        if (isRepair && table != null && animator.GetInteger("Food") == 0)
        {
            animator.SetTrigger("Repaired");
            table.TableRepair();

            isRepair = false;
        }
    }
}
