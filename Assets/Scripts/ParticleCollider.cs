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
        if (other.CompareTag("J1"))
        {
            Destroy(other.gameObject);
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