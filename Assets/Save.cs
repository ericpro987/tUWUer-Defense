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
         explosiveToSave = new List<ExplosiveEnemy>();
         magosToSave = new List<MagoOscuro>();
         bouncingToSave = new List<BouncingEnemy>();
         basicToSave = new List<BasicEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddExplosive(ExplosiveEnemy explosive)
    {
        explosiveToSave.Add(explosive);
    }
    public void AddMago(MagoOscuro mago)
    {
        magosToSave.Add(mago);
    }
    public void AddBouncing(BouncingEnemy bouncing)
    {
        bouncingToSave.Add(bouncing);
    }
    public void AddBasic(BasicEnemy basic)
    {
        basicToSave.Add(basic);
    }

    public void RemoveExplosive(ExplosiveEnemy explosive)
    {
        explosiveToSave.Remove(explosive);
    }
    public void RemoveMago(MagoOscuro mago)
    {
        magosToSave.Remove(mago);
    }
    public void RemoveBouncing(BouncingEnemy bouncing)
    {
        bouncingToSave.Remove(bouncing);
    }
    public void RemoveBasic(BasicEnemy basic)
    {
        basicToSave.Remove(basic);
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
                position = explosive.transform.position,
                tagEnemy = explosive.tagEnemy,
                tag = explosive.tag
            };
            if (data.explosiveEnemiesData == null)
                data.explosiveEnemiesData = new List<ExplosiveEnemyData>();
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
                position = mago.transform.position,
                tagEnemy = mago.tagEnemy,
                tag = mago.tag
            };
            if (data.magoOscuroData == null)
                data.magoOscuroData = new List<MagoOscuroData>();
            data.magoOscuroData.Add(magoData);
        }
        foreach (BouncingEnemy bo in bouncingToSave)
        {
            BouncingEnemyData bouncingData= new BouncingEnemyData
            {
                name = bo.name,
                hp = bo.hp,
                atk = bo.atk,
                spd = bo.spd,
                position = bo.transform.position,
                tagEnemy = bo.tagEnemy,
                tag = bo.tag
            };
            if (data.bouncingEnemiesData == null)
                data.bouncingEnemiesData = new List<BouncingEnemyData>();
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
                position = be.transform.position,
                tagEnemy = be.tagEnemy,
                tag = be.tag
            };
            if (data.basicEnemiesData == null)
                data.basicEnemiesData = new List<BasicEnemyData>();
            data.basicEnemiesData.Add(basicData);
        }
        TorreData torre1Data = new TorreData
        {
            name = pjTorreJ1.name,
            hp = pjTorreJ1.hp,
            atk = pjTorreJ1.atk,
            spd = pjTorreJ1.spd,
            tagEnemy = pjTorreJ1.tagEnemy,
            tag = pjTorreJ1.tag
        };
        data.torre1Data = torre1Data;
        TorreData torre2Data = new TorreData
        {
            name = pjTorreJ2.name,
            hp = pjTorreJ2.hp,
            atk = pjTorreJ2.atk,
            spd = pjTorreJ2.spd,
            tagEnemy = pjTorreJ2.tagEnemy,
            tag = pjTorreJ2.tag
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
                        ex.transform.tag = explosiveData.tag;
                        ex.tagEnemy = explosiveData.tagEnemy;
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
                        mo.transform.tag = magoData.tag;
                        mo.tagEnemy = magoData.tagEnemy;
                    }
                }
            }
            foreach (var bouncingData in data.bouncingEnemiesData)
            {
                foreach (BouncingEnemy bo in bouncingToSave)
                {
                    if (bo.name == bouncingData.name)
                    {
                        bo.SetHp(bouncingData.hp);
                        bo.SetAtk(bouncingData.atk);
                        bo.SetSpd(bouncingData.spd);
                        bo.transform.position = bouncingData.position;
                        bo.transform.tag = bouncingData.tag;
                        bo.tagEnemy = bouncingData.tagEnemy;
                    }
                }
            }
            foreach (var basicData in data.basicEnemiesData)
            {
                foreach (BasicEnemy be in basicToSave)
                {
                    if (be.name == basicData.name)
                    {
                        be.SetHp(basicData.hp);
                        be.SetAtk(basicData.atk);
                        be.SetSpd(basicData.spd);
                        be.transform.position = basicData.position;
                        be.transform.tag = basicData.tag;
                        be.tagEnemy = basicData.tagEnemy;
                    }
                }
            }
            pjTorreJ1.SetHp(data.torre1Data.hp);
            pjTorreJ1.SetAtk(data.torre1Data.atk);
            pjTorreJ1.SetSpd(data.torre1Data.spd);
            pjTorreJ1.transform.tag = data.torre1Data.tag;
            pjTorreJ1.tagEnemy = data.torre1Data.tagEnemy;
            pjTorreJ2.SetHp(data.torre2Data.hp);
            pjTorreJ2.SetAtk(data.torre2Data.atk);
            pjTorreJ2.SetSpd(data.torre2Data.spd);
            pjTorreJ2.transform.tag = data.torre2Data.tag;
            pjTorreJ2.tagEnemy = data.torre2Data.tagEnemy;

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
        public string tagEnemy;
        public string tag;
    }
    [Serializable]
    struct MagoOscuroData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
        public string tagEnemy;
        public string tag;
    }
    [Serializable]
    struct BouncingEnemyData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
        public string tagEnemy;
        public string tag;
    }
    [Serializable]
    struct BasicEnemyData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
        public string tagEnemy;
        public string tag;
    }
    struct TorreData
    {
        public string name;
        public int hp;
        public int atk;
        public int spd;
        public string tagEnemy;
        public string tag;
    }
    private static byte[] DeriveKey(string password, byte[] salt, int iterations = 10000)
    {
        Rfc2898DeriveBytes kdf = new Rfc2898DeriveBytes(password, salt, iterations);
        return kdf.GetBytes(32); // AES-256
    }
}
