using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public static string buttonName;
    // Start is called before the first frame update
    void Start()
    {
        SetButtonName();
    }

    //Setting Button Names to use for creation of words
    void SetButtonName()
    {
        gameObject.name = GetComponent<Button>().GetComponentInChildren<TMP_Text>().text;
        buttonName = gameObject.name;
    }
}
