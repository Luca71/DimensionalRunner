using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] GameObject NoHighScoreLabel;
    [SerializeField] GameObject HighScoreInput;
    [SerializeField] Animator SceneTransitionAnimator;

    private void Start()
    {
        ScoreText.text = ScoreManager.instance.GetTotalScore().ToString();
    }

    public void ToMainMenu()
    {
        StartCoroutine("ExitScene");
    }

    IEnumerator ExitScene()
    {
        SceneTransitionAnimator.SetTrigger("exit");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("MainMenu");
    }
}
