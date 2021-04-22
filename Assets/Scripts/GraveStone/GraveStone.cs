using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : MonoBehaviour
{
	[SerializeField] Transform InfluenceAreaPosition;
    [SerializeField] Vector2 overlapAreaSize;
    [SerializeField] LayerMask playerMask;

    [SerializeField] LayerMask WhatIsGraveStone;
    [SerializeField] Transform m_GroundCheck;
    float k_GroundedRadius = 1f; // Radius of the overlap circle to determine if grounded

    [SerializeField] SpriteRenderer Sprite;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject ghost;

    bool m_Grounded;
    bool canShowGhost;
    Rigidbody2D rb;
    GameObject[] graveStones;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        graveStones = new GameObject[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        Sprite.sprite = GetRandomSprite();
    }

    // Update is called once per frame
    void Update()
    {
        canShowGhost = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Dimension1") || 
            other.gameObject.CompareTag("Dimension2") || other.gameObject.CompareTag("GraveStone"))
            Physics2D.IgnoreCollision(other.collider, GetComponent<BoxCollider2D>());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DestroyGravestone>() != null)
            Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        
        // destroy other closer GraveStone
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_GroundedRadius, WhatIsGraveStone);
        if (colliders.Length > 1)
        {
            for (int i = 1; i < colliders.Length; i++)
            {
                Destroy(colliders[i].gameObject);
            }
        }

        // show ghost animation
        Collider2D[] animColliders = Physics2D.OverlapBoxAll(InfluenceAreaPosition.position, overlapAreaSize, 0f, playerMask);
        for (int i = 0; i < animColliders.Length; i++)
        {
            if (animColliders[i].gameObject != gameObject)
                canShowGhost = true;
        }

        rb.gravityScale = m_Grounded == true ? 0 : 1;
        if(m_Grounded)
            rb.velocity = Vector2.zero;
        bool state = canShowGhost == true ? true : false;
        ghost.SetActive(state);
    }

    Sprite GetRandomSprite()
    {
        return sprites[Random.Range(0, sprites.Length)];
    }
}
