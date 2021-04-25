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

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 point = other.transform.position + Vector3.up * 0.48f;
            if (myCol.bounds.Contains(point))
            {
                other.transform.position += Vector3.down * 0.5f;
            }
            Debug.Log(myCol.bounds.Contains(point));
        }
    }
}
