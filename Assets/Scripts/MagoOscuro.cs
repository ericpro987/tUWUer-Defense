using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoOscuro : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    public int hp_max { get; private set; } = 10;
    [SerializeField]
    public int hp { get; private set; }
    [SerializeField]
    public int atk { get; private set; } = 1;
    [SerializeField]
    public int spd { get; private set; } = 3;
    [SerializeField]
    public Range rangeDetection { get; private set; }
    [SerializeField]
    public Range rangeAttack { get; private set; }
    [SerializeField]
    public List<GameObject> enemies { get; private set; } = new List<GameObject>();
    [SerializeField]
    public List<Bullet> ammo { get; private set; } = new List<Bullet>();
    [SerializeField]
    public List<Transform> pos { get; private set; }
    [SerializeField]
    public GameObject tower { get; private set; }
    [SerializeField]
    public string tagEnemy { get; private set; }

    private Rigidbody2D rb;
    public bool isStopped = false;
    public bool cooldown = false;
    public bool isReloading = false;

    private void Awake()
    {
        ValidateReferences();

        hp = hp_max;

        InitializeAmmo();

        rb = GetComponent<Rigidbody2D>();

        rangeAttack.OnEnter += Attack;
        rangeAttack.OnStay += Attack;
        rangeDetection.OnEnter += StopMovement;
        rangeDetection.OnExit += ResumeMovement;
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
    }

    private void InitializeAmmo()
    {
        foreach (var bullet in ammo)
        {
            bullet.SetTagEnemy(tagEnemy);
            bullet.SetAtk(atk);
            bullet.SetTime(0);
        }
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
    private void Move()
    {
        Vector3 direction = (tower.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * spd;
    }

    private void ResumeMovement(GameObject go)
    {
     //   Time.timeScale = 0;
        isStopped = false;
    }

    private void StopMovement(GameObject enemy)
    {
        if (enemy.CompareTag(tagEnemy))
        {
            isStopped = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void Attack(GameObject enemy)
    {
        if (!enemy.CompareTag(tagEnemy)) return;

        if (!cooldown)
        {
            cooldown = true;

            Bullet bullet = GetAvailableBullet();

            if (bullet != null)
            {
                FireBullet(bullet, enemy);
            }
            else if (AllReload() || !isReloading)
            {
                StartCoroutine(Reload());
            }
            StartCoroutine(ResetCooldown());

        }
    }

    private Bullet GetAvailableBullet()
    {
        foreach (var bullet in ammo)
        {
            if (bullet.available)
            {
                bullet.available = false;
                return bullet;
            }
        }
        return null;
    }

    private void FireBullet(Bullet bullet, GameObject enemy)
    {
        bullet.transform.parent = null;
        Vector2 direction = (enemy.transform.position - bullet.transform.position).normalized;
        bullet.rigidbody2d.linearVelocity = direction * 6;
        bullet.Die(bullet.getTime);
     //   StartCoroutine(ResetCooldown());
    }

    public void ReceiveDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log($"Enemigo destruido: {gameObject.name}");
            DestroyAllAmmo();
            Destroy(gameObject);
        }
    }

    private void DestroyAllAmmo()
    {
        foreach (var bullet in ammo)
        {
            Destroy(bullet.gameObject);
        }
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(1);
        cooldown = false;
    }

    private bool AllReload()
    {
        foreach (var bullet in ammo)
        {
            if (!bullet.available)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        while (!AllReload())
        {
            for (int i = 0; i < ammo.Count; i++)
            {
                if (!ammo[i].available)
                {
                    yield return new WaitForSeconds(3);
                    ResetBullet(ammo[i], pos[i]);
                }
            }
        }

        isReloading = false;
    }

    private void ResetBullet(Bullet bullet, Transform position)
    {
        bullet.transform.parent = transform;
        bullet.rigidbody2d.linearVelocity = Vector2.zero;
        bullet.transform.position = position.position;
        bullet.gameObject.SetActive(true);
        bullet.available = true;
    }

    public void RemoveFromList(GameObject go)
    {
        if (go.CompareTag(tagEnemy))
        {
            enemies.Remove(go);
        }
    }

    private void OnDisable()
    {
        gameManager.RemoveOfList(this.gameObject);
    }
}