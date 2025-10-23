using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    public int selectedTroop;
    public int selectedTroopB;

    public GameObject generator;
    public GameObject generatorB;
    public int generatorLevel;
    public int generatorLevelB;

    public GameObject ATKBooster;
    public GameObject ATKBoosterB;
    public int ATKBoosterLevel;
    public int ATKBoosterLevelB;

    public GameObject SPBooster;
    public GameObject SPBoosterB;
    public int SPDBoosterLevel;
    public int SPDBoosterLevelB;

    public GameObject HPBooster;
    public GameObject HPBoosterB;
    public int HPBoosterLevel;
    public int HPBoosterLevelB;

    public List<GameObject> Structures;

    InputSystem_Actions actions;
    public int JugadorMoney;

    public GameObject bola;
    public GameObject bola2;
    public GameObject bola3;
    public GameObject bola4;

    public GameObject bola5;
    public GameObject bola6;
    public GameObject bola7;
    public GameObject bola8;


    public GameObject carta1;
    public GameObject carta2;
    public GameObject carta3;
    public GameObject carta4;

    public GameObject carta5;
    public GameObject carta6;
    public GameObject carta7;
    public GameObject carta8;

    public int CartaPreuA;
    public int CartaPreuB;
    public int CartaPreuC;
    public int CartaPreuD;



    public TextMeshProUGUI MONEDES;
    public TextMeshProUGUI MONEDESB;

    public ParticleSystem coinEffect;
    public ParticleSystem ATKEffect;

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




        actions.Torre.Num4.started += num4;
        actions.Torre.Enable();


        actions.Torre.Num4B.started += num4B;
        actions.Torre.Enable();


        actions.Torre.Num5.started += num5;
        actions.Torre.Enable();


        actions.Torre.Num5B.started += num5B;
        actions.Torre.Enable();


        actions.Torre.Num6.started += num6;
        actions.Torre.Enable();


        actions.Torre.Num6B.started += num6B;
        actions.Torre.Enable();


        actions.Torre.Num7.started += num7;
        actions.Torre.Enable();


        actions.Torre.Num7B.started += num7B;
        actions.Torre.Enable();


        actions.Torre.Num8.started += num8;
        actions.Torre.Enable();


        actions.Torre.Num8B.started += num8B;
        actions.Torre.Enable();


        actions.Torre.W.started += W;
        actions.Torre.Enable();

        actions.Torre.A.started += A;
        actions.Torre.Enable();

        actions.Torre.S.started += S;
        actions.Torre.Enable();

        actions.Torre.D.started += D;
        actions.Torre.Enable();

        actions.Torre.Up.started += Up;
        actions.Torre.Enable();

        actions.Torre.Down.started += Down;
        actions.Torre.Enable();

        actions.Torre.Left.started += Left;
        actions.Torre.Enable();

        actions.Torre.Right.started += Right;
        actions.Torre.Enable();


    }

    public void A(InputAction.CallbackContext context)
    {

        if (selectedTroop == 1)
        {
            if (coins >= CartaPreuA)
            {
                Instantiate(bola, spawnPoints[0].position, spawnPoints[0].rotation);
                coins -= CartaPreuA;
            }
            else
            {
                Debug.Log("not enough pasta");
            }
            
        } else if (selectedTroop == 2)
        {
            if (coins >= CartaPreuB)
            {
                Instantiate(bola2, spawnPoints[0].position, spawnPoints[0].rotation);
                coins -= CartaPreuB;
            }
            else
            {
                Debug.Log("not enough pasta");
            }
           
        }
        else if (selectedTroop == 3)
        {

            if (coins >= CartaPreuC)
            {
                Instantiate(bola3, spawnPoints[0].position, spawnPoints[0].rotation);
                coins -= CartaPreuC;
            }
            else
            {
                Debug.Log("not enough pasta");
            }
            
        }
        else if (selectedTroop == 4)
        {
            if (coins >= CartaPreuD)
            {
                Instantiate(bola4, spawnPoints[0].position, spawnPoints[0].rotation);
                coins -= CartaPreuD;
            }
            else
            {
                Debug.Log("not enough pasta");
            }

            
        }
        else
        {
            Debug.Log("Cap tropa seleccionada");
        }
        


    }

    public void Left(InputAction.CallbackContext context)
    {
        


        if (selectedTroopB == 1)
        {
            if (coinsB >= CartaPreuA)
            {
                Instantiate(bola5, spawnPointsB[0].position, spawnPointsB[0].rotation);
                coinsB -= CartaPreuA;
            }
            else
            {
                Debug.Log("not enough pasta");
            }
            
        }
        else if (selectedTroopB == 2)
        {
            if (coinsB >= CartaPreuB)
            {
                Instantiate(bola6, spawnPointsB[0].position, spawnPointsB[0].rotation);
                coinsB -= CartaPreuB;
            }
            else
            {
                Debug.Log("not enough pasta");
            }

            
        }
        else if (selectedTroopB == 3)
        {

            if (coinsB >= CartaPreuC)
            {
                Instantiate(bola7, spawnPointsB[0].position, spawnPointsB[0].rotation);
                coinsB -= CartaPreuC;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


           
        }
        else if (selectedTroopB == 4)
        {

            if (coinsB >= CartaPreuD)
            {
                Instantiate(bola8, spawnPointsB[0].position, spawnPointsB[0].rotation);
                coinsB -= CartaPreuD;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else
        {
            Debug.Log("Cap tropa seleccionada");
        }
    }

    public void S(InputAction.CallbackContext context)
    {
        


        if (selectedTroop == 1)
        {

            if (coins >= CartaPreuA)
            {
                Instantiate(bola, spawnPoints[1].position, spawnPoints[1].rotation);
                coins -= CartaPreuA;
            }
            else
            {
                Debug.Log("not enough pasta");
            }

           
        }
        else if (selectedTroop == 2)
        {

            if (coins >= CartaPreuB)
            {
                Instantiate(bola2, spawnPoints[1].position, spawnPoints[1].rotation);
                coins -= CartaPreuB;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroop == 3)
        {

            if (coins >= CartaPreuC)
            {
                Instantiate(bola3, spawnPoints[1].position, spawnPoints[1].rotation);
                coins -= CartaPreuC;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


           
        }
        else if (selectedTroop == 4)
        {

            if (coins >= CartaPreuD)
            {
                Instantiate(bola4, spawnPoints[1].position, spawnPoints[1].rotation);
                coins -= CartaPreuD;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


           
        }
        else
        {
            Debug.Log("Cap tropa seleccionada");
        }
    }


    public void Down(InputAction.CallbackContext context)
    {
        


        if (selectedTroopB == 1)
        {

            if (coinsB >= CartaPreuA)
            {
                Instantiate(bola5, spawnPointsB[1].position, spawnPointsB[1].rotation);
                coinsB -= CartaPreuA;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroopB == 2)
        {

            if (coinsB >= CartaPreuB)
            {
                Instantiate(bola6, spawnPointsB[1].position, spawnPointsB[1].rotation);
                coinsB -= CartaPreuB;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroopB == 3)
        {

            if (coinsB >= CartaPreuC)
            {
                Instantiate(bola7, spawnPointsB[1].position, spawnPointsB[1].rotation);
                coinsB -= CartaPreuC;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroopB == 4)
        {

            if (coinsB >= CartaPreuD)
            {
                Instantiate(bola8, spawnPointsB[1].position, spawnPointsB[1].rotation);
                coinsB -= CartaPreuD;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else
        {
            Debug.Log("Cap tropa seleccionada");
        }
    }


    public void D(InputAction.CallbackContext context)
    {
       


        if (selectedTroop == 1)
        {

            if (coins >= CartaPreuA)
            {
                Instantiate(bola, spawnPoints[2].position, spawnPoints[2].rotation);
                coins -= CartaPreuA;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroop == 2)
        {

            if (coins >= CartaPreuB)
            {
                Instantiate(bola2, spawnPoints[2].position, spawnPoints[2].rotation);
                coins -= CartaPreuB;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroop == 3)
        {

            if (coins >= CartaPreuC)
            {
                Instantiate(bola3, spawnPoints[2].position, spawnPoints[2].rotation);
                coins -= CartaPreuC;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroop == 4)
        {

            if (coins >= CartaPreuD)
            {
                Instantiate(bola4, spawnPoints[2].position, spawnPoints[2].rotation);
                coins -= CartaPreuD;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else
        {
            Debug.Log("Cap tropa seleccionada");
        }
    }


    public void Right(InputAction.CallbackContext context)
    {
       


        if (selectedTroopB == 1)
        {

            if (coinsB >= CartaPreuA)
            {
                Instantiate(bola5, spawnPointsB[2].position, spawnPointsB[2].rotation);
                coinsB -= CartaPreuA;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


           
        }
        else if (selectedTroopB == 2)
        {

            if (coinsB >= CartaPreuB)
            {
                Instantiate(bola6, spawnPointsB[2].position, spawnPointsB[2].rotation);
                coinsB -= CartaPreuB;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroopB == 3)
        {

            if (coinsB >= CartaPreuC)
            {
                Instantiate(bola7, spawnPointsB[2].position, spawnPointsB[2].rotation);
                coinsB -= CartaPreuC;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroopB == 4)
        {

            if (coinsB >= CartaPreuD)
            {
                Instantiate(bola8, spawnPointsB[2].position, spawnPointsB[2].rotation);
                coinsB -= CartaPreuD;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


           
        }
        else
        {
            Debug.Log("Cap tropa seleccionada");
        }
    }





    public void Up(InputAction.CallbackContext context)

    {
        if (selectedTroopB == 1)
        {


            if (coinsB >= CartaPreuA)
            {
                Instantiate(bola5, spawnPointsB[3].position, spawnPointsB[3].rotation);
                coinsB -= CartaPreuA;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


           
        }
        else if (selectedTroopB == 2)
        {

            if (coinsB >= CartaPreuB)
            {
                Instantiate(bola6, spawnPointsB[3].position, spawnPointsB[3].rotation);
                coinsB -= CartaPreuB;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroopB == 3)
        {

            if (coinsB >= CartaPreuC)
            {
                Instantiate(bola7, spawnPointsB[3].position, spawnPointsB[3].rotation);
                coinsB -= CartaPreuC;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroopB == 4)
        {

            if (coinsB >= CartaPreuD)
            {
                Instantiate(bola8, spawnPointsB[3].position, spawnPointsB[3].rotation);
                coinsB -= CartaPreuD;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else
        {
            Debug.Log("Cap tropa seleccionada");
        }
        
    }

   public void W(InputAction.CallbackContext context)
    {

        if (selectedTroop == 1)
        {

            if (coins >= CartaPreuA)
            {
                Instantiate(bola, spawnPoints[3].position, spawnPoints[3].rotation);
                coins -= CartaPreuA;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroop == 2)
        {

            if (coins >= CartaPreuB)
            {
                Instantiate(bola2, spawnPoints[3].position, spawnPoints[3].rotation);
                coins -= CartaPreuB;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


           
        }
        else if (selectedTroop == 3)
        {

            if (coins >= CartaPreuC)
            {
                Instantiate(bola3, spawnPoints[3].position, spawnPoints[3].rotation);
                coins -= CartaPreuC;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else if (selectedTroop == 4)
        {

            if (coins >= CartaPreuD)
            {
                Instantiate(bola4, spawnPoints[3].position, spawnPoints[3].rotation);
                coins -= CartaPreuD;
            }
            else
            {
                Debug.Log("not enough pasta");
            }


            
        }
        else
        {
            Debug.Log("Cap tropa seleccionada");
        }


        
    }

    public void num5(InputAction.CallbackContext context)
    {
        generatorLevel++;
    }


    public void num5B(InputAction.CallbackContext context)
    {
        generatorLevelB++;
    }


    public void num6(InputAction.CallbackContext context)
    {
        ATKBoosterLevel++;
        if (ATKBoosterLevel == 1)
        {
            Instantiate(ATKEffect, ATKBooster.transform.position, ATKBooster.transform.rotation);
        }
    }


    public void num6B(InputAction.CallbackContext context)
    {
        ATKBoosterLevelB++;
        if (ATKBoosterLevelB == 1)
        {
            Instantiate(ATKEffect, ATKBoosterB.transform.position, ATKBoosterB.transform.rotation);
        }
    }


    public void num7(InputAction.CallbackContext context)
    {
       SPDBoosterLevel++;
        if (SPDBoosterLevel == 1)
        {
            Instantiate(ATKEffect, SPBooster.transform.position, SPBooster.transform.rotation);
        }
    }


    public void num7B(InputAction.CallbackContext context)
    {
        SPDBoosterLevelB++;
        if (SPDBoosterLevelB == 1)
        {
            Instantiate(ATKEffect, SPBoosterB.transform.position, SPBoosterB.transform.rotation);
        }
    }


    public void num8(InputAction.CallbackContext context)
    {
        HPBoosterLevel++;
        if (HPBoosterLevel == 1)
        {
            Instantiate(ATKEffect, HPBooster.transform.position, HPBooster.transform.rotation);
        }
    }


    public void num8B(InputAction.CallbackContext context)
    {
        HPBoosterLevelB++;
        if (HPBoosterLevelB == 1)
        {
            Instantiate(ATKEffect, HPBoosterB.transform.position, HPBoosterB.transform.rotation);
        }
    }
    //   square1.GetComponent<Renderer>().material.color = Color.red;
    //el quadrat concret puja o baixa

    public void num1(InputAction.CallbackContext context)
    {
        if (selectedTroop == 1)
        {
            Debug.Log("Troop already selected");
            return;
        } if (selectedTroop == 2)
        {
            carta2.transform.position += new Vector3(0, 0.2f, 0);
        } if (selectedTroop == 3)
        {
            carta3.transform.position += new Vector3(0, 0.2f, 0);
        } if (selectedTroop == 4)
        {
            carta4.transform.position += new Vector3(0, 0.2f, 0);
        }

        selectedTroop = 1;
    

        carta1.transform.position += new Vector3(0, -0.2f, 0);
    }

    public void num2(InputAction.CallbackContext context)
    {
        if (selectedTroop == 2)
        {
            Debug.Log("Troop already selected");
            return;
        }
        if (selectedTroop == 1)
        {
            carta1.transform.position += new Vector3(0, 0.2f, 0);
        }
        if (selectedTroop == 3)
        {
            carta3.transform.position += new Vector3(0, 0.2f, 0);
        }
        if (selectedTroop == 4)
        {
            carta4.transform.position += new Vector3(0, 0.2f, 0);
        }

        selectedTroop = 2;


        carta2.transform.position += new Vector3(0, -0.2f, 0);
    }

    public void num3(InputAction.CallbackContext context)
    {
        if (selectedTroop == 3)
        {
            Debug.Log("Troop already selected");
            return;
        }
        if (selectedTroop == 2)
        {
            carta2.transform.position += new Vector3(0, 0.2f, 0);
        }
        if (selectedTroop == 1)
        {
            carta1.transform.position += new Vector3(0, 0.2f, 0);
        }
        if (selectedTroop == 4)
        {
            carta4.transform.position += new Vector3(0, 0.2f, 0);
        }

        selectedTroop = 3;


        carta3.transform.position += new Vector3(0, -0.2f, 0);
    }

    public void num4(InputAction.CallbackContext context)
    {
        if (selectedTroop == 4)
        {
            Debug.Log("Troop already selected");
            return;
        }
        if (selectedTroop == 2)
        {
            carta2.transform.position += new Vector3(0, 0.2f, 0);
        }
        if (selectedTroop == 3)
        {
            carta3.transform.position += new Vector3(0, 0.2f, 0);
        }
        if (selectedTroop == 1)
        {
            carta1.transform.position += new Vector3(0, 0.2f, 0);
        }

        selectedTroop = 4;


        carta4.transform.position += new Vector3(0, -0.2f, 0);
    }

    public void num1B(InputAction.CallbackContext context)
    {
        if (selectedTroopB == 1)
        {
            Debug.Log("Troop already selected");
            return;
        }
        if (selectedTroopB == 2)
        {
            carta6.transform.position += new Vector3(0, -0.2f, 0);
        }
        if (selectedTroopB == 3)
        {
            carta7.transform.position += new Vector3(0, -0.2f, 0);
        }
        if (selectedTroopB == 4)
        {
            carta8.transform.position += new Vector3(0, -0.2f, 0);
        }

        selectedTroopB = 1;


        carta5.transform.position += new Vector3(0, 0.2f, 0);
    }

    public void num2B(InputAction.CallbackContext context)
    {
        if (selectedTroopB == 2)
        {
            Debug.Log("Troop already selected");
            return;
        }
        if (selectedTroopB == 1)
        {
            carta5.transform.position += new Vector3(0, -0.2f, 0);
        }
        if (selectedTroopB == 3)
        {
            carta7.transform.position += new Vector3(0, -0.2f, 0);
        }
        if (selectedTroopB == 4)
        {
            carta8.transform.position += new Vector3(0, -0.2f, 0);
        }

        selectedTroopB = 2;


        carta6.transform.position += new Vector3(0, 0.2f, 0);
    }

    public void num3B(InputAction.CallbackContext context)
    {
        if (selectedTroopB == 3)
        {
            Debug.Log("Troop already selected");
            return;
        }
        if (selectedTroopB == 1)
        {
            carta5.transform.position += new Vector3(0, -0.2f, 0);
        }
        if (selectedTroopB == 2)
        {
            carta6.transform.position += new Vector3(0, -0.2f, 0);
        }
        if (selectedTroopB == 4)
        {
            carta8.transform.position += new Vector3(0, -0.2f, 0);
        }

        selectedTroopB = 3;


        carta7.transform.position += new Vector3(0, 0.2f, 0);
    }

    public void num4B(InputAction.CallbackContext context)
    {
        if (selectedTroopB == 4)
        {
            Debug.Log("Troop already selected");
            return;
        }
        if (selectedTroopB == 1)
        {
            carta5.transform.position += new Vector3(0, -0.2f, 0);
        }
        if (selectedTroopB == 3)
        {
            carta7.transform.position += new Vector3(0, -0.2f, 0);
        }
        if (selectedTroopB == 2)
        {
            carta6.transform.position += new Vector3(0, -0.2f, 0);
        }

        selectedTroopB = 4;


        carta8.transform.position += new Vector3(0, 0.2f, 0);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generatorLevel = 1;
        generatorLevelB = 1;
        ATKBoosterLevel = 0;
        ATKBoosterLevelB = 0;
        SPDBoosterLevel = 0;
        SPDBoosterLevelB = 0;
        HPBoosterLevel = 0;
        HPBoosterLevelB = 0;


        coins = 20;
        coinsB = 20;


        selectedTroop = 0;
        selectedTroopB = 0;

        CartaPreuA = 2;
        CartaPreuB = 4;
        CartaPreuC = 6;
        CartaPreuD = 8;

    
       

        StartCoroutine(generate());
        StartCoroutine(generateB());
        
    }


    

    IEnumerator generate()
    {
        while (true)
        {
            coins++;
            Debug.Log("Coins: " + coins);
            Instantiate(coinEffect, generator.transform.position, generator.transform.rotation);

            if (generatorLevel == 1)
            {
                yield return new WaitForSeconds(3);
            }
            else if (generatorLevel == 2)
            {
                yield return new WaitForSeconds(2);
            }
            else if (generatorLevel == 3)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log("you hacker");
            }

        }
    }

    IEnumerator generateB()
    {
        while (true)
        {
            coinsB++;
            Debug.Log("Coins: " + coinsB);
            Instantiate(coinEffect, generatorB.transform.position, generatorB.transform.rotation);

            if (generatorLevelB == 1)
            {
                yield return new WaitForSeconds(3);
            }
            else if (generatorLevelB == 2)
            {
                yield return new WaitForSeconds(2);
            }
            else if (generatorLevelB == 3)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log("you hacker");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        
            
        

        if (coins >= CartaPreuA)
        {
            carta1.GetComponent<Renderer>().material.color = Color.red;

        }
        else
        {
            carta1.GetComponent<Renderer>().material.color = Color.gray;
        }

        if (coins >= CartaPreuB)
        {
            carta2.GetComponent<Renderer>().material.color = Color.red;

        }
        else
        {
            carta2.GetComponent<Renderer>().material.color = Color.gray;
        }

        if (coins >= CartaPreuC)
        {
            carta3.GetComponent<Renderer>().material.color = Color.red;

        }
        else
        {
            carta3.GetComponent<Renderer>().material.color = Color.gray;
        }

        if (coins >= CartaPreuD)
        {
            carta4.GetComponent<Renderer>().material.color = Color.red;

        }
        else
        {
            carta4.GetComponent<Renderer>().material.color = Color.gray;
        }



        if (coinsB >= CartaPreuA)
        {
            carta5.GetComponent<Renderer>().material.color = Color.blue;

        }
        else
        {
            carta5.GetComponent<Renderer>().material.color = Color.gray;
        }

        if (coinsB >= CartaPreuB)
        {
            carta6.GetComponent<Renderer>().material.color = Color.blue;

        }
        else
        {
            carta6.GetComponent<Renderer>().material.color = Color.gray;
        }

        if (coinsB >= CartaPreuC)
        {
            carta7.GetComponent<Renderer>().material.color = Color.blue;

        }
        else
        {
            carta7.GetComponent<Renderer>().material.color = Color.gray;
        }

        if (coinsB >= CartaPreuD)
        {
            carta8.GetComponent<Renderer>().material.color = Color.blue;

        }
        else
        {
            carta8.GetComponent<Renderer>().material.color = Color.gray;
        }


        MONEDES.text = coins + " coins";
        MONEDESB.text = coinsB + " coins";

        if (generatorLevel == 0)
        {
            generator.GetComponent<Renderer>().material.color = Color.gray;
        }
     else if(generatorLevel == 1)
        {
            generator.GetComponent<Renderer>().material.color = Color.green;
        }
     else if (generatorLevel == 2)
        {
            generator.GetComponent<Renderer>().material.color = Color.yellow;
        }
     else if (generatorLevel == 3)
        {
            generator.GetComponent<Renderer>().material.color = Color.red;
        }
     else if( generatorLevel >= 4)
        {
            generatorLevel = 3;
        }


        if (generatorLevelB == 0)
        {
            generatorB.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (generatorLevelB == 1)
        {
            generatorB.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (generatorLevelB == 2)
        {
            generatorB.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (generatorLevelB == 3)
        {
            generatorB.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (generatorLevelB >= 4)
        {
            generatorLevelB = 3;
        }




        if (ATKBoosterLevel == 0)
        {
            ATKBooster.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (ATKBoosterLevel == 1)
        {
            
            ATKBooster.GetComponent<Renderer>().material.color = Color.green;
            
        }
        else if (ATKBoosterLevel == 2)
        {
            ATKBooster.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (ATKBoosterLevel == 3)
        {
            ATKBooster.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (ATKBoosterLevel >= 4)
        {
            ATKBoosterLevel = 3;
        }


        if (ATKBoosterLevelB == 0)
        {
            ATKBoosterB.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (ATKBoosterLevelB == 1)
        {
            
            ATKBoosterB.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (ATKBoosterLevelB == 2)
        {
            ATKBoosterB.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (ATKBoosterLevelB == 3)
        {
            ATKBoosterB.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (ATKBoosterLevelB >= 4)
        {
            ATKBoosterLevelB = 3;
        }









        if (SPDBoosterLevel == 0)
        {
            SPBooster.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (SPDBoosterLevel == 1)
        {
           
            SPBooster.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (SPDBoosterLevel == 2)
        {
            SPBooster.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (SPDBoosterLevel == 3)
        {
            SPBooster.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (SPDBoosterLevel >= 4)
        {
            SPDBoosterLevel = 3;
        }


        if (SPDBoosterLevelB == 0)
        {
            SPBoosterB.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (SPDBoosterLevelB == 1)
        {
           
            SPBoosterB.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (SPDBoosterLevelB == 2)
        {
            SPBoosterB.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (SPDBoosterLevelB == 3)
        {
            SPBoosterB.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (SPDBoosterLevelB >= 4)
        {
            SPDBoosterLevelB = 3;
        }









        if (HPBoosterLevel == 0)
        {
            HPBooster.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (HPBoosterLevel == 1)
        {
            HPBooster.GetComponent<Renderer>().material.color = Color.green;
           
        }
        else if (HPBoosterLevel == 2)
        {
            HPBooster.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (HPBoosterLevel == 3)
        {
            HPBooster.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (HPBoosterLevel >= 4)
        {
            HPBoosterLevel = 3;
        }


        if (HPBoosterLevelB == 0)
        {
            HPBoosterB.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (HPBoosterLevelB == 1)
        {
            HPBoosterB.GetComponent<Renderer>().material.color = Color.green;
           
        }
        else if (HPBoosterLevelB == 2)
        {
            HPBoosterB.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (HPBoosterLevelB == 3)
        {
            HPBoosterB.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (HPBoosterLevelB >= 4)
        {
            HPBoosterLevelB = 3;
        }


    }


    }

    


