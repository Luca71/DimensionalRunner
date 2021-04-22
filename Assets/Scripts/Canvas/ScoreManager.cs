using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int collectedCoin = 0;
    public int timeFromStart = 0;
    public Dictionary<string, int> highScores;

    private void Awake()
    {
        MakeInstance();

        
    }

    // aggiungere funzionalità

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
