using System.Collections;
using System.Transactions;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool available;
    public Rigidbody2D rigidbody2d;
    public Vector3 initialPosition;
    [SerializeField]
    private int layer;
    [SerializeField]
    private string tagEnemy;
    [SerializeField]
    private bool bouncing = false;
    [SerializeField]
    private LayerMask layerHit;
    public void SetBouncing(bool bouncing)
    {
        this.bouncing = bouncing;
    }
    public void SetTagEnemy(string tag)
    {
        this.tagEnemy = tag;
    }
    // public Rigidbody2D rb => rigidbody2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        layer = LayerMask.NameToLayer("Player");
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
        if (bouncing) Debug.LogAssertion("Entro a DIE");
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter");
        if (collision.transform.gameObject.tag == tagEnemy && collision.transform.gameObject.layer == layer)
        {
            Debug.Log("Entro al if");
            if (bouncing)
            {
                Debug.Log("Entro al bouncing");
                Collider2D hit = Physics2D.OverlapCircle(this.transform.position, 50, layerHit);
                if (hit)
                {
                    Debug.Log("Entro al hit");
                    Debug.LogError(hit.transform.name);

                        Debug.LogWarning("Entro al cambio de velocity");

                        Vector2 dir = (hit.transform.position - this.transform.position).normalized;
                        this.rigidbody2d.linearVelocity = dir * 5;
                }
                StartCoroutine(Die());

            }
            //this.gameObject.SetActive(false);
        }
    }
}