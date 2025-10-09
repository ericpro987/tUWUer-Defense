using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool available;
    public Rigidbody2D rigidbody2d;
    // public Rigidbody2D rb => rigidbody2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        available = true;

    }
    private void OnEnable()
    {
       // StartCoroutine(Die());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
}
