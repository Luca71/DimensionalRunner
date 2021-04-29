using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyFallState { idle, charge, attack, afterAttack}

public class EnemyMovementFall : MonoBehaviour
{
    public float ActionDistance;
    public float SpeedFall;
    public float SpeedReturn;
    public Sprite secondSprite;

    private GameObject[] Player;
    private Rigidbody2D myRigidbody;
    private Vector2 originalPosition;
    private bool fall = false;
    private EnemyFallState EnemyState;
    private float timer = 0;
    private Sprite FirstSprite;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player");
        myRigidbody = GetComponent<Rigidbody2D>();
        FirstSprite = GetComponent<SpriteRenderer>().sprite;
        originalPosition = transform.position;
        EnemyState = EnemyFallState.idle;
        myRigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (EnemyState)
        {
            case EnemyFallState.idle:
                Idle();
                break;

            case EnemyFallState.charge:
                ChargeAttack();
                break;

            case EnemyFallState.attack:
                Fall();
                break;

            case EnemyFallState.afterAttack:
                ReturnInOriginalPos();
                break;
        }
    }

    #region FALL
    void Fall()
    {
        //CADUTA
        myRigidbody.gravityScale = SpeedFall;
    }
    #endregion

    #region CHARGE ATTACK
    void ChargeAttack()
    {
        GetComponent<SpriteRenderer>().sprite = secondSprite;

        if (Player[0].transform.position.x > transform.position.x && Player[0].transform.position.x - transform.position.x < ActionDistance ||
           Player[0].transform.position.x < transform.position.x && transform.position.x - Player[0].transform.position.x < ActionDistance)
        {
            if (transform.position.x == originalPosition.x || transform.position.x < originalPosition.x - 0.1)
            {
                myRigidbody.velocity = new Vector2(1, 0);
            }
            else if (transform.position.x > originalPosition.x + 0.1)
            {
                myRigidbody.velocity = new Vector2(-1, 0);
            }

            //TIMER
            timer += Time.deltaTime;

            if (timer >= 0.5)
            {
                myRigidbody.velocity = Vector2.zero;
                EnemyState = EnemyFallState.attack;
                timer = 0;
            }
        }
        else
        {
            EnemyState = EnemyFallState.idle; 
            timer = 0;
        }
    }
    #endregion

    #region IDLE
    void Idle()
    {
        GetComponent<SpriteRenderer>().sprite = FirstSprite;
        myRigidbody.velocity = Vector2.zero;
        transform.position = originalPosition;

        if (Player[0].transform.position.x > transform.position.x && Player[0].transform.position.x - transform.position.x < ActionDistance ||
           Player[0].transform.position.x < transform.position.x && transform.position.x - Player[0].transform.position.x < ActionDistance)
        {
            EnemyState = EnemyFallState.charge;
        }
    }
    #endregion

    #region RETURN IN ORIGINAL POS
    void ReturnInOriginalPos()
    {
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            myRigidbody.velocity = new Vector2(0, SpeedReturn);
        }

        if (transform.position.y >= originalPosition.y)
        {
            timer = 0;
            EnemyState = EnemyFallState.idle;
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            myRigidbody.gravityScale = 0;
            myRigidbody.velocity = Vector2.zero;
            EnemyState = EnemyFallState.afterAttack;
        }
    }
}
