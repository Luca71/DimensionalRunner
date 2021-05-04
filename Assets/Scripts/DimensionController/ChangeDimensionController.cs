using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeDimensionController : MonoBehaviour
{
    public GameObject Dimension1, Dimension2;

    public static bool canChange = true;

    [Range(0, 1f)] [SerializeField] float transparency;
    TilemapRenderer renderer1, renderer2;
    CompositeCollider2D collider1, collider2;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer1 = Dimension1.GetComponent<TilemapRenderer>();
        renderer2 = Dimension2.GetComponent<TilemapRenderer>();
        collider1 = Dimension1.GetComponent<CompositeCollider2D>();
        collider2 = Dimension2.GetComponent<CompositeCollider2D>();
        GrayscaleToggle(renderer2, collider2);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canChange)
        {
            GrayscaleToggle(renderer1, collider1);
            GrayscaleToggle(renderer2, collider2);
            ChangeDimensionSprite.instance.ChangeDimension();
            audioSource.Play();
        }
    }

    private void GrayscaleToggle(TilemapRenderer renderer, CompositeCollider2D collider)
    {
        // change shader
        float amount = renderer.material.GetFloat("_GrayscaleAmount") == 0 ? 1 : 0;
        renderer.material.SetFloat("_GrayscaleAmount", amount);
        Color exColor = renderer.material.GetColor("_Color");
        float alpha = exColor.a < 1 ? 1 : transparency;
        renderer.material.color = new Color(exColor.r, exColor.g, exColor.b, alpha);
        bool st = collider.isTrigger;
        collider.isTrigger = !st;
        collider.GenerateGeometry();
    }
}
