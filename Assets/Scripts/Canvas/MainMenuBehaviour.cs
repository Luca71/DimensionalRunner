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
        StartCoroutine("ExitScene");
    }

    IEnumerator ExitScene()
    {
        SceneTransitionAnimator.SetTrigger("exit");
        ScoreManager.Instance.ResetCoinToZero();
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("TutorialScene");
    }
}
