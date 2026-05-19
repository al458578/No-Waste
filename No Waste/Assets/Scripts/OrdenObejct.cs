using UnityEngine;

public class OrdenObejct : MonoBehaviour
{
    public int offset = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {//Objtener el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {//Modificar estado de layer, según la posición Y del objeto
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100) + offset;
    }
}
