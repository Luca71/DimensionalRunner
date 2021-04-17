using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TMP_Text TimeText;

    float seconds;
    
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        seconds = (int)(Time.time % 60);
        TimeText.text = "Time: " + seconds.ToString("00000") + " sec.";
    }
}
