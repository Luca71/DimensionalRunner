using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChest : MonoBehaviour
{
    [SerializeField] int value = 100;
    [SerializeField] AudioClip coinSound;

    private bool isOpen;
    private Animator anim;
    private ParticleSystem particles;

    private void Start()
    {
        anim = GetComponent<Animator>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isOpen)
            {
                anim.enabled = true;
                particles.Play();
                ScoreManager.Instance.AddCoins(value);
                other.GetComponent<AudioSource>().PlayOneShot(coinSound);
                isOpen = true;
            }
        }
    }
}
