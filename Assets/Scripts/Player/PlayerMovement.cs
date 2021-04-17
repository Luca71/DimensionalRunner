using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float RunSpeed = 10f;

    float horizontalMove = 0f;
    float verticalSpeed = 0f; // for jump animation
    bool canJump = false;
    Animator myAnim;

    // steps sound
    [SerializeField] AudioClip stepSounds;
    [SerializeField] AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal move
        horizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;
        myAnim.SetFloat("speed", Mathf.Abs(horizontalMove));

        // Jump animation
        myAnim.SetFloat("vSpeed", verticalSpeed);
        if (Input.GetButtonDown("Jump"))
        {
            canJump = true;
        }
    }

    private void FixedUpdate()
    {
        verticalSpeed = controller.m_Rigidbody2D.velocity.y;
        controller.Move(horizontalMove * Time.fixedDeltaTime, canJump);
        canJump = false;
    }

    public void PlaySteps()
    {
        audioSource.PlayOneShot(stepSounds);
    }
}
