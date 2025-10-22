using UnityEngine;

public class ExplosiveEnemy : MonoBehaviour
{
    [SerializeField]
    private Range rangeDetection;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject tower;
    [SerializeField]
    private string tagEnemy;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rangeDetection.OnEnter += StopMovement;
        rangeDetection.OnExit += ResumeMovement;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    bool isStopped = false;
    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
            Move();
    }
    void Move()
    {
        Vector3 dir = (tower.transform.position - this.transform.position).normalized;
        rb.linearVelocity = dir * 2;
    }
    void ResumeMovement(GameObject go)
    {
        isStopped = false;
        // Move();
    }
    void StopMovement(GameObject enemy)
    {
        /*    if (enemy.CompareTag(tagEnemy))
            {*/
        isStopped = true;
        rb.linearVelocity = Vector2.zero;
        // }
    }
    void Explode()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == tagEnemy)
        {
            Explode();
        }
    }
}
