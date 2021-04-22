using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [HideInInspector]
    public int collectedCoin = 0;
    [HideInInspector]
    public int timeFromStart = 0;
    public Dictionary<string, int> highScores;

    // in game score text
    TMP_Text scoreText;

    private void Awake()
    {
        MakeInstance();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        ResetCoinToZero();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMP_Text>();
        if (scoreText == null)
            return;
        else
            scoreText.text = "Score: " + collectedCoin.ToString("00000");
    }

    // aggiungere funzionalità

    public void AddCoins(int value)
    {
        collectedCoin += value;
        scoreText.text = "Score: " + collectedCoin.ToString("00000");
    }

    public int GetTotalScore()
    {
        int lostPointsForTime = (int)(timeFromStart * 0.10);
        int totalScore = (collectedCoin - lostPointsForTime) * 120;
        return totalScore;
    }

    public void ResetCoinToZero()
    {
        collectedCoin = 0;
    }

    public void SaveBestScore()
    {
        SaveSystem.SaveBestScore(this);
    }

    public void LoadBestScore()
    {
        GameData data = SaveSystem.LoadBestScore();
        highScores = data.ScoreTable;
    }

    void ScoreTableUpdate()
    {
        highScores.Add("aa", 5); // test da cancellare
        highScores = highScores.OrderByDescending(x => x.Value) as Dictionary<string, int>;
        highScores = highScores.Take(5) as Dictionary<string, int>;
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
}
