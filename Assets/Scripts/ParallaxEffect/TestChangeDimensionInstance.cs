using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeDimensionInstance : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeDimensionSprite.instance.ChangeDimension();
    }
}
