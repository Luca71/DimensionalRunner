using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float Angle = 180f;

    void Update()
    {
        transform.Rotate(Vector3.forward, Angle * Time.deltaTime);
    }
}
