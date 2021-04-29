using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum OptionMenu { FullScreen, SizeScreen, Sound }

public class OptionMenuManager : MonoBehaviour
{
    float timer;
    OptionMenu selectOption;
    GameObject FullScreen;
    GameObject ScreenSize;
    GameObject Sound;
    float timerSeconds = 0.3f;

    float SetScreen = 3;
    Vector2 ScreenResolution;
    Vector2[] allScreenResolution;

    float GameSoundVolume = 1;

    // Start is called before the first frame update
    void Start()
    {
        FullScreen = GameObject.Find("FullScreen");
        ScreenSize = GameObject.Find("ScreenSize");
        Sound = GameObject.Find("Sounds");
        ScreenResolution = new Vector2(1920, 1080);
        allScreenResolution = new Vector2[4];
        Screen.SetResolution((int)ScreenResolution.x, (int)ScreenResolution.y, Screen.fullScreenMode);
        CreateScreenResolution();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("BlackSky").GetComponent<Image>().CrossFadeAlpha(0.5f, 0, true);
        OptionSelectManager();
        timer += Time.deltaTime;
    }

    #region Option Select Manager
    void OptionSelectManager()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && timer > timerSeconds)
        {
            switch (selectOption)
            {
                case OptionMenu.FullScreen:
                    selectOption = OptionMenu.SizeScreen;
                    break;

                case OptionMenu.SizeScreen:
                    selectOption = OptionMenu.Sound;
                    break;

                case OptionMenu.Sound:
                    selectOption = OptionMenu.FullScreen;
                    break;
            }

            timer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && timer > timerSeconds)
        {
            switch (selectOption)
            {
                case OptionMenu.FullScreen:
                    selectOption = OptionMenu.Sound;
                    break;

                case OptionMenu.SizeScreen:
                    selectOption = OptionMenu.FullScreen;
                    break;

                case OptionMenu.Sound:
                    selectOption = OptionMenu.SizeScreen;
                    break;
            }

            timer = 0;
        }
        
        switch (ChangeSelectionMouse.NameGameObjectSelect)
        {
            case "FullScreen":
                selectOption = OptionMenu.FullScreen;
                break;

            case "ScreenSize":
                selectOption = OptionMenu.SizeScreen;
                break;

            case "Sounds":
                selectOption = OptionMenu.Sound;
                break;
        }

        switch (selectOption)
        {
            case OptionMenu.FullScreen:
                SelectFullScreen();
                break;

            case OptionMenu.SizeScreen:
                SelectScreenSize();
                break;

            case OptionMenu.Sound:
                SelectSound();
                break;
        }
    }
    #endregion

    #region Set Full Sreen
    void SelectFullScreen()
    {
        FullScreen.GetComponent<TextMeshProUGUI>().color = Color.yellow;
        ScreenSize.GetComponent<TextMeshProUGUI>().color = Color.white;
        Sound.GetComponent<TextMeshProUGUI>().color = Color.white;
        
        if (Input.GetKeyDown(KeyCode.Return) && timer > timerSeconds || Input.GetKeyDown(KeyCode.RightArrow) && timer > timerSeconds 
            || Input.GetKeyDown(KeyCode.LeftArrow) && timer > timerSeconds || Input.GetMouseButtonDown(0) && timer > timerSeconds)
        {
            string switchScreen;

            if (!Screen.fullScreen)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                switchScreen = "ON";
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                switchScreen = "OFF";
            }
            FullScreen.GetComponent<TextMeshProUGUI>().text = $"Full Screen: {switchScreen}";
        }
    }
    #endregion

    #region Create ScreenResolution 
    void CreateScreenResolution()
    {
        allScreenResolution[0] = new Vector2(480, 270);
        allScreenResolution[1] = new Vector2(960, 540);
        allScreenResolution[2] = new Vector2(1280, 720);
        allScreenResolution[3] = new Vector2(1920, 1080);
    }
    #endregion

    #region Set Screen Size
    void SelectScreenSize()
    {
        ScreenSize.GetComponent<TextMeshProUGUI>().color = Color.yellow;
        FullScreen.GetComponent<TextMeshProUGUI>().color = Color.white;
        Sound.GetComponent<TextMeshProUGUI>().color = Color.white;

        if (Input.GetKeyDown(KeyCode.RightArrow) && timer > timerSeconds || Input.GetMouseButtonDown(0) && timer > timerSeconds)
        {
            switch (SetScreen)
            {
                case 0:
                    SetScreen = 1;
                    break;

                case 1:
                    SetScreen = 2;
                    break;

                case 2:
                    SetScreen = 3;
                    break;

                case 3:
                    SetScreen = 0;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && timer > timerSeconds)
        {
            switch (SetScreen)
            {
                case 0:
                    SetScreen = 3;
                    break;

                case 1:
                    SetScreen = 0;
                    break;

                case 2:
                    SetScreen = 1;
                    break;

                case 3:
                    SetScreen = 2;
                    break;
            }
        }

        switch (SetScreen)
        {
            case 0:
                ScreenResolution = allScreenResolution[0];
                break;

            case 1:
                ScreenResolution = allScreenResolution[1];
                break;

            case 2:
                ScreenResolution = allScreenResolution[2];
                break;

            case 3:
                ScreenResolution = allScreenResolution[3];
                break;
        }

        Screen.SetResolution((int)ScreenResolution.x, (int)ScreenResolution.y, Screen.fullScreenMode);
        ScreenSize.GetComponent<TextMeshProUGUI>().text = $"Screen Size: {ScreenResolution.x} x {ScreenResolution.y}";
    }
    #endregion

    #region Set Sound Volume
    void SelectSound()
    {
        Sound.GetComponent<TextMeshProUGUI>().color = Color.yellow;
        ScreenSize.GetComponent<TextMeshProUGUI>().color = Color.white;
        FullScreen.GetComponent<TextMeshProUGUI>().color = Color.white;

        if (Input.GetKey(KeyCode.LeftArrow) && timer > timerSeconds && GameSoundVolume > 0 
            || Input.GetMouseButton(0) && timer > timerSeconds && GameSoundVolume > 0)
        {
            GameSoundVolume -= 0.01f;
            AudioListener.volume = GameSoundVolume;
            Sound.GetComponent<TextMeshProUGUI>().text = $"Sounds Volume: {(int)(GameSoundVolume * 100)}";
            timer = 0;
            
        }
        else if(Input.GetKey(KeyCode.RightArrow) && timer > timerSeconds && GameSoundVolume < 1 
            || Input.GetMouseButton(1) && timer > timerSeconds && GameSoundVolume < 1)
        {
            GameSoundVolume += 0.01f;
            AudioListener.volume = GameSoundVolume;
            Sound.GetComponent<TextMeshProUGUI>().text = $"Sounds Volume: {(int)(GameSoundVolume * 100)}";
            timer = 0;
        }
    }
    #endregion
}
