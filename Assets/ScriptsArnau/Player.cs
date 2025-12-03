using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{

    [SerializeField]
    private bool Jugador;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private Transform[] spawnPointsB;

    [SerializeField]
    private GameObject torre1;

    [Header("Player's Coins")]
    [SerializeField]
    private int coins;
    public int coinJ1 => coins;
    [SerializeField]
    private int coinsB;
    public int coinJ2 => coinsB;

    [Header("Player's selected troop")]
    [SerializeField]
    private int selectedTroop;
    [SerializeField]
    private int selectedTroopB;

    [Header("Coin generators")]
    [SerializeField]
    private GameObject generator;
    [SerializeField]
    private GameObject generatorB;
    [SerializeField]
    private int generatorLevel;
    public int pubGeneratorLevel => generatorLevel;

    [SerializeField]
    private int generatorLevelB;
    public int pubGeneratorLevel2 => generatorLevelB;

    [Header("Attack booster")]
    [SerializeField]
    private GameObject ATKBooster;
    [SerializeField]
    private GameObject ATKBoosterB;
    [SerializeField]
    private int ATKBoosterLevel;
    public int pubATKBoosterLevel => ATKBoosterLevel;
    [SerializeField]
    private int ATKBoosterLevelB;
    public int pubATKBoosterLevel2 => ATKBoosterLevelB;

    [Header("Speed booster")]
    [SerializeField]
    private GameObject SPBooster;
    [SerializeField]
    private GameObject SPBoosterB;
    [SerializeField]
    private int SPDBoosterLevel;
    public int pubSPBooster => SPDBoosterLevel;
    [SerializeField]
    private int SPDBoosterLevelB;
    public int pubSPBooster2 => SPDBoosterLevelB;

    [Header("Health Points booster")]
    [SerializeField]
    private GameObject HPBooster;
    [SerializeField]
    private GameObject HPBoosterB;
    [SerializeField]
    private int HPBoosterLevel;
    public int pubHPBoosterLevel => HPBoosterLevel;
    [SerializeField]
    private int HPBoosterLevelB;
    public int pubHPBoosterLevel2 => HPBoosterLevelB;
    [Header("Structures")]
    [SerializeField]
    private List<GameObject> Structures;

    [Header("Input system")]
    private InputSystem_Actions actions;
    [SerializeField]
    private int JugadorMoney;

    [SerializeField]
    private GameObject bola;
    [SerializeField]
    private GameObject bola2;
    [SerializeField]
    private GameObject bola3;
    [SerializeField]
    private GameObject bola4;

    [SerializeField]
    private GameObject bola5;
    [SerializeField]
    private GameObject bola6;
    [SerializeField]
    private GameObject bola7;
    [SerializeField]
    private GameObject bola8;

    [SerializeField]
    private int CartaPreuA;
    [SerializeField]
    private int CartaPreuB;
    [SerializeField]
    private int CartaPreuC;
    [SerializeField]
    private int CartaPreuD;



    [SerializeField]
    private ParticleSystem coinEffect;
    [SerializeField]
    private ParticleSystem ATKEffect;

    [Header("Coins")]
    [SerializeField]
    public GameObject[] mejorasPrecioA;

    [SerializeField]
    public GameObject[] mejorasPrecioB;


    public static event Action<int> OnGeneratorLevelChangedJ1;
    public static event Action<int> OnGeneratorLevelChangedJ2;
    public static event Action<int> OnATKLevelChangedJ1;
    public static event Action<int> OnATKLevelChangedJ2;
    public static event Action<int> OnSPDLevelChangedJ1;
    public static event Action<int> OnSPDLevelChangedJ2;
    public static event Action<int> OnHPLevelChangedJ1;
    public static event Action<int> OnHPLevelChangedJ2;


    public void SetCoinsJ1(int coins)
    {
        this.coins = coins;
    }
    public void SetCoinsJ2(int coins)
    {
        this.coinsB = coins;
    }
    public void SetGeneratorLvlJ1(int lvl)
    {
        this.generatorLevel = lvl;
    }
    public void SetGeneratorLvlJ2(int lvl)
    {
        this.generatorLevelB = lvl;
    }
    public void SetHPBoosterLvlJ1(int lvl)
    {
        this.HPBoosterLevel = lvl;
    }
    public void SetHPBoosterLvlJ2(int lvl)
    {
        this.HPBoosterLevelB = lvl;
    }
    public void SetSPDBoosterLvlJ1(int lvl)
    {
        this.SPDBoosterLevel = lvl;
    }
    public void SetSPDBoosterLvlJ2(int lvl)
    {
        this.SPDBoosterLevelB = lvl;
    }
    public void SetATKBoosterLvlJ1(int lvl)
    {
        this.ATKBoosterLevel = lvl;
    }
    public void SetATKBoosterLvlJ2(int lvl)
    {
        this.ATKBoosterLevelB = lvl;
    }


    private GameObject[] bolasJ1;
    private GameObject[] bolasJ2;
    private int[] costes;


    public static event Action<int> OnCoinsChangedJ1;
    public static event Action<int> OnCoinsChangedJ2;

    public static event Action<string> OnLogMessageJ1;
    public static event Action<string> OnLogMessageJ2;

    public static event Action<int> OnSelectedTroopChangedJ1;
    public static event Action<int> OnSelectedTroopChangedJ2;


    public static Player Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        actions = new InputSystem_Actions();
        actions.Torre.Num1.started += num1;
        actions.Torre.Num1B.started += num1B;
        actions.Torre.Num2.started += num2;
        actions.Torre.Num2B.started += num2B;
        actions.Torre.Num3.started += num3;
        actions.Torre.Num3B.started += num3B;
        actions.Torre.Num4.started += num4;
        actions.Torre.Num4B.started += num4B;
        actions.Torre.Num5.started += num5;
        actions.Torre.Num5B.started += num5B;
        actions.Torre.Num6.started += num6;
        actions.Torre.Num6B.started += num6B;
        actions.Torre.Num7.started += num7;
        actions.Torre.Num7B.started += num7B;
        actions.Torre.Num8.started += num8;
        actions.Torre.Num8B.started += num8B;

        actions.Torre.W.started += W;
        actions.Torre.A.started += A;
        actions.Torre.S.started += S;
        actions.Torre.D.started += D;

        actions.Torre.Up.started += Up;
        actions.Torre.Down.started += Down;
        actions.Torre.Left.started += Left;
        actions.Torre.Right.started += Right;
        actions.Torre.Enable();
    }

    public void enableActions()
    {
        actions.Torre.Enable();
    }

    public void disableActions()
    {
        actions.Torre.Disable();
    }

    public void A(InputAction.CallbackContext c) => TrySpawn(false, selectedTroop, 0);
    public void S(InputAction.CallbackContext c) => TrySpawn(false, selectedTroop, 1);
    public void D(InputAction.CallbackContext c) => TrySpawn(false, selectedTroop, 2);
    public void W(InputAction.CallbackContext c) => TrySpawn(false, selectedTroop, 3);

    public void Left(InputAction.CallbackContext c) => TrySpawn(true, selectedTroopB, 0);
    public void Down(InputAction.CallbackContext c) => TrySpawn(true, selectedTroopB, 1);
    public void Right(InputAction.CallbackContext c) => TrySpawn(true, selectedTroopB, 2);
    public void Up(InputAction.CallbackContext c) => TrySpawn(true, selectedTroopB, 3);


    public void num5(InputAction.CallbackContext context)
    {
        
        if (coins >= CartaPreuC)
        {
            generatorLevel++;
            coins-= CartaPreuC;
            OnGeneratorLevelChangedJ1?.Invoke(generatorLevel);
        }
        else
        {
            Debug.Log("No tens suficients coins");
            OnLogMessageJ1?.Invoke("No tens suficients coins");

        }
    }


    public void num5B(InputAction.CallbackContext context)
    {
       

        if (coinsB >= CartaPreuC)
        {
            generatorLevelB++;
            OnGeneratorLevelChangedJ2?.Invoke(generatorLevelB);
            coinsB -= CartaPreuC;
        }
        else
        {
            Debug.Log("No tens suficients coins");
            OnLogMessageJ2?.Invoke("No tens suficients coins");
        }
    }


    public void num6(InputAction.CallbackContext context)
    {
        
        if (coins >= CartaPreuC)
        {
            ATKBoosterLevel++;
            coins -= CartaPreuC;
            OnATKLevelChangedJ1?.Invoke(ATKBoosterLevel);
        }
        else
        {
            Debug.Log("No tens suficients coins");
            OnLogMessageJ1?.Invoke("No tens suficients coins");
        }
        if (ATKBoosterLevel == 1)
        {
            Instantiate(ATKEffect, ATKBooster.transform.position, ATKBooster.transform.rotation);
        }
    }


    public void num6B(InputAction.CallbackContext context)
    {
        if (coinsB >= CartaPreuC)
        {
            ATKBoosterLevelB++;
            OnATKLevelChangedJ2?.Invoke(ATKBoosterLevelB);
            coinsB -= CartaPreuC;
        }
        else
        {
            Debug.Log("No tens suficients coins");
            OnLogMessageJ2?.Invoke("No tens suficients coins");
        }
        if (ATKBoosterLevelB == 1)
        {
            Instantiate(ATKEffect, ATKBoosterB.transform.position, ATKBoosterB.transform.rotation);
        }
    }


    public void num7(InputAction.CallbackContext context)
    {
       
        if (coins >= CartaPreuC)
        {
            SPDBoosterLevel++;
            OnSPDLevelChangedJ1?.Invoke(SPDBoosterLevel);
            coins -= CartaPreuC;
        }
        else
        {
            Debug.Log("No tens suficients coins");
            OnLogMessageJ1?.Invoke("No tens suficients coins");

        }
        if (SPDBoosterLevel == 1)
        {
            Instantiate(ATKEffect, SPBooster.transform.position, SPBooster.transform.rotation);
        }
    }


    public void num7B(InputAction.CallbackContext context)
    {
        if (coinsB >= CartaPreuC)
        {
            SPDBoosterLevelB++;
            OnSPDLevelChangedJ2?.Invoke(SPDBoosterLevelB);
            coinsB -= CartaPreuC;
        }
        else
        {
            Debug.Log("No tens suficients coins");
            OnLogMessageJ2?.Invoke("No tens suficients coins");
        }
        if (SPDBoosterLevelB == 1)
        {
            Instantiate(ATKEffect, SPBoosterB.transform.position, SPBoosterB.transform.rotation);
        }
    }


    public void num8(InputAction.CallbackContext context)
    {
       
        if (coins >= CartaPreuC)
        {
            HPBoosterLevel++;
            OnHPLevelChangedJ1?.Invoke(HPBoosterLevel);
            coins -= CartaPreuC;
        }
        else
        {
            Debug.Log("No tens suficients coins");
            OnLogMessageJ1?.Invoke("No tens suficients coins");
        }
        if (HPBoosterLevel == 1)
        {
            Instantiate(ATKEffect, HPBooster.transform.position, HPBooster.transform.rotation);
        }
    }


    public void num8B(InputAction.CallbackContext context)
    {
        if (coinsB >= CartaPreuC)
        {
            HPBoosterLevelB++;
            OnHPLevelChangedJ2?.Invoke(HPBoosterLevelB);
            coinsB -= CartaPreuC;
        }
        else
        {
            Debug.Log("No tens suficients coins");
            OnLogMessageJ2?.Invoke("No tens suficients coins");
        }
        if (HPBoosterLevelB == 1)
        {
            Instantiate(ATKEffect, HPBoosterB.transform.position, HPBoosterB.transform.rotation);
        }
    }

    public void num1(InputAction.CallbackContext context)
    {
        if (selectedTroop == 1)
            return;

        selectedTroop = 1;
        OnSelectedTroopChangedJ1?.Invoke(0);
        OnLogMessageJ1?.Invoke("");
    }


    public void num2(InputAction.CallbackContext context)
    {
        if (selectedTroop == 2)
            return;

        selectedTroop = 2;
        OnSelectedTroopChangedJ1?.Invoke(1);
        OnLogMessageJ1?.Invoke("");
    }


    public void num3(InputAction.CallbackContext context)
    {
        if (selectedTroop == 3)
            return;

        selectedTroop = 3;
        OnSelectedTroopChangedJ1?.Invoke(2);
        OnLogMessageJ1?.Invoke("");
    }


    public void num4(InputAction.CallbackContext context)
    {
        if (selectedTroop == 4)
            return;

        selectedTroop = 4;
        OnSelectedTroopChangedJ1?.Invoke(3);
        OnLogMessageJ1?.Invoke("");
    }


    public void num1B(InputAction.CallbackContext context)
    {
        if (selectedTroopB == 1)
            return;

        selectedTroopB = 1;
        OnSelectedTroopChangedJ2?.Invoke(0);
        OnLogMessageJ2?.Invoke("");
    }


    public void num2B(InputAction.CallbackContext context)
    {
        if (selectedTroopB == 2)
            return;

        selectedTroopB = 2;
        OnSelectedTroopChangedJ2?.Invoke(1);
        OnLogMessageJ2?.Invoke("");
    }

    public void num3B(InputAction.CallbackContext context)
    {
        if (selectedTroopB == 3)
            return;

        selectedTroopB = 3;
        OnSelectedTroopChangedJ2?.Invoke(2);
        OnLogMessageJ2?.Invoke("");
    }

    public void num4B(InputAction.CallbackContext context)
    {
        if (selectedTroopB == 4)
            return;

        selectedTroopB = 4;
        OnSelectedTroopChangedJ2?.Invoke(3);
        OnLogMessageJ2?.Invoke("");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CartaPreuA = 2;
        CartaPreuB = 4;
        CartaPreuC = 6;
        CartaPreuD = 8;

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

        

        StartCoroutine(generate());
        StartCoroutine(generateB());

        bolasJ1 = new GameObject[] { bola, bola2, bola3, bola4 };
        bolasJ2 = new GameObject[] { bola5, bola6, bola7, bola8 };
        costes = new int[] { CartaPreuA, CartaPreuB, CartaPreuC, CartaPreuD };

        
    }

    void TrySpawn(bool isPlayerB, int selectedTroop, int laneIndex)
    {
       
        if (selectedTroop == 0)
        {
            if (!isPlayerB)
            OnLogMessageJ1?.Invoke("Cap tropa seleccionada");

            else
                OnLogMessageJ2?.Invoke("Cap tropa seleccionada");
            return;
        }

        int troopIndex = selectedTroop - 1;

       
        int coste = costes[troopIndex];
        
        int coinsRef;
        if (!isPlayerB)
            coinsRef = coins;
        else
            coinsRef = coinsB;

        if (coinsRef < coste)
        {
            if (!isPlayerB)
                OnLogMessageJ1?.Invoke("No tens suficients coins");
            else
                OnLogMessageJ2?.Invoke("No tens suficients coins");
            return;
        }

      
        GameObject prefab;
        if (!isPlayerB)
            prefab = bolasJ1[troopIndex];
        else
            prefab = bolasJ2[troopIndex];

        
        Transform sp;
        if (!isPlayerB)
            sp = spawnPoints[laneIndex];
        else
            sp = spawnPointsB[laneIndex];

       
        GameObject newBola = Instantiate(prefab, sp.position, sp.rotation);

        int hpBoost;
        int atkBoost;
        int spdBoost;

        if (!isPlayerB)
        {
            hpBoost = HPBoosterLevel;
            atkBoost = ATKBoosterLevel;
            spdBoost = SPDBoosterLevel;
        }
        else
        {
            hpBoost = HPBoosterLevelB;
            atkBoost = ATKBoosterLevelB;
            spdBoost = SPDBoosterLevelB;
        }

        // les estats
        BasicEnemy enemy = newBola.GetComponent<BasicEnemy>();
        enemy.SetHp(enemy.hp + hpBoost);
        enemy.SetAtk(enemy.atk + atkBoost);
        enemy.SetSpd(enemy.spd + spdBoost);


        if (!isPlayerB)
        {
            coins -= coste;
            OnCoinsChangedJ1?.Invoke(coins);
            OnLogMessageJ1?.Invoke("");      
        }
        else
        {
            coinsB -= coste;
            OnCoinsChangedJ2?.Invoke(coinsB);
            OnLogMessageJ2?.Invoke("");
        }

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
OnCoinsChangedJ1?.Invoke(coins);
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
OnCoinsChangedJ2?.Invoke(coinsB);
        }
        
    }

    
    }