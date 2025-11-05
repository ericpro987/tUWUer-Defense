using JetBrains.Annotations;
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
    private int time = 2;
    public int getTime => time;
    [SerializeField]
    private LayerMask layerHit;
    public int atk { get; private set; }
    public void SetBouncing(bool bouncing)
    {
        this.bouncing = bouncing;
    }
    public void SetTagEnemy(string tag)
    {
        this.tagEnemy = tag;
    }
    public void SetAtk(int atk)
    {
        this.atk = atk;
    }
    public void SetTime(int time)
    {
        this.time = time;
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
    public IEnumerator Die(int time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == tagEnemy && collision.transform.gameObject.layer == layer)
        {
            if (bouncing)
            {
                Collider2D hit = Physics2D.OverlapCircle(this.transform.position, 50, layerHit);
                if (hit)
                {
                        Vector2 dir = (hit.transform.position - this.transform.position).normalized;
                        this.rigidbody2d.linearVelocity = dir * 5;
                }
            }
            Debug.LogAssertion(collision.gameObject.name);
            if (collision.gameObject.TryGetComponent<MagoOscuro>(out MagoOscuro magoOscuro))
            {
                Debug.Log("mE HAN ATACADO: mAGO");
                magoOscuro.ReceiveDamage(atk);
            }
            else if (collision.gameObject.TryGetComponent<BouncingEnemy>(out BouncingEnemy bouncingEnemy))
            {
                Debug.LogError("mE HAN ATACADO: bOUNCING");
                bouncingEnemy.ReceiveDamage(atk);
            }
            else if (collision.gameObject.TryGetComponent<ExplosiveEnemy>(out ExplosiveEnemy explosiveEnemy))
            {
                explosiveEnemy.ReceiveDamage(atk);
            }
            else if(collision.gameObject.TryGetComponent<PjTorrePrincipal>(out PjTorrePrincipal pjTorrePrincipal))
            {
                pjTorrePrincipal.ReceiveDamage(atk);
            }
            else if (collision.gameObject.TryGetComponent<BasicEnemy>(out BasicEnemy basicEnemy))
            {
                basicEnemy.ReceiveDamage(atk);
            }
            StartCoroutine(Die(0));

            //this.gameObject.SetActive(false);
        }
    }
}