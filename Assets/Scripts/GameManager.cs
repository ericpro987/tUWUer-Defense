using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> playersJ1;
    private List<GameObject> playersJ2;

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
        if (go.CompareTag("J1"))
        {
            playersJ1.Add(go);
        }
        else if (go.CompareTag("J2"))
        {
            playersJ2.Add(go);
        }
    }
    public void RemoveOfList(GameObject go)
    {
        if (go.CompareTag("J2"))
        {
            NotifyPlayers(playersJ1, go);
        }
        else if (go.CompareTag("J1"))
        {
            NotifyPlayers(playersJ2, go);
        }
    }

    private void NotifyPlayers(List<GameObject> players, GameObject go)
    {
        foreach (var player in players)
        {
            if (player.TryGetComponent<BouncingEnemy>(out BouncingEnemy bouncingEnemy))
            {
                bouncingEnemy.RemoveFromList(go);
            }
            else if (player.TryGetComponent<MagoOscuro>(out MagoOscuro magoOscuro))
            {
                magoOscuro.RemoveFromList(go);
            }
        }
    }
}
