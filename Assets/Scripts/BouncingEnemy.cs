using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private int hp_max = 20;
    [SerializeField]
    private int hp;
    [SerializeField]
    private int atk = 1;
    [SerializeField]
    private int spd = 3;
    [SerializeField]
    private Range rangeDetection;
    [SerializeField]
    private Range rangeAttack;
    [SerializeField]
    private Bullet bullet;  
    [SerializeField]
    private GameObject tower;
    [SerializeField]
    private string tagEnemy;

    private Rigidbody2D rb;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();
    private bool isStopped = false;
    private bool cooldown = false;



    [SerializeField]
    private GameObject torre1;

    [SerializeField]
    private GameObject torre2;

    private void Awake()
    {
        ValidateReferences();

        hp = hp_max;

        InitializeBullet();

        rb = GetComponent<Rigidbody2D>();

        rangeAttack.OnEnter += Attack;
        rangeAttack.OnStay += Attack;
        rangeDetection.OnEnter += PushInList;
        rangeDetection.OnExit += RemoveFromList;
        rangeAttack.OnEnter += StopMovement;
        rangeAttack.OnExit += ResumeMovement;



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

    private void Start()
    {
        gameManager.AddIntoList(this.gameObject);
    }

    private void Update()
    {
        if (!isStopped)
        {
            Move();
        }
    }

    private void ValidateReferences()
    {
        if (gameManager == null) Debug.LogError("GameManager no est� asignado.");
        if (tower == null) Debug.LogError("Tower no est� asignado.");
        if (rangeDetection == null) Debug.LogError("RangeDetection no est� asignado.");
        if (rangeAttack == null) Debug.LogError("RangeAttack no est� asignado.");
        if (bullet == null) Debug.LogError("Bullet no est� asignado.");
    }

    private void InitializeBullet()
    {
        bullet.SetAtk(atk);
        bullet.SetTagEnemy(tagEnemy);
        bullet.SetBouncing(true);
        bullet.gameObject.SetActive(false);
    }

    private void Move()
    {
        Vector3 direction = (tower.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * spd;
    }

    private void PushInList(GameObject go)
    {
        if (go.CompareTag(tagEnemy))
        {
            enemies.Add(go);
        }
    }

    public void RemoveFromList(GameObject go)
    {
        if (go.CompareTag(tagEnemy))
        {
            enemies.Remove(go);
        }
    }

    private void ResumeMovement(GameObject go)
    {
        if (enemies.Count == 0)
        {
            isStopped = false;
        }
        else
        {
            rangeDetection.OnStay?.Invoke(enemies[0]);
        }
    }

    private void StopMovement(GameObject enemy)
    {
        isStopped = true;
        rb.linearVelocity = Vector2.zero;
    }

    private void Attack(GameObject enemy)
    {
        if (!enemy.CompareTag(tagEnemy) || cooldown) return;

        cooldown = true;

        FireBullet(enemy);
        StartCoroutine(ResetCooldown());
    }

    private void FireBullet(GameObject enemy)
    {
        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
        bullet.transform.parent = null;

        Vector2 direction = (enemy.transform.position - bullet.transform.position).normalized;
        bullet.rigidbody2d.linearVelocity = direction * 6;
    }

    public void ReceiveDamage(int damage)
    {
        Debug.LogError("AYUDA!!! ME ESTAN MATANDO!!!");
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(2.5f);
        bullet.transform.parent = transform;
        cooldown = false;
    }

    private void OnDisable()
    {
        gameManager.RemoveOfList(this.gameObject);
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
}
