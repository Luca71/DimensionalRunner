using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string DialogContent;
    public AudioClip PopSound;

    PlayerMovement player;
    bool isActive;
    bool readed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !readed)
        {
            other.GetComponent<AudioSource>().PlayOneShot(PopSound);
            player.CanMoveToggle(false); // block player movement
            DialogManager.instance.DialogBoxState(true);
            DialogManager.instance.SetText(DialogContent);
            isActive = true;
        }
    }
    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                readed = true;
                DialogManager.instance.SetText("");
                DialogManager.instance.DialogBoxState(false);
                player.CanMoveToggle(true); // allow player movement
            }
        }
    }
}
