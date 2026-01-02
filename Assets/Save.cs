using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Save : MonoBehaviour
{
    private const string filePath = "savefile.";

    [Header("References")]
    [SerializeField] private Player gameManagerMalo;

    [SerializeField] private List<ExplosiveEnemy> explosiveToSave;
    [SerializeField] private List<MagoOscuro> magosToSave;
    [SerializeField] private List<BouncingEnemy> bouncingToSave;
    [SerializeField] private List<BasicEnemy> basicToSave;

    [SerializeField] private PjTorrePrincipal pjTorreJ1;
    [SerializeField] private PjTorrePrincipal pjTorreJ2;

    // ===================== SAVE =====================
    public void SaveGame()
    {
#if UNITY_EDITOR
        string filepath = filePath;
#else
        string filepath = Path.Combine(Application.persistentDataPath, filePath);
#endif
        try
        {
            Debug.Log("Saving game...");

            // --- Encryption setup ---
            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            rng.GetBytes(iv);

            byte[] key = DeriveKey("666", salt);

            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            fs.Write(salt, 0, salt.Length);
            fs.Write(iv, 0, iv.Length);

            // --- Data ---
            GameData data = new GameData
            {
                explosiveEnemiesData = new List<ExplosiveEnemyData>(),
                magoOscuroData = new List<MagoOscuroData>(),
                bouncingEnemiesData = new List<BouncingEnemyData>(),
                basicEnemiesData = new List<BasicEnemyData>()
            };

            // --- Enemies ---
            foreach (ExplosiveEnemy e in explosiveToSave)
                data.explosiveEnemiesData.Add(new ExplosiveEnemyData
                {
                    name = e.name,
                    hp = e.hp,
                    atk = e.atk,
                    spd = e.spd,
                    position = e.transform.position
                });

            foreach (MagoOscuro m in magosToSave)
                data.magoOscuroData.Add(new MagoOscuroData
                {
                    name = m.name,
                    hp = m.hp,
                    atk = m.atk,
                    spd = m.spd,
                    position = m.transform.position
                });

            foreach (BouncingEnemy b in bouncingToSave)
                data.bouncingEnemiesData.Add(new BouncingEnemyData
                {
                    name = b.name,
                    position = b.transform.position
                });

            foreach (BasicEnemy b in basicToSave)
                data.basicEnemiesData.Add(new BasicEnemyData
                {
                    name = b.name,
                    hp = b.hp,
                    atk = b.atk,
                    spd = b.spd,
                    position = b.transform.position
                });

            // --- Towers ---
            data.torre1Data = new TorreData
            {
                name = pjTorreJ1.name,
                hp = pjTorreJ1.hp,
                atk = pjTorreJ1.atk,
                spd = pjTorreJ1.spd
            };

            data.torre2Data = new TorreData
            {
                name = pjTorreJ2.name,
                hp = pjTorreJ2.hp,
                atk = pjTorreJ2.atk,
                spd = pjTorreJ2.spd
            };

            // --- Player stats ---
            data.generatorJ1LVL = gameManagerMalo.pubGeneratorLevel;
            data.generatorJ2LVL = gameManagerMalo.pubGeneratorLevel2;
            data.HPBoosterJ1LVL = gameManagerMalo.pubHPBoosterLevel;
            data.HPBoosterJ2LVL = gameManagerMalo.pubHPBoosterLevel2;
            data.SPDBoosterJ1LVL = gameManagerMalo.pubSPBooster;
            data.SPDBoosterJ2LVL = gameManagerMalo.pubSPBooster2;
            data.ATKBoosterJ1LVL = gameManagerMalo.pubATKBoosterLevel;
            data.ATKBoosterJ2LVL = gameManagerMalo.pubATKBoosterLevel2;

            data.moneyJ1 = gameManagerMalo.coinJ1;
            data.moneyJ2 = gameManagerMalo.coinJ2;

            // --- Write ---
            ICryptoTransform encryptor = aes.CreateEncryptor();
            CryptoStream cs = new CryptoStream(fs, encryptor, CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs, Encoding.UTF8);

            sw.Write(JsonUtility.ToJson(data));
            sw.Close();
            cs.Close();
            fs.Close();
            aes.Clear();

            Debug.Log("Game saved successfully");
        }
        catch (Exception ex)
        {
            Debug.LogError("SAVE ERROR: " + ex);
        }
    }

    // ===================== LOAD =====================
    public void LoadGame()
    {
#if UNITY_EDITOR
        string filepath = filePath;
#else
        string filepath = Path.Combine(Application.persistentDataPath, filePath);
#endif
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("No save file found");
            return;
        }

        try
        {
            Debug.Log("Loading game...");

            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            fs.Read(salt, 0, 16);
            fs.Read(iv, 0, 16);

            byte[] key = DeriveKey("666", salt);

            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            CryptoStream cs = new CryptoStream(fs, aes.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding.UTF8);

            GameData data = JsonUtility.FromJson<GameData>(sr.ReadToEnd());

            sr.Close();
            cs.Close();
            fs.Close();

            // --- Player ---
            gameManagerMalo.SetGeneratorLvlJ1(data.generatorJ1LVL);
            gameManagerMalo.SetGeneratorLvlJ2(data.generatorJ2LVL);
            gameManagerMalo.SetHPBoosterLvlJ1(data.HPBoosterJ1LVL);
            gameManagerMalo.SetHPBoosterLvlJ2(data.HPBoosterJ2LVL);
            gameManagerMalo.SetSPDBoosterLvlJ1(data.SPDBoosterJ1LVL);
            gameManagerMalo.SetSPDBoosterLvlJ2(data.SPDBoosterJ2LVL);
            gameManagerMalo.SetATKBoosterLvlJ1(data.ATKBoosterJ1LVL);
            gameManagerMalo.SetATKBoosterLvlJ2(data.ATKBoosterJ2LVL);
            gameManagerMalo.SetCoinsJ1(data.moneyJ1);
            gameManagerMalo.SetCoinsJ2(data.moneyJ2);

          //  Player.OnCoinsChangedJ1?.Invoke(data.moneyJ1);
          //  Player.OnCoinsChangedJ2?.Invoke(data.moneyJ2);

            // --- Towers ---
            pjTorreJ1.SetHp(data.torre1Data.hp);
            pjTorreJ1.SetAtk(data.torre1Data.atk);
            pjTorreJ1.SetSpd(data.torre1Data.spd);

            pjTorreJ2.SetHp(data.torre2Data.hp);
            pjTorreJ2.SetAtk(data.torre2Data.atk);
            pjTorreJ2.SetSpd(data.torre2Data.spd);

            Debug.Log("Game loaded successfully");
        }
        catch (Exception ex)
        {
            Debug.LogError("LOAD ERROR: " + ex);
        }
    }

    // ===================== DATA =====================
    [Serializable]
    struct GameData
    {
        public List<ExplosiveEnemyData> explosiveEnemiesData;
        public List<MagoOscuroData> magoOscuroData;
        public List<BouncingEnemyData> bouncingEnemiesData;
        public List<BasicEnemyData> basicEnemiesData;

        public TorreData torre1Data;
        public TorreData torre2Data;

        public int generatorJ1LVL, generatorJ2LVL;
        public int HPBoosterJ1LVL, HPBoosterJ2LVL;
        public int SPDBoosterJ1LVL, SPDBoosterJ2LVL;
        public int ATKBoosterJ1LVL, ATKBoosterJ2LVL;
        public int moneyJ1, moneyJ2;
    }

    [Serializable]
    struct ExplosiveEnemyData
    { public string name; public int hp, atk, spd; public Vector3 position; }

    [Serializable]
    struct MagoOscuroData
    { public string name; public int hp, atk, spd; public Vector3 position; }

    [Serializable]
    struct BouncingEnemyData
    { public string name; public Vector3 position; }

    [Serializable]
    struct BasicEnemyData
    { public string name; public int hp, atk, spd; public Vector3 position; }

    [Serializable]
    struct TorreData
    { public string name; public int hp, atk, spd; }

    private static byte[] DeriveKey(string password, byte[] salt, int iterations = 10000)
    {
        return new Rfc2898DeriveBytes(password, salt, iterations).GetBytes(32);
    }
}
