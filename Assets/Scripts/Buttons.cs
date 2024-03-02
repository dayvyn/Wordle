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

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetButtonName()
    {
        gameObject.name = GetComponent<Button>().GetComponentInChildren<TMP_Text>().text;
        buttonName = gameObject.name;
    }
    public string GetButtonName()
    {
        return buttonName;
    }
}
