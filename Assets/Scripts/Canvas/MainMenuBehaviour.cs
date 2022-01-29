using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] Animator SceneTransitionAnimator;

    
    public void TutorialOnClick()
    {
        StartCoroutine(ExitScene( "TutorialScene", true));
    }
    public void SettingsOnClick()
    {
        StartCoroutine(ExitScene( "Settings", false));
    }

    public void PlayOnClick()
    {
        StartCoroutine(ExitScene( "Level1", true));
    }

    IEnumerator ExitScene(string sceneName, bool resetTime)
    {
        SceneTransitionAnimator.SetTrigger("exit");
        ScoreManager.Instance.ResetCoinToZero();
        yield return new WaitForSeconds(0.4f);
        if(resetTime)
            ScoreManager.Instance.ResetTimer();
        SceneManager.LoadScene(sceneName);
    }
}
