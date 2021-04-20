using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager2 : MonoBehaviour
{
    public static int score;

    GameObject ScoreObj;

    // Start is called before the first frame update
    void Start()
    {
        ScoreObj = GameObject.Find("Score");
        ScoreObj.GetComponent<TextMeshProUGUI>().outlineColor = Color.black;
        ScoreObj.GetComponent<TextMeshProUGUI>().outlineWidth = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreObj.GetComponent<TextMeshProUGUI>().text = "SCORE: " + score;
    }
}
