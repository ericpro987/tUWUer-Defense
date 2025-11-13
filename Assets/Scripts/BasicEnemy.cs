using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
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
    [SerializeField]
    List<GameObject> enemies;




    [SerializeField]
    private GameObject torre1;

    [SerializeField]
    private GameObject torre2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        enemies = new List<GameObject>();
        hp_max = 20;
        hp = hp_max;
        atk = 1;
        spd = 3;
        bullet.SetAtk(atk);
        bullet.SetTagEnemy(tagEnemy);
        rb = GetComponent<Rigidbody2D>();
        bullet.SetBouncing(false);
        bullet.gameObject.SetActive(false);
        rangeAttack.OnEnter += Attack;
        rangeAttack.OnStay += Attack;
        rangeDetection.OnEnter += PushInList;
        rangeDetection.OnExit += RemoveFromList;
        rangeDetection.OnEnter += StopMovement;
        rangeDetection.OnExit += ResumeMovement;



        torre1 = GameObject.Find("Torre1");
        torre2 = GameObject.Find("Torre2");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        if (transform.position.x < 0)
        {

            tower = torre2;
            tagEnemy = "J2";
            gameObject.tag = "J1";


        }
        else if (transform.position.x > 0)
        {
            tower = torre1;
            tagEnemy = "J1";
            gameObject.tag = "J2";
        }


        bullet.SetTagEnemy(tagEnemy);
        bullet.SetAtk(atk);
        bullet.SetTime(0);
    }
    void Start()
    {
        gameManager.AddIntoList(this.gameObject);

    }
    bool isStopped = false;
    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
            Move();
    }
    private void PushInList(GameObject go)
    {
        if (go.tag == tagEnemy)
            enemies.Add(go);
    }
    public void RemoveFromList(GameObject go)
    {
        if (go.tag == tagEnemy)
            enemies.Remove(go);
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
            // Reanudar movimiento o lo que necesites hacer
    }

    void StopMovement(GameObject enemy)
    {
            isStopped = true;
            rb.linearVelocity = Vector2.zero; // Detenemos el movimiento
    }


    bool cooldown = false;
    private void Attack(GameObject enemy)
    {
        if (!cooldown)
        {
            cooldown = true;
            bullet.transform.position = transform.position;
            bullet.gameObject.SetActive(true);
            bullet.transform.parent = null;
            Vector2 dir = (enemy.transform.position - bullet.transform.position).normalized;
            bullet.rigidbody2d.linearVelocity = dir * 6; // Asegúrate de usar velocity
            StartCoroutine(ResetCooldown());
        }
    }

    private IEnumerator Attack2(GameObject enemy)
    {
        while (enemies.Count > 0)
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
            yield return new WaitForEndOfFrame();
        }
    }
    public void ReceiveDamage(int atk)
    {
        Debug.LogError("AYUDA!!! ME ESTAN MATANDO!!!");
        this.hp -= atk;
        if (this.hp <= 0)
        {
            Destroy(bullet.gameObject);
            Destroy(this.gameObject);
        }
    }
    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        bullet.transform.parent = this.transform;
        cooldown = false;
    }
    private void OnDestroy()
    {
        gameManager.RemoveOfList(this.gameObject);
    }

}
