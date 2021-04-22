using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("TutorialScene");
    }
}
