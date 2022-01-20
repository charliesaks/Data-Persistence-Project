using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public string playerName;
    public string hiScorer;
    public int hiScore;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadHiScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string hiScorer;
        public int hiScore;
    }

    public void SaveHiScore()
    {
        SaveData data = new SaveData();
        data.hiScorer = hiScorer;
        data.hiScore = hiScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHiScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            hiScorer = data.hiScorer;
            hiScore = data.hiScore;
        }
    }
}
