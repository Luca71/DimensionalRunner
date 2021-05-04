using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TMP_Text TimeText;


    float seconds;
    WaitForSeconds wait;
    // Start is called before the first frame update
    void Start()
    {
        wait = new WaitForSeconds(1);
        seconds = ScoreManager.Instance.timeFromStart;
        StartCoroutine(UpdateSecond());
    }

    IEnumerator UpdateSecond()
    {
        while (this.gameObject.activeSelf)
        {
            //seconds = Time.time;
            TimeText.text = "Time: " + (Time.time - seconds).ToString("00000") + " sec.";
            yield return wait;
        }
    }
}
