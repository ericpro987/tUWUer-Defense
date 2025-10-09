using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public bool Jugador;

    public Transform[] spawnPoints;

    public Transform[] spawnPointsB;

    public GameObject torre1;

    public int coins;
    public int coinsB;

    public int generatorLevel;
    public int generatorLevelB;

    public int ATKBoosterLevel;
    public int ATKBoosterLevelB;

    public int SPDBoosterLevel;
    public int SPDBoosterLevelB;

    public int HPBoosterLevel;
    public int HPBoosterLevelB;

    public List<GameObject> Structures;

    InputSystem_Actions actions;
    public int JugadorMoney;

    public GameObject bola;

    private void Awake()
    {
        actions = new InputSystem_Actions();
        actions.Torre.Num1.started += num1;
        actions.Torre.Enable();


        actions.Torre.Num1B.started += num1B;
        actions.Torre.Enable();


        actions.Torre.Num2.started += num2;
        actions.Torre.Enable();


        actions.Torre.Num2B.started += num2B;
        actions.Torre.Enable();


        actions.Torre.Num3.started += num3;
        actions.Torre.Enable();


        actions.Torre.Num3B.started += num3B;
        actions.Torre.Enable();

    }

    public void num1(InputAction.CallbackContext context)
    {
    Instantiate(bola, spawnPoints[0].position, spawnPoints[0].rotation);
    }

    public void num1B(InputAction.CallbackContext context)
    {
        Instantiate(bola, spawnPointsB[0].position, spawnPointsB[0].rotation);
    }

    public void num2(InputAction.CallbackContext context)
    {
        Instantiate(bola, spawnPoints[1].position, spawnPoints[1].rotation);
    }


    public void num2B(InputAction.CallbackContext context)
    {
        Instantiate(bola, spawnPointsB[1].position, spawnPointsB[1].rotation);
    }


    public void num3(InputAction.CallbackContext context)
    {
        Instantiate(bola, spawnPoints[2].position, spawnPoints[2].rotation);
    }


    public void num3B(InputAction.CallbackContext context)
    {
        Instantiate(bola, spawnPointsB[2].position, spawnPointsB[2].rotation);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        
    }

    

}
