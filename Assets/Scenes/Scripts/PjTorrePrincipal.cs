using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PjTorrePrincipal : MonoBehaviour
{
    [SerializeField]
    private Range rangeDetection;
    [SerializeField]
    private Range rangeAttack;
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private List<Bullet> ammo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
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

    private void PushEnemiesInList(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    bool cooldown = false;
    private void Attack(GameObject enemy)
    {
        Debug.Log("Entro a Attack");
        if (!cooldown)
        {
            cooldown = true;
            Bullet bullet = null;
            for (int i = 0; i < ammo.Count; i++)
            {
                if (!ammo[i].gameObject.activeSelf && ammo[i].available)
                {
                    Debug.Log("Entro en bullet y asigno");
                    bullet = ammo[i];
                    bullet.transform.position = this.transform.position;
                    bullet.available = false;
                    break;
                }
            }
            if (bullet != null)
            {
                Debug.Log("Bullet no es null");
                bullet.gameObject.SetActive(true);
                Vector2 dir = (enemy.transform.position - bullet.transform.position).normalized;
                bullet.rigidbody2d.linearVelocity = dir*6;
                StartCoroutine(ResetCooldown());
            }
            else
            {
                Debug.Log("Bullet es null");
                StartCoroutine(Reload());
            }
            
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
