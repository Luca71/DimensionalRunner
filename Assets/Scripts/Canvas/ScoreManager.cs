using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get
        {
            if(instance == null)
            {
                var gm = new GameObject("ScoreManager");
                instance = gm.AddComponent<ScoreManager>(); 

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private static ScoreManager instance;

    [HideInInspector]
    public int collectedCoin = 0;
    [HideInInspector]
    public float timeFromStart = 0;
    public Dictionary<string, int> highScores;
    public float musicLevel = 0;
    public float sfxLevel = 0;

    private float penalityMultiplier = 1f;

 
    public delegate void OnScoreUpdateDelegate(int coins);
    public static OnScoreUpdateDelegate OnScoreUpdate;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Debug.LogError("Score manager ha una doppia istanza nella scena");
            Destroy(this);
        }
        
        highScores = new Dictionary<string, int>();

        SetFakeScores();
        ScoreTableUpdate();

    }

    // aggiungere funzionalità

    public void AddCoins(int value)
    {
        collectedCoin += value;
        OnScoreUpdate(collectedCoin);
    }

    public int GetTotalScore()
    {
        int lostPointsForTime = (int)(timeFromStart * penalityMultiplier);
        int totalScore = (collectedCoin * 10 - lostPointsForTime * 10) * 5;
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
        ScoreTableUpdate();
    }

    public void SetFakeScores()
    {
        for (int i = 0; i < 10; i++)
        {
            highScores.Add("None" + i, i * 10);
        }
    }

    public void SetSoundsVolume(float musicVol, float sfxVol)
    {
        musicLevel = musicVol;
        sfxLevel = sfxVol;
    }

    public bool CheckScoreGraduatory(int lastGameScore)
    {
        bool res = false;
        foreach (var value in highScores.Values)
        {
            if (lastGameScore > value)
            {
                res = true;
            }
            break;
        }
        return res;
    }

    public void SetScore(string name, int lastGameScore)
    {
        if (highScores.ContainsKey(name))
        {
            highScores[name] = lastGameScore;
        }
        else
        {
            highScores.Add(name, lastGameScore);
        }
    }

    void ScoreTableUpdate()
    {
        if (highScores.Count == 0) return;

        int num = highScores.Count;
        if(highScores.Count >= 5)
        {
            num = 5;
        }
        highScores = highScores.OrderByDescending(x => x.Value).ToDictionary(X => X.Key, X => X.Value);
        highScores = highScores.Take(num).ToDictionary(X => X.Key, X => X.Value);
    }

    public void ResetTimer()
    {
        timeFromStart = Time.time;
    }
}
