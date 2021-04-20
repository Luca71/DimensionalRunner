using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : MonoBehaviour
{
	[SerializeField] Transform InfluenceAreaPosition;
    [SerializeField] Vector2 overlapAreaSize;
    [SerializeField] LayerMask playerMask;

    [SerializeField] LayerMask m_WhatIsGround;
    [SerializeField] Transform m_GroundCheck;
    float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    [SerializeField] SpriteRenderer Sprite;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject ghost;

    bool m_Grounded;
    bool canShowGhost;
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("GraveStone"))
    //        Destroy(other.gameObject);
    //}

    private void FixedUpdate()
    {
        // Check ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
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
