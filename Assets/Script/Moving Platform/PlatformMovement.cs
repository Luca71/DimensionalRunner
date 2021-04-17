using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public float Speed;
    public Vector3 FinalPos;
    private bool IsMax;
    private Vector3 StartingPos;
    private Vector3 TargetPos;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StartingPos = GetComponent<Transform>().transform.position;
        TargetPos = FinalPos;
        IsMax = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (body.position.x > TargetPos.x && TargetPos==FinalPos)
        {
            Speed = -Speed;
            TargetPos = StartingPos;
            //body.velocity = new Vector2(-Speed * Time.fixedDeltaTime, 0);
         
        }
        else if(body.position.x < TargetPos.x && TargetPos==StartingPos)
        {
            Speed = -Speed;
            TargetPos = FinalPos;
            
        }
        body.velocity = new Vector2(Speed * Time.fixedDeltaTime, 0);
    }
}
