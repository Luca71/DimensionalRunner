using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeDimensionController : MonoBehaviour
{
    public GameObject Dimension1, Dimension2;

    [Range(0, 1f)] [SerializeField] float transparency;
    TilemapRenderer renderer1, renderer2;
    TilemapCollider2D collider1, collider2;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer1 = Dimension1.GetComponent<TilemapRenderer>();
        renderer2 = Dimension2.GetComponent<TilemapRenderer>();
        collider1 = Dimension1.GetComponent<TilemapCollider2D>();
        collider2 = Dimension2.GetComponent<TilemapCollider2D>();
        GrayscaleToggle(renderer2, collider2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GrayscaleToggle(renderer1, collider1);
            GrayscaleToggle(renderer2, collider2);
        }
    }

    private void GrayscaleToggle(TilemapRenderer renderer, TilemapCollider2D collider)
    {
        // change shader
        float amount = renderer.material.GetFloat("_GrayscaleAmount") == 0 ? 1 : 0;
        renderer.material.SetFloat("_GrayscaleAmount", amount);
        Color exColor = renderer.material.GetColor("_Color");
        float alpha = exColor.a < 1 ? 1 : transparency;
        renderer.material.color = new Color(exColor.r, exColor.g, exColor.b, alpha);

        // disable collider
        bool state = collider.enabled == true ? false : true;
        collider.enabled = state;
    }
}
