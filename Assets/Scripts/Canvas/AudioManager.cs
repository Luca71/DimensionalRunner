using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioClip[] audioClips;

    AudioSource audioSource;
    Scene currentScene;
    public static AudioManager instance;

    float volume;

    float dVelocity = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        audioSource = GetComponent<AudioSource>();
        mixer.SetFloat("MasterVolume", -50);
        ChangeAudioClip(currentScene);
        audioSource.Play();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene() != currentScene)
        {
            currentScene = SceneManager.GetActiveScene();
            ChangeAudioClip(SceneManager.GetActiveScene());
        }
        float value;
        mixer.GetFloat("MasterVolume", out value);
        if (value < 0f)
            FadeAudioIn(value);
    }


    public void ChangeAudioClip(Scene curScene)
    {
        if (audioSource.clip != audioClips[curScene.buildIndex])
        {
            audioSource.clip = audioClips[curScene.buildIndex];
            mixer.SetFloat("MasterVolume", -50);
            audioSource.Play();
        }
    }

    private void FadeAudioIn(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.SmoothDamp(value, 0, ref dVelocity, 0.3f));
    }

    public float GetMusicVolume()
    {
        mixer.GetFloat("MusicVolume", out volume);
        return volume;
    }
    public float GetSFXVolume()
    {
        mixer.GetFloat("SFXVolume", out volume);
        return volume;
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", value);
    }
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
    }
}
