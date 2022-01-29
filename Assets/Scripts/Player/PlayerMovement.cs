using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Rigidbody2D myRb;
    BoxCollider2D myCollider;

    // steps sound
    [SerializeField] AudioClip stepSounds;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Transform startSpawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myRb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // Horizontal move
            horizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;
            myAnim.SetFloat("speed", Mathf.Abs(horizontalMove));
            myAnim.SetFloat("vSpeed", verticalSpeed);

            // Jump animation
            if (Input.GetButtonDown("Jump"))
            {
                canJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
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
        audioSource.PlayOneShot(dieSound);
        myRb.velocity = Vector2.zero;
        myRb.gravityScale = 0;
        myCollider.enabled = false;
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
        myRb.gravityScale = 1;
        myCollider.enabled = true;
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
