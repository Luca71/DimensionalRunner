using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOptionScript_Delete : MonoBehaviour
{
    float timer;
    float timerSeconds = 0.5f;
    bool isActive = false;
    GameObject OptionMenu;

    // Start is called before the first frame update
    void Start()
    {
        OptionMenu = GameObject.Find("Options_Menu");
        OptionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && timer > timerSeconds)
        {
            if (isActive == false)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }

            OptionMenu.SetActive(isActive);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
