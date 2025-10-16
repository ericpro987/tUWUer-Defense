using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoOscuro : MonoBehaviour
{
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
        rb = GetComponent<Rigidbody2D>();
        rangeAttack.OnEnter += Attack;
        rangeAttack.OnStay += Attack;
        rangeDetection.OnEnter += StopMovement;
        rangeDetection.OnExit += ResumeMovement;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    bool isStopped = false;
    // Update is called once per frame
    void Update()
    {
        if(!isStopped)
            Move();
    }
    void Move()
    {
        Vector3 dir = (tower.transform.position - this.transform.position).normalized;
        rb.linearVelocity = dir*2;
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
    bool isReloading = false;
    private void Attack(GameObject enemy)
    {
        if (enemy.tag == tagEnemy)
        {
            Debug.Log("Entro a Attack");
            if (!cooldown)
            {
                cooldown = true;
                Bullet bullet = null;
                for (int i = 0; i < ammo.Count; i++)
                {
                    if (ammo[i].available)
                    {
                        Debug.Log("Entro en bullet y asigno");
                        bullet = ammo[i];
                        bullet.available = false;
                        break;
                    }
                }
                if (bullet != null)
                {
                    bullet.transform.parent = null;
                    Debug.Log("Bullet no es null");
                    Vector2 dir = (enemy.transform.position - bullet.transform.position).normalized;
                    bullet.rigidbody2d.linearVelocity = dir * 6;
                    bullet.Die();
                    StartCoroutine(ResetCooldown());
                }
                else
                {
                    Debug.Log("Bullet es null");
                    if (AllReload() || !isReloading)
                        StartCoroutine(Reload());
                }

            }
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
}
