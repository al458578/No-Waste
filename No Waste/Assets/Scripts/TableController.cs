using UnityEngine;

public class TableController : MonoBehaviour
{
    private Animator animator;
    private bool isBroken = false;
    private bool isOcuped = false;
    private int random;
    private int typeFood;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        random = UnityEngine.Random.Range(1, 11);
        isOcuped = true;
        typeFood = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isBroken)
        {
            //Método para aparecer reparado y nuevo número
        }
        else if (isOcuped)
        {
            gameObject.tag = "Table";
            animator.SetInteger("Custom", random);
        }
    }

    public void TableBreak()
    {
        isBroken = true;
        animator.SetBool("Broken", true);
        animator.SetInteger("Custom", 0);
        gameObject.tag = "Broken";
        isOcuped = false;
    }

    public void TableRepair()
    {
        gameObject.tag = "Untagged";
        isBroken = false;
        animator.SetBool("Broken", false);
    }

    public bool CheckFood(int foodNumber)
    {
        return (typeFood == foodNumber);
    }

    public void Reset()
    {
        animator.SetInteger("Custom", 0);
        isOcuped = false;
        gameObject.tag = "Untagged";
    }
}
