using UnityEngine;

public class BotController : MonoBehaviour
{
    public Transform target;
    public GameObject mesa;
    [SerializeField] private float speed = 3f;
    private Rigidbody2D rb;
    private float minDistance = 0.5f;
    private bool lookingRight = true;

    [SerializeField] private float detectionDistance = 2.5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float rotationSmoothTime = 0.05f;
    private float avoidanceStickiness = 0.2f;
    private Vector2 currentVelocity;
    private Vector2 chosenAvoidanceDir;
    private float avoidanceTimer;

    private Animator animator;
    private bool isAttacking = false;
    [SerializeField] private GameObject dieEffect;
    private int objective;

    [Header("Attack Settings")]
    [SerializeField] private int damageAmount = 5;
    [SerializeField] private float attackCooldown = 2.0f;
    private float nextAttackTime;
    public GameObject personaje;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objective = Random.Range(1, 8);
        personaje = GameObject.Find("Player");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        switch (objective)
        {
            case 1:
                {
                    mesa = GameObject.Find("MesaDuo");
                    target = mesa.transform;
                    break;
                } 
            case 2:
                {
                    mesa = GameObject.Find("MesaDuo 1");
                    target = mesa.transform;
                    break;
                }
            case 3:
                {
                    mesa = GameObject.Find("MesaTrio");
                    target = mesa.transform;
                    break;
                }
            case 4:
                {
                    mesa = GameObject.Find("MesaTrio (1)");
                    target = mesa.transform;
                    break;
                }
            case 5:
                {
                    mesa = GameObject.Find("MesaCuart");
                    target = mesa.transform;
                    break;
                }
            case 6:
                {
                    mesa = GameObject.Find("MesaCuart (1)");
                    target = mesa.transform;
                    break;
                }
            default:
                {
                    mesa = GameObject.Find("MesaCuart (2)");
                    target = mesa.transform;
                    break;
                }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            if (Time.time >= nextAttackTime) ApplyContinuousDamage();
            return;
        }
        Vector2 dirToTarget = (target.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance > minDistance)
        {
            Vector2 finalDir = CalculateSmartDirection(dirToTarget);
            Vector2 smoothDir = Vector2.SmoothDamp(rb.linearVelocity.normalized, finalDir, ref currentVelocity, rotationSmoothTime);
            rb.linearVelocity = smoothDir * speed;
            ControlRotation();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            avoidanceTimer = 0;
        }
    }

    private void ControlRotation()
    {
        if (rb.linearVelocity.x > -0.2f && !lookingRight)
            RotateY();
        else if (rb.linearVelocity.x < -0.2f && lookingRight)
            RotateY();
    }

    private void RotateY()
    {
        lookingRight = !lookingRight;
        transform.Rotate(0, 180, 0);
    }

    private Vector2 CalculateSmartDirection(Vector2 targetDir)
    {
        Vector2 finalDir = targetDir;
        if (avoidanceTimer > 0)
        {
            avoidanceTimer -= Time.fixedDeltaTime;
            finalDir = chosenAvoidanceDir;
        }
        else //Lanzamos rayos
        {
            RaycastHit2D hitCenter = Physics2D.CircleCast(transform.position, 0.3f, targetDir, detectionDistance, obstacleLayer);
            if (hitCenter.collider != null) //Hay bloqueo
            {
                //Calculamos direcciones alternativas
                Vector2 leftRayDir = Quaternion.Euler(0, 0, 60) * targetDir;
                Vector2 rightRayDir = Quaternion.Euler(0, 0, -60) * targetDir;

                RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, leftRayDir, detectionDistance, obstacleLayer);
                RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rightRayDir, detectionDistance, obstacleLayer);

                if (hitLeft.collider == null)
                    finalDir = leftRayDir;
                else if (hitRight.collider == null)
                    finalDir = rightRayDir;
                else //Si todos están bloqueados, la normal del obstáculo
                {
                    Vector2 perpendicular = Vector2.Perpendicular(hitCenter.normal);
                    float dot = Vector2.Dot(perpendicular, targetDir);
                    finalDir = perpendicular * (dot > 0 ? 1 : -1);
                }
            }
            chosenAvoidanceDir = finalDir;
            avoidanceTimer = avoidanceStickiness;
        }
        return finalDir;
    }

    private void ApplyContinuousDamage()
    {
        PlayerHealth player = personaje.GetComponent<PlayerHealth>();
        if (player != null) player.TakeDamage(damageAmount);
        nextAttackTime = Time.time + attackCooldown;
    }

    private void OnCollisionEnter2D(Collision2D collision) //Añadir Retroceso
    {
        if (collision.transform == target)
        {
            isAttacking = true;
            //rb.linearVelocity = Vector2.zero;
            if (animator != null)
                animator.SetBool("Attacking", true);
            TableController table = collision.gameObject.GetComponent<TableController>();
            if (table != null) table.TableBreak();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == target)
        {
            isAttacking = false;
            if (animator != null) animator.SetBool("Attacking", false);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Instantiate(dieEffect, transform.position, transform.rotation);
    }
}
