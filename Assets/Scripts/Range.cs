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
        Debug.Log($"El enemigo: {collision.gameObject.name}");
        if (collision.TryGetComponent<MagoOscuro>(out MagoOscuro magoOscuro))
        {
            if (!magoOscuro.CompareTag(magoOscuro.tag)) 
             OnEnter?.Invoke(collision.gameObject);
        }
        else
        {
            OnEnter?.Invoke(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"El enemigo: {collision.gameObject.name}");
        OnStay?.Invoke(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"El enemigo: {collision.gameObject.name}");
        if (collision.TryGetComponent<MagoOscuro>(out MagoOscuro magoOscuro))
        {
            if (!magoOscuro.CompareTag(magoOscuro.tag))
                OnEnter?.Invoke(collision.gameObject);
        }
        else
        {
            OnEnter?.Invoke(collision.gameObject);
        }
    }
}
