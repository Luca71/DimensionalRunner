using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float RunSpeed = 10f;
    public GameObject GravePrefab;
    public GameObject DieAnimation;

    float horizontalMove = 0f;
    float verticalSpeed = 0f; // for jump animation
    bool canJump = false;
    bool canMove = true;
    Animator myAnim;
    SpriteRenderer spriteRenderer;

    // steps sound
    [SerializeField] AudioClip stepSounds;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Transform startSpawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // debug
        if (Input.GetKeyDown(KeyCode.F))
            Die();
        
        if (canMove)
        {
            // Horizontal move
            horizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;
            myAnim.SetFloat("speed", Mathf.Abs(horizontalMove));

            // Jump animation
            if (Input.GetButtonDown("Jump"))
            {
                canJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        myAnim.SetFloat("vSpeed", verticalSpeed);
        verticalSpeed = controller.m_Rigidbody2D.velocity.y;
        if(canMove)
            controller.Move(horizontalMove * Time.fixedDeltaTime, canJump);
        canJump = false;
    }

    // Die or interact with other obj
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Die();
        }

        if (other.CompareTag("InteractableObj"))
            other.GetComponent<Interactable>().PlayAnimation();
    }

    public void PlaySteps()
    {
        audioSource.PlayOneShot(stepSounds);
    }

    void Die()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector3 currPos = transform.position;
        AnimationAndMovementState(false);
        SpawnGraveStone(currPos + Vector3.up);
        Invoke("ResetStartPosition", 3);
    }

    public void AnimationAndMovementState(bool state)
    {
        spriteRenderer.enabled = state;
        myAnim.enabled = state;
        DieAnimation.SetActive(!state);
        canMove = state;
    }

    private void SpawnGraveStone(Vector3 position)
    {
        Instantiate(GravePrefab, position, Quaternion.identity);
    }

    public void ResetStartPosition()
    {
        transform.position = startSpawnPoint.position;
        AnimationAndMovementState(true);
    }

    public void CanMoveToggle(bool state)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //spriteRenderer.enabled = state;
        myAnim.enabled = state;
        canMove = state;
    }

}
