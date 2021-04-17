using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float smoothTime = .15f;

    public bool XMaxEnable = false;
    public float XMaxValue = 0;
    public bool XMinEnable = false;
    public float XMinValue = 0;

    public bool YMaxEnable = false;
    public float YMaxValue = 0;
    public bool YMinEnable = false;
    public float YMinValue = 0;




    Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 targetPos = Target.position;

        // vertical
        if (YMinEnable && YMaxEnable)
            targetPos.y = Mathf.Clamp(Target.position.y, YMinValue, YMaxValue);
        else if(YMinEnable)
            targetPos.y = Mathf.Clamp(Target.position.y, YMinValue, Target.position.y);
        else if (YMaxEnable)
            targetPos.y = Mathf.Clamp(Target.position.y, Target.position.y, YMaxValue);

        // horizontal
        if (XMinEnable && XMaxEnable)
            targetPos.x = Mathf.Clamp(Target.position.x, XMinValue, XMaxValue);
        else if (XMinEnable)
            targetPos.x = Mathf.Clamp(Target.position.x, XMinValue, Target.position.x);
        else if (XMaxEnable)
            targetPos.x = Mathf.Clamp(Target.position.x, Target.position.x, XMaxValue);

        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
