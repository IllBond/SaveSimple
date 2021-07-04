using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{


    public static string[] saves = new string[3] { "/save1.save", "/save2.save", "/save3.save" };

    public string filePath;
   
    public List<GameObject> EnemySaves = new List<GameObject>();
    public GameObject PlayerSaves;

    [SerializeField] private GameController _gameController;

    public void Start()
    {
        //string path = Application.persistentDataPath + saves[saveIndex];
        //filePath = Application.persistentDataPath + "/save1.save";
    }

    public void SaveGame(int saveIndex) {
        BinaryFormatter formatter = new BinaryFormatter();
        filePath = Application.persistentDataPath + saves[saveIndex];

        FileStream stream = new FileStream(filePath, FileMode.Create);

        Save save = new Save();


        save.SavePlayer(PlayerSaves);
        save.SaveEnemies(EnemySaves);
        save.score = _gameController.score;

        formatter.Serialize(stream, save);
        stream.Close();
    }
    
    public void LoadGame(int saveIndex) {
        filePath = Application.persistentDataPath + saves[saveIndex];

        if (!File.Exists(filePath)) {
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open);

        Save save = (Save)formatter.Deserialize(stream);
        stream.Close();

        _gameController.score = save.score;

        int i = 0;
        foreach (var enemy in save.EnemiesData)
        {
            EnemySaves[i].GetComponent<Enemy>().LoadData(enemy);
            i++;
        }

        PlayerSaves.GetComponent<Player>().LoadData(save.PlayerData);
    }




    [System.Serializable]
    public class Save 
    {
        [System.Serializable]
        public struct Vec3 
        {
            public float x, y, z;

            public Vec3(float x, float y, float z) {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        [System.Serializable]
        public struct EnemySaveData 
        {
            
            public Vec3 Position;

            public EnemySaveData(Vec3 pos) {
                Position = pos;
            }
        }

        [System.Serializable]
        public struct PlayerSaveData 
        {
            public int level;
            public int health;

            public Vec3 Position;

            public PlayerSaveData(Vec3 pos, int _level, int _health) {
            level = _level;
            health = _health;
            Position = pos;
            }
        }

        
        


        public int score;
        public List<EnemySaveData> EnemiesData =
            new List<EnemySaveData>();
        public PlayerSaveData PlayerData;


        public void SaveEnemies(List<GameObject> enemies)
        {
            foreach (var item in enemies)
            {
                //var em = item.GetComponent<Enemy>();
                Vec3 pos = new Vec3(item.transform.position.x, item.transform.position.y, item.transform.position.z);
                EnemiesData.Add(new EnemySaveData(pos));
            }
        }

        public void SavePlayer(GameObject player) 
        {
                int level = player.GetComponent<Player>().level;
                int health = player.GetComponent<Player>().health;
    
                Vec3 pos = new Vec3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                PlayerData = new PlayerSaveData(pos, level, health);
        }

    }

} 
