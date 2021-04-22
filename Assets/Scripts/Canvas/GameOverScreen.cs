using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_InputField HighScoreInput;
    [SerializeField] Animator SceneTransitionAnimator;

    private void Start()
    {
        ScoreText.text = ScoreManager.Instance.GetTotalScore().ToString("000000");
        Debug.Log(Application.persistentDataPath);
    }

    public void SaveScoreAndName()
    {
        string name = HighScoreInput.text == "" ? "USER0" : HighScoreInput.text;
        ScoreManager.Instance.SetScore(name, ScoreManager.Instance.GetTotalScore());
    }

    public void ToMainMenu()
    {
        SaveScoreAndName();
        ScoreManager.Instance.SaveBestScore();
        ScoreManager.Instance.LoadBestScore();

        StartCoroutine("ExitScene");
    }

    IEnumerator ExitScene()
    {
        SceneTransitionAnimator.SetTrigger("exit");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("MainMenu");
    }
}