using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> playersJ1;
    List<GameObject> playersJ2;

    private void Awake()
    {
        playersJ1 = new List<GameObject>();
        playersJ2 = new List<GameObject>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddIntoList(GameObject go)
    {
        if (go.tag == "J1")
        {
            playersJ1.Add(go);
        }
        else if (go.tag == "J2")
        {
            playersJ2.Add(go);
        }
    }
    public void RemoveOfList(GameObject go)
    {
        if (go.tag == "J2")
        {
            for (int i = 0; i < playersJ1.Count; i++)
            {
                if (playersJ1.ElementAt(i).TryGetComponent<BouncingEnemy>(out BouncingEnemy e))
                {
                    e.RemoveFromList(go);
                }
                else if (playersJ1.ElementAt(i).TryGetComponent<MagoOscuro>(out MagoOscuro mo))
                {
                    mo.RemoveFromList(go);
                }
            }
        }
        else
        {
            for (int i = 0; i < playersJ2.Count; i++)
            {
                if (playersJ2.ElementAt(i).TryGetComponent<BouncingEnemy>(out BouncingEnemy e))
                {
                    e.RemoveFromList(go);
                }
                else if (playersJ2.ElementAt(i).TryGetComponent<MagoOscuro>(out MagoOscuro mo))
                {
                    mo.RemoveFromList(go);
                }
            }
        }
    }
}
