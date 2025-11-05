using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoOscuro : MonoBehaviour
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
    private List<GameObject> enemies;
    [SerializeField]
    private List<Bullet> ammo;
    [SerializeField]
    private List<Transform> pos;
    Rigidbody2D rb;
    [SerializeField]
    private GameObject tower;
    [SerializeField]
    private string tagEnemy;
    private void Awake()
    {
        hp_max = 10;
        hp = hp_max;
        atk = 1;
        spd = 3;
        for (int i = 0; i < ammo.Count; i++)
        {
                ammo[i].SetTagEnemy(tagEnemy);
                ammo[i].SetAtk(atk);
                ammo[i].SetTime(0);
        }
        rb = GetComponent<Rigidbody2D>();
        rangeAttack.OnEnter += Attack;
        rangeAttack.OnStay += Attack;
        rangeDetection.OnEnter += StopMovement;
        rangeDetection.OnExit += ResumeMovement;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager.AddIntoList(this.gameObject);

    }
    bool isStopped = false;
    // Update is called once per frame
    void Update()
    {
        if(!isStopped)
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
        rb.linearVelocity = dir*spd;
    }
    void ResumeMovement(GameObject go)
    {
        isStopped = false;
       // Move();
    }
    void StopMovement(GameObject enemy)
    {
        if (enemy.CompareTag(tagEnemy))
        {
            isStopped = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    bool cooldown = false;
    bool isReloading = false;
    private void Attack(GameObject enemy)
    {
        if (enemy.tag == tagEnemy)
        {
            if (!cooldown)
            {
                cooldown = true;
                Bullet bullet = null;
                for (int i = 0; i < ammo.Count; i++)
                {
                    if (ammo[i].available)
                    {
                        bullet = ammo[i];
                        bullet.available = false;
                        break;
                    }
                }
                if (bullet != null)
                {
                    bullet.transform.parent = null;
                    Vector2 dir = (enemy.transform.position - bullet.transform.position).normalized;
                    bullet.rigidbody2d.linearVelocity = dir * 6;
                    bullet.Die(bullet.getTime);
                    StartCoroutine(ResetCooldown());
                }
                else
                {
                    if (AllReload() || !isReloading)
                        StartCoroutine(Reload());
                }

            }
        }
    }
    public void ReceiveDamage(int atk)
    {
        this.hp -= atk;
        if(this.hp <= 0)
        {
            for (int i = 0; i < ammo.Count; i++)
            {
                Destroy(ammo[i].gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(1);
        cooldown = false;
    }
    bool AllReload()
    {
        for (int i = 0; i < ammo.Count; i++)
        {
            if (!ammo[i].available)
                return false;
        }
        return true;
    }
    IEnumerator Reload()
    {
        isReloading = true;
        bool recargado = AllReload();

        while (!recargado)
        {
            for (int i = 0; i < ammo.Count; i++)
            {
                if (!ammo[i].available)
                {
                    yield return new WaitForSeconds(3);
                    ammo[i].transform.parent = this.transform;
                    ammo[i].rigidbody2d.linearVelocity = new Vector2(0, 0);
                    ammo[i].transform.position = pos[i].position;
                    ammo[i].gameObject.SetActive(true);
                    ammo[i].available = true;
                    StartCoroutine(ResetCooldown());
                }
            }
            recargado = AllReload();
        }
        StartCoroutine(ResetCooldown());
        isReloading = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnDisable()
    {
        gameManager.RemoveOfList(this.gameObject);
    }
}
