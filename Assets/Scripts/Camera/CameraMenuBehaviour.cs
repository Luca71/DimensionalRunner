using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuBehaviour : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
