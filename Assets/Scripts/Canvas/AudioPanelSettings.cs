using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AudioPanelSettings : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Animator SceneTransitionAnimator;

    private void Start()
    {
        musicSlider.value = AudioManager.instance.GetMusicVolume();
        SFXSlider.value = AudioManager.instance.GetSFXVolume();
    }

    public void SetMusic()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
    }
    public void SetFsx()
    {
        AudioManager.instance.SetSFXVolume(SFXSlider.value);
    }
    public void ToMainMenu()
    {
        SaveSoundsVolume();
        StartCoroutine("ExitScene");
    }

    public void SaveSoundsVolume()
    {
        ScoreManager.Instance.SetSoundsVolume(musicSlider.value, SFXSlider.value);
    }

    IEnumerator ExitScene()
    {
        SceneTransitionAnimator.SetTrigger("exit");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("MainMenu");
    }
}
