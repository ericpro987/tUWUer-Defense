using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PjTorrePrincipal : MonoBehaviour
{
    [SerializeField]
    public int hp_max { get; private set; }
    [field: SerializeField]
    public int hp { get; private set; }
    public int atk { get; private set; }
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
    private string tagEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        hp_max = 10;
        hp = hp_max;
        atk = 1;
        spd = 3;
        for (int i = 0; i < ammo.Count; i++)
        {
            if (!ammo[i].available)
            {
                ammo[i].SetTagEnemy(tagEnemy);
                ammo[i].SetAtk(atk);
            }
        }
        rangeDetection.OnEnter += PushEnemiesInList;
        rangeAttack.OnEnter += Attack;
        rangeAttack.OnStay += Attack;
    }
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetHp(int hp)
    {
        this.hp = hp;
    }
    public void SetSpd(int spd)
    {
        this.spd= spd;
    }
    public void SetAtk(int atk)
    {
        this.atk = atk;
    }

    private void PushEnemiesInList(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    bool cooldown = false;
    private void Attack(GameObject enemy)
    {
        if (!cooldown)
        {
            cooldown = true;
            Bullet bullet = null;
            for (int i = 0; i < ammo.Count; i++)
            {
                if (!ammo[i].gameObject.activeSelf && ammo[i].available)
                {
                    bullet = ammo[i];
                    bullet.transform.position = this.transform.position;
                    bullet.available = false;
                    break;
                }
            }
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
                Vector2 dir = (enemy.transform.position - bullet.transform.position).normalized;
                bullet.rigidbody2d.linearVelocity = dir*6;
                StartCoroutine(ResetCooldown());
            }
            else
            {
                StartCoroutine(Reload());
            }
            
        }
    }
    public void ReceiveDamage(int atk)
    {
        this.hp -= atk;
        if (this.hp <= 0)
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
    IEnumerator Reload()
    {
        cooldown = true;
        yield return new WaitForSeconds(3);
        for (int i = 0; i < ammo.Count; i++)
        {
            ammo[i].gameObject.SetActive(false);
            ammo[i].available = true;
        }
        cooldown = false;
    }

}
