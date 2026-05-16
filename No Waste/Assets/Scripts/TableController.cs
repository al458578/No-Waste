using UnityEngine;

public class TableController : MonoBehaviour
{
    private Animator animator;
    private bool isOcuped = false;
    private int random;
    private int typeFood;

    private float elapsedTime = 0f;
    public int timeCooldown;
    public int time = 30;

    private PlayerCooking playerScore;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        timeCooldown = UnityEngine.Random.Range(5, 16);
        typeFood = 1;
        player = GameObject.Find("Player");
        playerScore = player.GetComponent<PlayerCooking>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isOcuped && gameObject.tag == "Table")
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1f && time > 0)
            {
                time--;
                elapsedTime = 0f;
            }

            if (time == 0)
            {
                Reset();
                playerScore.points -= 20;
                playerScore.ShowScore();
            }
        }
        else if (isOcuped == false && gameObject.tag == "Untagged")
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f && timeCooldown > 0)
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

    public void TableBreak()
    {
        animator.SetBool("Broken", true);
        animator.SetInteger("Custom", 0);
        gameObject.tag = "Broken";
        isOcuped = false;
        time = 30;
    }

    public void TableRepair()
    {
        gameObject.tag = "Untagged";
        animator.SetBool("Broken", false);
        time = 30;
        timeCooldown = UnityEngine.Random.Range(5, 16);
        random = UnityEngine.Random.Range(1, 11);
    }

    public bool CheckFood(int foodNumber)
    {
        return (typeFood == foodNumber);
    }

    public void Reset()
    {
        isOcuped = false;
        animator.SetInteger("Custom", 0);
        gameObject.tag = "Untagged";
        elapsedTime = 0f;
        timeCooldown = UnityEngine.Random.Range(5, 16);
    }
    public void OnOcuped()
    {
        isOcuped = true;
        gameObject.tag = "Table";
        random = UnityEngine.Random.Range(1, 11);
        animator.SetInteger("Custom", random);
        time = 30;
    }
}
