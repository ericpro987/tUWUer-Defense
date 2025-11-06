using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Authentication;
using UnityEngine;

public class Save : MonoBehaviour
{
    string filePath = "savefile.sav";
    GameManager gameManager;
    List<ExplosiveEnemy> explosiveToSave;
    List<MagoOscuro> magosToSave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosiveToSave = new List<ExplosiveEnemy>();
        magosToSave = new List<MagoOscuro>();
        // objectsToSave = gameManager.GetAllGameObjects(); // Hypothetical method to get all relevant game objects
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            GameData data = new GameData();
            data.explosiveEnemiesData = new List<ExplosiveEnemyData>();
            data.magoOscuroData = new List<MagoOscuroData>();
            foreach (var explosive in explosiveToSave)
            {
                ExplosiveEnemyData explosiveData = new ExplosiveEnemyData
                {
                    hp = explosive.hp,
                    atk = explosive.atk,
                    spd = explosive.spd,
                    position = explosive.transform.position
                };
                data.explosiveEnemiesData.Add(explosiveData);
            }
            foreach (var mago in magosToSave)
            {
                MagoOscuroData magoData = new MagoOscuroData
                {
                    hp = mago.hp,
                    atk = mago.atk,
                    spd = mago.spd,
                    position = mago.transform.position
                };
                data.magoOscuroData.Add(magoData);
            }
            formatter.Serialize(stream, data);
        }
    }
    void LoadGame()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            GameData data = (GameData)formatter.Deserialize(stream);
            foreach (var explosiveData in data.explosiveEnemiesData)
            {

                ExplosiveEnemy explosive = null;
                explosive.SetHp(explosiveData.hp);
                explosive.SetAtk(explosiveData.atk);
                explosive.SetSpd(explosiveData.spd);
                explosive.transform.position = explosiveData.position;

            }
            foreach (var magoData in data.magoOscuroData)
            {
                MagoOscuro mago = null;
                mago.SetHp(magoData.hp);
                mago.SetAtk(magoData.atk);
                mago.SetSpd(magoData.spd);
                mago.transform.position = magoData.position;
            }
        }
    } 
    struct GameData
    {
        public List<ExplosiveEnemyData> explosiveEnemiesData;
        public List<MagoOscuroData> magoOscuroData;
    }
    struct ExplosiveEnemyData
    {
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
    }
    struct MagoOscuroData
    {
        public int hp;
        public int atk;
        public int spd;
        public Vector3 position;
    }
}
