using System.Collections;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{

    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {

        // Ejemplo: aplicar daño si el objeto tiene cierto tag
        if (other.CompareTag("J1"))
        {
            Destroy(other.gameObject);
            // Podés llamar a un método del enemigo, por ejemplo
        }
    }
    public void PlayAnimation()
    {
        ps.Play();
        StartCoroutine(EndAnimation());
    }
    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

}