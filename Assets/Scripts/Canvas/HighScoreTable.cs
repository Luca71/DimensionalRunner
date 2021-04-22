using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] Transform scoreNamesParent;
    [SerializeField] Transform scoreValuesParent;

    TMP_Text[] scoreNames;
    TMP_Text[] scoreValues;

    ScoreManager scoreMgr;

    // Start is called before the first frame update
    void Start()
    {
        scoreMgr ??= ScoreManager.Instance; // in null do it
        scoreNames = scoreNamesParent.GetComponentsInChildren<TMP_Text>();
        scoreValues = scoreValuesParent.GetComponentsInChildren<TMP_Text>();

        int index = 0;
        foreach (var item in scoreMgr.highScores)
        {
            scoreNames[index].text = item.Key;
            scoreValues[index].text = item.Value.ToString("000000");
            index++;
        }
        index = 0;
    }

}
