using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float ActionDistance;
    public float Speed;
    public Sprite secondSprite;

    private GameObject[] Player;
    private Rigidbody2D myRigidbody;
    private Sprite FirstSprite;
    private bool Stop = false;

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
            myRigidbody.velocity = new Vector2(Speed, 0);
            GetComponent<SpriteRenderer>().sprite = secondSprite;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x - Player[0].transform.position.x < ActionDistance &&
                transform.position.x > Player[0].transform.position.x)
             {            
                Stop = true;

                //SEGUI VERSO SX
                myRigidbody.velocity = new Vector2(-Speed, 0);
                GetComponent<SpriteRenderer>().sprite = secondSprite;
                GetComponent<SpriteRenderer>().flipX = false;
             }
        else if (Stop == true)
        {
            GetComponent<SpriteRenderer>().sprite = FirstSprite;
            myRigidbody.velocity = new Vector2(0, 0);
            Stop = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Player[0].transform.position = new Vector2(-1, 10);
        }
    }
}
