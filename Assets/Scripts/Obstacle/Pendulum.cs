using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    ConstantForce2D cForce;
    public float Period = 2f;
    public float Force = 10f;
    float currentTimePassed = 0f;
    Vector2 ForceVec;
    void Start()
    {
        cForce = GetComponent<ConstantForce2D>();
        ForceVec.x = Force;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimePassed += Time.deltaTime;
        if (currentTimePassed > Period)
        {
            currentTimePassed = 0f;
            Force = -Force;
            ForceVec.x = Force;
        }
        cForce.relativeForce = ForceVec;
    }
}
