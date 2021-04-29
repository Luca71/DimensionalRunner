using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSelectionMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static string NameGameObjectSelect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        NameGameObjectSelect = gameObject.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        NameGameObjectSelect = "";
    }
}
