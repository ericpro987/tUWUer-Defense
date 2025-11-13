using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Save : MonoBehaviour
{
    private const string filePath = "savefile.";
    [SerializeField]
    Player gameManagerMalo;
    GameManager gameManager;
    [SerializeField]
    List<ExplosiveEnemy> explosiveToSave;
    [SerializeField]
    List<MagoOscuro> magosToSave;
    [SerializeField]
    List<BouncingEnemy> bouncingToSave;
    [SerializeField]
    List<BasicEnemy> basicToSave;
    [SerializeField]
    PjTorrePrincipal pjTorreJ1;
    [SerializeField]
    PjTorrePrincipal pjTorreJ2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //  explosiveToSave = new List<ExplosiveEnemy>();
        // magosToSave = new List<MagoOscuro>();
        // objectsToSave = gameManager.GetAllGameObjects(); // Hypothetical method to get all relevant game objects
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SaveGame()
    {
#if UNITY_EDITOR
        string filepath = filePath;
#else
        string filepath = Path.Combine(Application.persistentDataPath, filePath);
#endif

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
        GameData data = new GameData();
        data.explosiveEnemiesData = new List<ExplosiveEnemyData>();
        data.magoOscuroData = new List<MagoOscuroData>();
        pjTorreJ1.SetHp(data.torre1Data.hp);
        pjTorreJ2.SetHp(data.torre2Data.hp);
        pjTorreJ1.SetAtk(data.torre1Data.atk);
        pjTorreJ2.SetAtk(data.torre2Data.atk);
        pjTorreJ1.SetSpd(data.torre1Data.spd);
        pjTorreJ2.SetSpd(data.torre2Data.spd);
        foreach (ExplosiveEnemy explosive in explosiveToSave)
        {
            ExplosiveEnemyData explosiveData = new ExplosiveEnemyData
            {
                name = explosive.name,
                hp = explosive.hp,
                atk = explosive.atk,
                spd = explosive.spd,
                position = explosive.transform.position
            };
            data.explosiveEnemiesData.Add(explosiveData);
        }
        foreach (MagoOscuro mago in magosToSave)
        {
            MagoOscuroData magoData = new MagoOscuroData
            {
                name = mago.name,
                hp = mago.hp,
                atk = mago.atk,
                spd = mago.spd,
                position = mago.transform.position
            };
            data.magoOscuroData.Add(magoData);
        }
        foreach (BouncingEnemy bo in bouncingToSave)
        {
            BouncingEnemyData bouncingData= new BouncingEnemyData
            {
                name = bo.name,
                //hp = bo.hp,
                //atk = bo.atk,
                //spd = bo.spd,
                position = bo.transform.position
            };
            data.bouncingEnemiesData.Add(bouncingData);
        }
        foreach (BasicEnemy be in basicToSave)
        {
            BasicEnemyData basicData = new BasicEnemyData
            {
                name = be.name,
                hp = be.hp,
                atk = be.atk,
                spd = be.spd,
                position = be.transform.position
            };
            data.basicEnemiesData.Add(basicData);
        }
        TorreData torre1Data = new TorreData
        {
            name = pjTorreJ1.name,
            hp = pjTorreJ1.hp,
            atk = pjTorreJ1.atk,
            spd = pjTorreJ1.spd,
        };
        data.torre1Data = torre1Data;
        TorreData torre2Data = new TorreData
        {
            name = pjTorreJ2.name,
            hp = pjTorreJ2.hp,
            atk = pjTorreJ2.atk,
            spd = pjTorreJ2.spd,
        };
        data.torre2Data = torre2Data;

        data.generatorJ1LVL = gameManagerMalo.pubGeneratorLevel;
        data.generatorJ2LVL = gameManagerMalo.pubGeneratorLevel2;
        data.HPBoosterJ1LVL= gameManagerMalo.pubHPBoosterLevel;
        data.HPBoosterJ2LVL= gameManagerMalo.pubHPBoosterLevel2;
        data.SPDBoosterJ1LVL= gameManagerMalo.pubSPBooster;
        data.SPDBoosterJ2LVL= gameManagerMalo.pubSPBooster2;
        data.ATKBoosterJ1LVL= gameManagerMalo.pubATKBoosterLevel;
        data.ATKBoosterJ2LVL= gameManagerMalo.pubATKBoosterLevel2;

        data.moneyJ1 = gameManagerMalo.coinJ1;
        data.moneyJ2 = gameManagerMalo.coinJ2;
        try
        {
            ICryptoTransform encryptor = aes.CreateEncryptor();
            CryptoStream cs = new CryptoStream(fs, encryptor, CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs, Encoding.UTF8);
            string jsonData = JsonUtility.ToJson(data);
            sw.Write(jsonData);
            sw.Close();
            cs.Close();
            fs.Close();
            aes.Clear();
            //            File.WriteAllText(filePath, jsonData);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        
    }
    public void LoadGame()
    {
#if UNITY_EDITOR
        string filepath = filePath;
#else
        string filepath = Path.Combine(Application.persistentDataPath, filePath);
#endif
        try
        {
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            // Leer salt (16 bytes) e IV (16 bytes)
            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            fs.Read(salt, 0, 16);
            fs.Read(iv, 0, 16);


            // Derivar la clave
            byte[] key = DeriveKey("666", salt);
            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Descifrar
            ICryptoTransform decryptor = aes.CreateDecryptor();
            CryptoStream cs = new CryptoStream(fs, decryptor, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding.UTF8);
            string jsonData= sr.ReadToEnd();
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            sr.Close();
            cs.Close();
            fs.Close();
            /*     string jsonData = File.ReadAllText(filepath);
                 GameData data = JsonUtility.FromJson<GameData>(jsonData); ;*/
            gameManagerMalo.SetGeneratorLvlJ1(data.generatorJ1LVL);
            gameManagerMalo.SetGeneratorLvlJ2(data.generatorJ2LVL);
            gameManagerMalo.SetHPBoosterLvlJ1(data.HPBoosterJ1LVL);
            gameManagerMalo.SetHPBoosterLvlJ2(data.HPBoosterJ2LVL);
            gameManagerMalo.SetATKBoosterLvlJ1(data.ATKBoosterJ1LVL);
            gameManagerMalo.SetATKBoosterLvlJ2(data.ATKBoosterJ2LVL);
            gameManagerMalo.SetSPDBoosterLvlJ1(data.SPDBoosterJ1LVL);
            gameManagerMalo.SetSPDBoosterLvlJ2(data.SPDBoosterJ2LVL);

            gameManagerMalo.SetCoinsJ1(data.moneyJ1);
            gameManagerMalo.SetCoinsJ2(data.moneyJ2);
            foreach (var explosiveData in data.explosiveEnemiesData)
            {
                foreach (ExplosiveEnemy ex in explosiveToSave)
                {
                    if (ex.name == explosiveData.name)
                    {
                        ex.SetHp(explosiveData.hp);
                        ex.SetAtk(explosiveData.atk);
                        ex.SetSpd(explosiveData.spd);
                        ex.transform.position = explosiveData.position;
                    }
                }
            }
            foreach (var magoData in data.magoOscuroData)
            {
                foreach (MagoOscuro mo in magosToSave)
                {
                    if (mo.name == magoData.name)
                    {
                        mo.SetHp(magoData.hp);
                        mo.SetAtk(magoData.atk);
                        mo.SetSpd(magoData.spd);
                        mo.transform.position = magoData.position;
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }           
    }
    [Serializable]
    struct GameData
    {
        public List<ExplosiveEnemyData> explosiveEnemiesData;
        public List<MagoOscuroData> magoOscuroData;
        public List<BouncingEnemyData> bouncingEnemiesData;
        public List<BasicEnemyData> basicEnemiesData;
        public TorreData torre1Data;
        public TorreData torre2Data;
        public int generatorJ1LVL;
        public int generatorJ2LVL;
        public int HPBoosterJ1LVL;
        public int HPBoosterJ2LVL;
        public int SPDBoosterJ1LVL;
        public int SPDBoosterJ2LVL;
        public int ATKBoosterJ1LVL;
        public int ATKBoosterJ2LVL;
        public int moneyJ1;
        public int moneyJ2;
    }
    [Serializable]
    struct ExplosiveEnemyData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
    }
    [Serializable]
    struct MagoOscuroData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
    }
    [Serializable]
    struct BouncingEnemyData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
    }
    [Serializable]
    struct BasicEnemyData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
    }
    struct TorreData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
    }
    private static byte[] DeriveKey(string password, byte[] salt, int iterations = 10000)
    {
        Rfc2898DeriveBytes kdf = new Rfc2898DeriveBytes(password, salt, iterations);
        return kdf.GetBytes(32); // AES-256
    }
}
