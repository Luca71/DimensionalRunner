using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AvoidInnerCollision : MonoBehaviour
{
    private CompositeCollider2D myCol;

    void Start()
    {
        myCol = GetComponent<CompositeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            ChangeDimensionController.canChange = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            ChangeDimensionController.canChange = true;
    }

    //private void OnCollisionStay2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Vector3 point = other.transform.position + Vector3.up * 0.48f;
    //        if (myCol.bounds.Contains(point))
    //        {
    //            myCol.isTrigger = true;
    //        }
    //    }
    //}

    //private void OnCollisionEnter2D (Collision2D collision)
    //{
    //    Debug.Log("Ontriggerenter");
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        myCol.isTrigger = false;
    //    }
    //}
}
