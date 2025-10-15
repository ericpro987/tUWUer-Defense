using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool available;
    public Rigidbody2D rigidbody2d;
    public Vector3 initialPosition;
    [SerializeField]
    LayerMask layer;
    // public Rigidbody2D rb => rigidbody2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        initialPosition = transform.position;
        rigidbody2d = GetComponent<Rigidbody2D>();
        available = true;

    }
    private void OnEnable()
    {
       // StartCoroutine(Die());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter");
        if (collision.gameObject.layer == layer)
        {
            Debug.Log("Entro al if");
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter");
        if (collision.gameObject.layer == layer)
        {
            this.gameObject.SetActive(false);
        }
    }
}
