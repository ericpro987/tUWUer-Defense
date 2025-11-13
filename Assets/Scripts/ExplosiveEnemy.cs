using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveEnemy : MonoBehaviour
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
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject tower;
    [SerializeField]
    private string tagEnemy;
    [SerializeField]
    private ParticleCollider ps;



    [SerializeField]
    private GameObject torre1;

    [SerializeField]
    private GameObject torre2;


    private void Awake()
    {
        hp_max = 10;
        hp = hp_max;
        atk = 1;
        spd = 3;
        rb = GetComponent<Rigidbody2D>();
        rangeDetection.OnEnter += Explode;


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


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            gameManager.AddIntoList(this.gameObject);

    }
    // Update is called once per frame
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
    void Explode(GameObject go)
    {
        ps.transform.parent = null;
        ps.PlayAnimation();
        Destroy(this.gameObject);
    }
    public void ReceiveDamage(int atk)
    {
        this.hp -= atk;
        if (this.hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        gameManager.RemoveOfList(this.gameObject);
    }
}
