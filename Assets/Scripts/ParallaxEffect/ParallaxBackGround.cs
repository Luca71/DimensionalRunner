using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    private float length, startpos;
    public Camera Camera;
    public SpriteRenderer LayerImage;
    public float parallaxEffect;
    public Sprite[] dimensions = new Sprite[2];
    private int index = 0;

    private void Start()
    {
        startpos = transform.position.x;
        length = LayerImage.bounds.size.x;
    }

    private void FixedUpdate()
    {

        float temp = (Camera.transform.position.x * (1 - parallaxEffect));
        float dist = (Camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);


        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;

    }
    public void ChangeSprite()
    {
        if (dimensions[0] == null || dimensions[1] == null) return;
        index++;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = dimensions[index % dimensions.Length];
        }
    }
}
