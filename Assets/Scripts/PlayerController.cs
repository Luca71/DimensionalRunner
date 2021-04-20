using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D myRigidbody;
    private float jump = 10;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.velocity = new Vector2(Speed, myRigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.velocity = new Vector2(-Speed, myRigidbody.velocity.y);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * jump;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            ScoreManager.score += 10;
        }
    }

}
