using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] TMP_Text dialogText;
    [SerializeField] GameObject dialogBox;
    //public string dialogLines;

    public static DialogManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DialogBoxState(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DialogBoxState(bool state)
    {
        dialogBox.SetActive(state);
    }

    public void SetText(string text)
    {
        dialogText.text = text;
    }
}
