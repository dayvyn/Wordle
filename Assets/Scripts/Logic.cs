using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Logic : MonoBehaviour
{
    [SerializeField] Button[] alphabetBoxes;
    Display displayScript;
    int row;
    string answer = "HELLO";
    string guessedWord;
    char[] letterArray;
    char[] answerArray;
    int[,] inputBoxes = new int[5,5];
    int num;
    [SerializeField] TextAsset allowedWords;
    // Start is called before the first frame update
    void Start()
    {
        displayScript = GetComponent<Display>();
        alphabetBoxes = GameObject.FindObjectsByType<Button>(FindObjectsSortMode.InstanceID);
        Set2DArray();
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
            letterArray = guessedWord.ToCharArray();
            answerArray = answer.ToCharArray();
            Dictionary<char, int> letterFrequency = new Dictionary<char, int>();
            Dictionary<char, int> letterCounter = new Dictionary<char, int>();
            foreach (char c in answer)
                {
                    if (letterFrequency.ContainsKey(c))
                    {
                        letterFrequency[c] += 1;
                    }
                    else
                    {
                        letterFrequency.Add(c, 1);
                    }
                }
            foreach (char c in answer)
            {
                if (!letterCounter.ContainsKey(c))
                {
                    letterCounter.Add(c, 0);
                }
            }
            for (int h = 0; h < letterArray.Length; h++)
            {
                if (letterArray[h] == answerArray[h] && letterCounter[letterArray[h]] != letterFrequency[letterArray[h]])
                { 
                    displayScript.ChangeToGreen(inputBoxes[GetRow(), h]);
                    letterCounter[letterArray[h]] += 1;
                }
            }
            for (int h = 0; h < letterArray.Length; h++)
            {
                if (answer.Contains(letterArray[h]) && letterCounter[letterArray[h]] != letterFrequency[letterArray[h]])
                {
                    displayScript.ChangeToYellow(inputBoxes[GetRow(), h]);
                    letterCounter[letterArray[h]] += 1;
                }
            }

            row++;
            displayScript.SetFilledBox();
        }
    }
    void Set2DArray()
    {
        for (int j = 0; j < 5; j++)
        {
            for (int p = 0; p < 5; p++)
            {
                inputBoxes[j, p] = num++;
                Debug.Log(inputBoxes[j, p]);
            }
        }
    }
}