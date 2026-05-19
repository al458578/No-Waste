using UnityEngine;

public class TableController : MonoBehaviour
{
    private Animator animator;
    private bool isOcuped = false;
    private int random;
    private int typeFood;

    //Variables de tiempo de espera
    private float elapsedTime = 0f;
    public int timeCooldown;
    public int time = 30;

    private PlayerCooking playerScore;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { //Establecer valores a la variables
        animator = GetComponent<Animator>();
        timeCooldown = UnityEngine.Random.Range(10, 21);
        typeFood = 1; //Tipo de comida que piden los clientes, en un primer momento pensado para más pero al final limitado a 1
        player = GameObject.Find("Player");
        playerScore = player.GetComponent<PlayerCooking>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isOcuped && gameObject.tag == "Table") //Si la mesa está ocupada por clientes
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1f && time > 0) //Esperar su tiempo de espera (30 segundos)
            {
                time--;
                elapsedTime = 0f;
            }

            if (time == 0) //Si el tiempo se agota, reinciiar/vaciar mesa
            {
                Reset();
                playerScore.points -= 10;
                playerScore.ShowScore();
            }
        }
        else if (isOcuped == false && gameObject.tag == "Untagged") //Si la mesa está vacía
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f && timeCooldown > 0) //Esperar el Cooldown para aparecer nuevo clientes
            {
                timeCooldown--;
                elapsedTime = 0f;
            }

            if (timeCooldown == 0)
            {
                OnOcuped();
            }
        }
    }

    public void TableBreak() //Passar mesa al estado roto
    {
        animator.SetBool("Broken", true);
        animator.SetInteger("Custom", 0);
        gameObject.tag = "Broken";
        isOcuped = false;
        time = 30;
    }

    public void TableRepair() //Reinciar al estado origianl una vez reparada
    {
        gameObject.tag = "Untagged";
        animator.SetBool("Broken", false);
        time = 30;
        timeCooldown = UnityEngine.Random.Range(10, 21);
        random = UnityEngine.Random.Range(1, 11);
    }

    public bool CheckFood(int foodNumber)
    { //Revisar que la comida que lleva el jugador es la misma que piden en la mesa (acción pensada para que hubiera más opciones)
        return (typeFood == foodNumber);
    }

    public void Reset() //Restablecer mesa a su estado vacío
    {
        isOcuped = false;
        animator.SetInteger("Custom", 0);
        gameObject.tag = "Untagged";
        elapsedTime = 0f;
        timeCooldown = UnityEngine.Random.Range(10, 21);
    }
    public void OnOcuped() //Passar mesa al estado ocupada/llena
    {
        isOcuped = true;
        gameObject.tag = "Table";
        random = UnityEngine.Random.Range(1, 11);
        animator.SetInteger("Custom", random);
        time = 30;
    }
}
