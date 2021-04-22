using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviours : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddCoins(value);
            other.GetComponent<AudioSource>().PlayOneShot(coinSound);
            Destroy(gameObject);
        }
    }
}
