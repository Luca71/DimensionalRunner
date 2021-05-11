using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] GameObject HighScoreRoot;
    [SerializeField] GameObject NoHighScoreRoot;
    [SerializeField] TMP_InputField HighScoreInput;
    [SerializeField] Animator SceneTransitionAnimator;

    bool canSaveName;

    private void Start()
    {
        canSaveName = false;
        HighScoreRoot.SetActive(false);
        NoHighScoreRoot.SetActive(false);
        ScoreManager.Instance.ResetTimer(); // calculte the total gamplay time before subtratting it from score;
        ScoreText.text = ScoreManager.Instance.GetTotalScore().ToString("000000");
        Debug.Log(Application.persistentDataPath);
        if (!ScoreManager.Instance.CheckScoreGraduatory(ScoreManager.Instance.GetTotalScore()))
        {
            // enable not in high score dialog box
            NoHighScoreRoot.SetActive(true);
        }
        else {

            HighScoreRoot.SetActive(true);
            canSaveName = true;
        }
    }

    public void SaveScoreAndName()
    {
        string name = HighScoreInput.text == "" ? "USER0" : GetPlayerName();
        ScoreManager.Instance.SetScore(name, ScoreManager.Instance.GetTotalScore());
    }

    private string GetPlayerName()
    {
        return HighScoreInput.text.Length > 5 ? HighScoreInput.text.Substring(0, 5) : HighScoreInput.text;
    }

    public void ToMainMenu()
    {
        if(canSaveName)
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
