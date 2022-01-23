using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class HighScore
    {
        public int score;
        public string playerName;

        public HighScore(int score, string playerName)
        {
            this.score = score;
            this.playerName = playerName;
        }

        public HighScore() : this(0, "Name") { }
    }

    [Serializable]
    public class HighScores
    {
        public HighScore[] highScores = new HighScore[0];
    }

    public static GameManager instance { get; private set; }

    public string playerName;

    public HighScores highScores = new HighScores();
    public HighScore bestScore = new HighScore();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
        LoadHighScores();
    }

    public void ShowMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void ShowHighScores()
    {
        SceneManager.LoadScene("High Scores");
    }

    public void Quit()
    {
        SaveHighScores();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveScore(int score)
    {
        Array.Resize(ref highScores.highScores, highScores.highScores.Length + 1);
        highScores.highScores[highScores.highScores.Length - 1] = new HighScore(score, playerName);
        Array.Sort(highScores.highScores, (a, b) => b.score - a.score);
    }

    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(highScores);
        string path = Application.persistentDataPath + "/highscores.json";
        File.WriteAllText(path, json);
    }

    private void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (!File.Exists(path))
        {
            return;
        }

        string json = File.ReadAllText(path);
        highScores = JsonUtility.FromJson<HighScores>(json);
        if (highScores.highScores.Length > 0)
        {
            Array.Sort(highScores.highScores, (a, b) => b.score - a.score);
            bestScore = highScores.highScores[0];
        }
    }
}
