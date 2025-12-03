using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Monedas")]
    [SerializeField] private TMP_Text monedasJ1;
    [SerializeField] private TMP_Text monedasJ2;

    [Header("Logs")]
    [SerializeField] private TMP_Text logsJ1;
    [SerializeField] private TMP_Text logsJ2;

    [Header("Cartas Jugador 1")]
    [SerializeField] private GameObject carta1;
    [SerializeField] private GameObject carta2;
    [SerializeField] private GameObject carta3;
    [SerializeField] private GameObject carta4;

    [Header("Cartas Jugador 2")]
    [SerializeField] private GameObject carta5;
    [SerializeField] private GameObject carta6;
    [SerializeField] private GameObject carta7;
    [SerializeField] private GameObject carta8;

    [Header("Mejoras Precio J1")]
    [SerializeField] private GameObject[] mejorasA;

    [Header("Mejoras Precio J2")]
    [SerializeField] private GameObject[] mejorasB;

    [Header("Boosters J1")]
    [SerializeField] private GameObject genJ1;
    [SerializeField] private GameObject atkJ1;
    [SerializeField] private GameObject spdJ1;
    [SerializeField] private GameObject hpJ1;

    [Header("Boosters J2")]
    [SerializeField] private GameObject genJ2;
    [SerializeField] private GameObject atkJ2;
    [SerializeField] private GameObject spdJ2;
    [SerializeField] private GameObject hpJ2;


    [SerializeField] private int[] preciosMejorasA = { 2, 4, 6, 8 };
    [SerializeField] private int[] preciosMejorasB = { 2, 4, 6, 8 };


    private Vector3[] cartasJ1OriginalPositions;
    private Vector3[] cartasJ2OriginalPositions;
    private void Start()
    {
        cartasJ1OriginalPositions = new Vector3[] { carta1.transform.position, carta2.transform.position, carta3.transform.position, carta4.transform.position };
        cartasJ2OriginalPositions = new Vector3[] { carta5.transform.position, carta6.transform.position, carta7.transform.position, carta8.transform.position };
    }
    private void Update()
    {
        if (Player.Instance == null) return;

        UpdatePriceColors(mejorasA, preciosMejorasA, Player.Instance.coinJ1);
        UpdatePriceColors(mejorasB, preciosMejorasB, Player.Instance.coinJ2);
    }



    private void OnEnable()
    {
        Player.OnCoinsChangedJ1 += UpdateCoinsJ1;
        Player.OnCoinsChangedJ2 += UpdateCoinsJ2;

        Player.OnLogMessageJ1 += UpdateLogJ1;
        Player.OnLogMessageJ2 += UpdateLogJ2;

        Player.OnSelectedTroopChangedJ1 += HighlightCardJ1;
        Player.OnSelectedTroopChangedJ2 += HighlightCardJ2;

        Player.OnGeneratorLevelChangedJ1 += lvl => UpdateBoosterColor(genJ1, lvl);
        Player.OnGeneratorLevelChangedJ2 += lvl => UpdateBoosterColor(genJ2, lvl);

        Player.OnATKLevelChangedJ1 += lvl => UpdateBoosterColor(atkJ1, lvl);
        Player.OnATKLevelChangedJ2 += lvl => UpdateBoosterColor(atkJ2, lvl);

        Player.OnSPDLevelChangedJ1 += lvl => UpdateBoosterColor(spdJ1, lvl);
        Player.OnSPDLevelChangedJ2 += lvl => UpdateBoosterColor(spdJ2, lvl);

        Player.OnHPLevelChangedJ1 += lvl => UpdateBoosterColor(hpJ1, lvl);
        Player.OnHPLevelChangedJ2 += lvl => UpdateBoosterColor(hpJ2, lvl);
    }

    private void OnDisable()
    {
        Player.OnCoinsChangedJ1 -= UpdateCoinsJ1;
        Player.OnCoinsChangedJ2 -= UpdateCoinsJ2;

        Player.OnLogMessageJ1 -= UpdateLogJ1;
        Player.OnLogMessageJ2 -= UpdateLogJ2;

        Player.OnSelectedTroopChangedJ1 -= HighlightCardJ1;
        Player.OnSelectedTroopChangedJ2 -= HighlightCardJ2;

        Player.OnGeneratorLevelChangedJ1 -= lvl => UpdateBoosterColor(genJ1, lvl);
        Player.OnGeneratorLevelChangedJ2 -= lvl => UpdateBoosterColor(genJ2, lvl);

        Player.OnATKLevelChangedJ1 -= lvl => UpdateBoosterColor(atkJ1, lvl);
        Player.OnATKLevelChangedJ2 -= lvl => UpdateBoosterColor(atkJ2, lvl);

        Player.OnSPDLevelChangedJ1 -= lvl => UpdateBoosterColor(spdJ1, lvl);
        Player.OnSPDLevelChangedJ2 -= lvl => UpdateBoosterColor(spdJ2, lvl);

        Player.OnHPLevelChangedJ1 -= lvl => UpdateBoosterColor(hpJ1, lvl);
        Player.OnHPLevelChangedJ2 -= lvl => UpdateBoosterColor(hpJ2, lvl);
    }

   

    private void UpdateCoinsJ1(int value)
    {
        monedasJ1.text = value + " coins";
    }

    private void UpdateCoinsJ2(int value)
    {
        monedasJ2.text = value + " coins";
    }


    private void UpdateLogJ1(string msg) => logsJ1.text = msg;
    private void UpdateLogJ2(string msg) => logsJ2.text = msg;

   

    private void HighlightCardJ1(int index)
    {
        GameObject[] cartas = { carta1, carta2, carta3, carta4 };

        for (int i = 0; i < cartas.Length; i++)
        {
            Vector3 p = cartasJ1OriginalPositions[i];  
            if (i == index)
                p.y -= 0.2f; 
            cartas[i].transform.position = p;
        }
    }


    private void HighlightCardJ2(int index)
    {
        GameObject[] cartas = { carta5, carta6, carta7, carta8 };

        for (int i = 0; i < cartas.Length; i++)
        {
            Vector3 p = cartasJ2OriginalPositions[i];
            if (i == index)
                p.y -= 0.2f;
            cartas[i].transform.position = p;

        }
    }

   

    private void UpdateBoosterColor(GameObject booster, int level)
    {
        Color c = Color.gray;
        if (level == 1) c = Color.green;
        if (level == 2) c = Color.yellow;
        if (level >= 3) c = Color.red;

        booster.GetComponent<Renderer>().material.color = c;
    }


    private void UpdatePriceColors(GameObject[] objs, int[] precios, int coins)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            var sr = objs[i].GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
                sr.color = (coins >= precios[i]) ? Color.yellow : Color.gray;
        }
    }

}
