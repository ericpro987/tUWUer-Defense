using System;
using UnityEngine;

public class Range : MonoBehaviour
{
    public Action<GameObject> OnEnter;
    public Action<GameObject> OnStay;
    public Action<GameObject> OnExit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MagoOscuro>(out MagoOscuro magoOscuro))
        {
            if (!this.transform.parent.gameObject.CompareTag(collision.tag)) 
             OnEnter?.Invoke(collision.gameObject);
        }
        else if (collision.TryGetComponent<BouncingEnemy>(out BouncingEnemy bouncingEnemy))
        {
            if (!this.transform.parent.gameObject.CompareTag(bouncingEnemy.tag))
                OnEnter?.Invoke(collision.gameObject);
        }
        else if (collision.TryGetComponent<ExplosiveEnemy>(out ExplosiveEnemy explosiveEnemy))
        {
            if (!this.transform.parent.gameObject.CompareTag(explosiveEnemy.tag))
                OnEnter?.Invoke(collision.gameObject);
        }
        else
        {
            OnEnter?.Invoke(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MagoOscuro>(out MagoOscuro magoOscuro))
        {
            if (!this.transform.parent.gameObject.CompareTag(magoOscuro.tag))
                OnStay?.Invoke(collision.gameObject);
        }
        else if (collision.TryGetComponent<BouncingEnemy>(out BouncingEnemy bouncingEnemy))
        {
            if (!this.transform.parent.gameObject.CompareTag(bouncingEnemy.tag))
                OnStay?.Invoke(collision.gameObject);
        }else if (collision.TryGetComponent<ExplosiveEnemy>(out ExplosiveEnemy explosiveEnemy))
        {
            if (!this.transform.parent.gameObject.CompareTag(explosiveEnemy.tag))
                OnStay?.Invoke(collision.gameObject);
        }
        else
        {
            OnStay?.Invoke(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MagoOscuro>(out MagoOscuro magoOscuro))
        {
            if (!this.transform.parent.gameObject.CompareTag(magoOscuro.tag))
                OnExit?.Invoke(collision.gameObject);
        }
        else if (collision.TryGetComponent<BouncingEnemy>(out BouncingEnemy bouncingEnemy))
        {
            if (!this.transform.parent.gameObject.CompareTag(bouncingEnemy.tag))
                OnExit?.Invoke(collision.gameObject);
        }
        else if (collision.TryGetComponent<ExplosiveEnemy>(out ExplosiveEnemy explosiveEnemy))
        {
            if (!this.transform.parent.gameObject.CompareTag(explosiveEnemy.tag))
                OnExit?.Invoke(collision.gameObject);
        }
        else
        {
            OnExit?.Invoke(collision.gameObject);
        }
    }
}
