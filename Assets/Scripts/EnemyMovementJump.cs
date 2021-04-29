using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementJump : MonoBehaviour
{
    public float ActionDistance;
    public float Speed;
    public float jump;
    public Sprite secondSprite;

    private GameObject[] Player;
    private Rigidbody2D myRigidbody; 
    private Sprite FirstSprite;
    private bool Stop = false;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player");
        myRigidbody = GetComponent<Rigidbody2D>();
        FirstSprite = GetComponent<SpriteRenderer>().sprite;
        myRigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //FOLLOW
        if (transform.position.x < Player[0].transform.position.x &&
            Player[0].transform.position.x - transform.position.x < ActionDistance)
        {
            Stop = true;

            //SEGUI VERSO DX
            myRigidbody.velocity = new Vector2 (Speed, myRigidbody.velocity.y);
            GetComponent<SpriteRenderer>().sprite = secondSprite;
            GetComponent<SpriteRenderer>().flipX = true;

            Jump();
        }
        else if (transform.position.x - Player[0].transform.position.x < ActionDistance &&
                transform.position.x > Player[0].transform.position.x)
        {
                Stop = true;

                //SEGUI VERSO SX
                myRigidbody.velocity = new Vector2(-Speed, myRigidbody.velocity.y);
                GetComponent<SpriteRenderer>().sprite = secondSprite;
                GetComponent<SpriteRenderer>().flipX = false;

                Jump();
        }
        else
        {
            if (Stop == true)
            {
                GetComponent<SpriteRenderer>().sprite = FirstSprite;
                myRigidbody.velocity = new Vector2(0, 0);
                Stop = false;
                timer = 0;
            }
        }
    }

    void Jump()
    {
        //SALTO
        if (Player[0].transform.position.y > transform.position.y)
        {
            timer += Time.deltaTime;

            if(timer >= 0.7)
            {
                myRigidbody.velocity = Vector2.up * jump;
                timer = 0;
            }
        }
    }
}
