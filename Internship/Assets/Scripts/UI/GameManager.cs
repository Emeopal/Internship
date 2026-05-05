using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    public int currentLevel;
    public int currentMaxLevel;
    public int maxLevel;

    public void Awake()
    {
        maxLevel = levels.Count;
    }
    public void SaveData()
    {
        string DirPath = Path.Combine(Application.persistentDataPath, "Game_SaveData");
        string FilePath = Path.Combine(Application.persistentDataPath, "Game_SaveData", "Data.txt");

        if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream file;
        if (!File.Exists(FilePath))
            file = File.Create(FilePath);
        else
            file = File.OpenWrite(FilePath);

            string json = JsonUtility.ToJson(currentMaxLevel, true);

        binaryFormatter.Serialize(file, json);
        
        file.Close();
    }

    public void LoadData()
    {
        string DirPath = Path.Combine(Application.persistentDataPath, "Game_SaveData");
        string FilePath = Path.Combine(Application.persistentDataPath, "Game_SaveData", "Data.txt");

        if (Directory.Exists(DirPath))
        {

            //虽然不安全
            //创建二进制工具
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            //打开文件
            FileStream file = File.Open(Application.persistentDataPath + "/Game_SaveData/Data.txt", FileMode.Open);

            int temp = 1;

            JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), temp);

            currentMaxLevel = currentMaxLevel > temp ? currentMaxLevel : temp;
            //关闭文件
            file.Close();
        }
    }
}