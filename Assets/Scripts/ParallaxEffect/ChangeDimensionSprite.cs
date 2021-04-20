using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeDimensionSprite : MonoBehaviour
{
    //To Use instance for changing dimension to layer: ChangeDimensionSprite.instance.ChangeDimension();
    public static ChangeDimensionSprite instance;
    List<ParallaxBackGround> layers = new List<ParallaxBackGround>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        layers.AddRange(transform.GetComponentsInChildren<ParallaxBackGround>());
    }

    public void ChangeDimension()
    {
        foreach (ParallaxBackGround layer in layers)
            layer.ChangeSprite();
    }
}
