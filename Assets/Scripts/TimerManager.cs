using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    GameObject TimerObj;
    float timer;
    string nZero;

    // Start is called before the first frame update
    void Start()
    {
        TimerObj = GameObject.Find("Timer");
        TimerObj.GetComponent<TextMeshProUGUI>().outlineColor = Color.black;
        TimerObj.GetComponent<TextMeshProUGUI>().outlineWidth = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 10)
        {
            nZero = "00";
        }
        else if (timer < 100)
        {
            nZero = "0";
        }
        else if (timer >= 100)
        {
            nZero = "";
        }

        TimerObj.GetComponent<TextMeshProUGUI>().text = "TIMER: " + nZero + (int)timer;
    }
}
