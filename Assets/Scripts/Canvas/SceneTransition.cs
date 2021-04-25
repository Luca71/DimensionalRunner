using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Image>().enabled = true;
    }
}
