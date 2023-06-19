using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public TextMeshProUGUI bestScoreText;
    private int bestScore;
    private string bestPlayerName;
    public InputField playerNameInput;
    public Button saveNameButton;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        saveNameButton.onClick.AddListener(SavePlayerName);

        bestScore = LoadScore();
        bestPlayerName = LoadName();

        UpdateBestScoreText();
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = "Best Score: " + bestPlayerName + " : " + bestScore.ToString();
    }

    [Serializable]
    class SaveData
    {
        public int score;
        public string playerName;
    }

    public void SaveName(string playerName)
    {
        SaveData data = new SaveData();
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
    }

    public string LoadName()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data.playerName;
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de datos guardados.");
            return string.Empty;
        }
    }

    public void SaveScore(int score)
    {
        SaveData data = new SaveData();
        data.score = score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
    }

    public int LoadScore()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data.score;
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de datos guardados.");
            return 0;
        }
    }

    public void SavePlayerName()
    {
        string playerName = playerNameInput.text;
        SaveName(playerName);
    }
}