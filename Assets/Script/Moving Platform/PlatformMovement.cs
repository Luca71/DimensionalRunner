using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public float SpeedX, SpeedY;
    public Vector3 FinalPos;
    public bool MoveOnXAxis, MoveOnYAxis;
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
        if(MoveOnXAxis == true)
        {
            if (body.position.x > TargetPos.x && TargetPos==FinalPos)
            {
                SpeedX = -SpeedX;
                TargetPos = StartingPos;
                //body.velocity = new Vector2(-Speed * Time.fixedDeltaTime, 0);
         
            }
            else if(body.position.x < TargetPos.x && TargetPos==StartingPos)
            {
                SpeedX = -SpeedX;
                TargetPos = FinalPos;
            
            }
            body.velocity = new Vector2(SpeedX * Time.fixedDeltaTime, 0);
        }

        if (MoveOnYAxis == true)
        {
            if (body.position.y > TargetPos.y && TargetPos == FinalPos)
            {
                SpeedY = -SpeedY;
                TargetPos = StartingPos;
                //body.velocity = new Vector2(-Speed * Time.fixedDeltaTime, 0);

            }
            else if (body.position.y < TargetPos.y && TargetPos == StartingPos)
            {
                SpeedY = -SpeedY;
                TargetPos = FinalPos;

            }
            body.velocity = new Vector2( 0, SpeedY * Time.fixedDeltaTime);
        }       
    }
}
