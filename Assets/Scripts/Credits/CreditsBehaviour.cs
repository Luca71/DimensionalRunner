using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject CreditsMenu;
    CircleCollider2D coll;
    bool isVisited;

    private void Start()
    {
        coll = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isVisited)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isVisited = true;
                OpenTab();
            }
        }
    }

    public void CloseTab()
    {
        Time.timeScale = 1;
        CreditsMenu.SetActive(false);
    }
    public void OpenTab()
    {
        Time.timeScale = 0;
        CreditsMenu.SetActive(true);
    }
}
