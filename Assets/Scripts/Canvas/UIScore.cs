using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    public TMP_Text scoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreUpdate += ScoreUpdate;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreUpdate -= ScoreUpdate;
    }

    private void Start()
    {
        if (scoreText == null)
            return;
        else
            scoreText.text = "Score: " + "00000";
    }

    private void ScoreUpdate(int value)
    {
        scoreText.text = "Score: " + value.ToString("00000");
    }
}
