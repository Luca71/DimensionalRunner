using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Animator SceneTransitionAnimator;

    bool isActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // pause game
        {
            if (!isActive)
            {
                PauseMenu.SetActive(true);
                isActive = true;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
                isActive = false;
            }
        }
    }

    public void ResumeGame()
    {
        if (isActive)
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            isActive = false;
        }
    }

    public void ToMainMenu()
    {
        if (isActive)
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            isActive = false;
            StartCoroutine("ExitScene");
        }
    }

    IEnumerator ExitScene()
    {
        SceneTransitionAnimator.SetTrigger("exit");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("MainMenu");
    }
}
