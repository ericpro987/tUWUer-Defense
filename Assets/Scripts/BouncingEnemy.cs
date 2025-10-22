using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{
    [SerializeField]
    private Range rangeDetection;
    [SerializeField]
    private Range rangeAttack;
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private GameObject tower;
    private Rigidbody2D rb;
    [SerializeField]
    private string tagEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        bullet.SetTagEnemy(tagEnemy);
        rb = GetComponent<Rigidbody2D>();
        bullet.SetBouncing(true);
        rangeAttack.OnEnter += Attack;
        rangeAttack.OnStay += Attack;
        rangeDetection.OnEnter += StopMovement;
        rangeDetection.OnExit += ResumeMovement;
    }
    void Start()
    {

    }
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

    bool cooldown = false;
    private void Attack(GameObject enemy)
    {
        if (!cooldown && !bullet.isActiveAndEnabled)
        {
            cooldown = true;
            if (enemy.tag == tagEnemy)
            {
                Debug.Log("Entro a Attack");
                bullet.transform.parent = null;
                bullet.transform.position = transform.position;
                bullet.gameObject.SetActive(true);
                Vector2 dir = (enemy.transform.position - bullet.transform.position).normalized;
                bullet.rigidbody2d.linearVelocity = dir * 6;
                StartCoroutine(ResetCooldown());
            }
        }
      
    }  
    IEnumerator ResetCooldown()
        {
            yield return new WaitForSeconds(2.5f);
            cooldown = false;
        }
    }
