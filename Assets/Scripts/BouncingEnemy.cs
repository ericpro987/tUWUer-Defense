using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{
    [SerializeField]
    public int hp_max { get; private set; }
    [SerializeField]
    public int hp { get; private set; }
    [SerializeField]
    public int atk { get; private set; }
    [SerializeField]
    public int spd { get; private set; }
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
        hp_max = 10;
        hp = 10;
        atk = 1;
        spd = 3;
        bullet.SetAtk(atk);
        bullet.SetTagEnemy(tagEnemy);
        rb = GetComponent<Rigidbody2D>();
        bullet.SetBouncing(true);
        bullet.gameObject.SetActive(false);
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
    public void SetHp(int hp)
    {
        this.hp = hp;
    }
    public void SetSpd(int spd)
    {
        this.spd = spd;
    }
    public void SetAtk(int atk)
    {
        this.atk = atk;
    }
    void Move()
    {
        Vector3 dir = (tower.transform.position - this.transform.position).normalized;
        rb.linearVelocity = dir * spd;
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
                bullet.transform.position = transform.position;
                bullet.gameObject.SetActive(true);  
                bullet.transform.parent = null;
                Vector2 dir = (enemy.transform.position - bullet.transform.position).normalized;
                bullet.rigidbody2d.linearVelocity = dir * 6;
                StartCoroutine(ResetCooldown());
            }
        }
      
    }
    public void ReceiveDamage(int atk)
    {
        this.hp -= atk;
        Debug.Log(hp);
        if (this.hp <= 0)
        {
            Destroy(bullet.gameObject);
            Destroy(this.gameObject);
        }
    }
    IEnumerator ResetCooldown()
        {
            yield return new WaitForSeconds(2.5f);
            cooldown = false;
        }
    }
