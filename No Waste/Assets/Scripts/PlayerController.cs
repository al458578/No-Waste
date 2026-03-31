using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 8f;

    private float initialScaleX;

    private Rigidbody2D rb;
    private Animator animator;
    
    private float horizontalInput;
    private float verticalInput;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Guardamos la escala que le pusiste en el Inspector al empezar
        initialScaleX = transform.localScale.x;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
 }
}
