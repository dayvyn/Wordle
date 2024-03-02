using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Logic : MonoBehaviour
{
    [SerializeField] Button[] alphabetBoxes;
    Display displayScript;
    public int row;
    string answer;
    public string guessedWord;
    // Start is called before the first frame update
    void Start()
    {
        displayScript = GetComponent<Display>();
        alphabetBoxes = GameObject.FindObjectsByType<Button>(FindObjectsSortMode.InstanceID);
        for (int i = 0; i < alphabetBoxes.Length; i++)
        {
            SetNames(i);
        }
    }

    void SetNames(int k)
    {
        if (alphabetBoxes[k].name != "Enter" && alphabetBoxes[k].name != "Back")
        {
            alphabetBoxes[k].onClick.AddListener(() => displayScript.DisplayLetter(alphabetBoxes[k].name));
        }
        else if (alphabetBoxes[k].name == "Enter")
        {
            alphabetBoxes[k].onClick.AddListener(()=> CheckAnswer());
        }
        else if (alphabetBoxes[k].name == "Back")
        {
            alphabetBoxes[k].onClick.AddListener(() => displayScript.RemoveLetter());
        }
    }
    public int GetRow()
    {
        return row;
    }

    void CheckAnswer()
    {
        if (displayScript.GetFilledBox() == true)
        {
            guessedWord = displayScript.FormWord();
            row++;
            displayScript.SetFilledBox();
        }
    }

}