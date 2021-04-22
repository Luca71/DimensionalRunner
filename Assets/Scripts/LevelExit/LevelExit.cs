using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] Animator SceneTransitionAnimator;
    [SerializeField] AudioClip endLevelSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<AudioSource>().PlayOneShot(endLevelSound);
            StartCoroutine("ExitScene");
        }
    }

    IEnumerator ExitScene()
    {
        SceneTransitionAnimator.SetTrigger("exit");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("EndLevelScene");
    }
}
