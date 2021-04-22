using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float Speed;
    public Vector3 FinalPos;
    private Vector3 StartingPos;
    private Vector3 TargetPos;

    // Start is called before the first frame update
    void Start()
    {
        StartingPos = transform.position;
        FinalPos += StartingPos;
        TargetPos = FinalPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(TargetPos.x, transform.position.x) && Mathf.Approximately(TargetPos.y, transform.position.y))
            if (TargetPos == FinalPos)
            {
                TargetPos = StartingPos;
            }
            else
            {
                TargetPos = FinalPos;
            }

        transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime);

        
        //newVelocity = Vector2.zero;

        //if (MoveOnXAxis == true)
        //{
        //    if (body.position.x > TargetPos.x && TargetPos.x == FinalPos.x)
        //    {
        //        SpeedX = -SpeedX;
        //        TargetPos.x = StartingPos.x;
        //        //body.velocity = new Vector2(-Speed * Time.fixedDeltaTime, 0);
         
        //    }
        //    else if(body.position.x < TargetPos.x && TargetPos.x ==StartingPos.x)
        //    {
        //        SpeedX = -SpeedX;
        //        TargetPos.x = FinalPos.x;
            
        //    }
        //    newVelocity.x = SpeedX * Time.deltaTime;
        //}

        //if (MoveOnYAxis == true)
        //{
        //    if (body.position.y > TargetPos.y && TargetPos.y == FinalPos.y)
        //    {
        //        SpeedY = -SpeedY;
        //        TargetPos.y = StartingPos.y;
        //        //body.velocity = new Vector2(-Speed * Time.fixedDeltaTime, 0);

        //    }
        //    else if (body.position.y < TargetPos.y && TargetPos.y == StartingPos.y)
        //    {
        //        SpeedY = -SpeedY;
        //        TargetPos.y = FinalPos.y;

        //    }
        //    newVelocity.y = SpeedY * Time.deltaTime;
        //}
        //body.AddForce(newVelocity, ForceMode2D.Force);
    }
}
