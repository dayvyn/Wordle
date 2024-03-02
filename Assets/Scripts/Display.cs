using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Display : MonoBehaviour
{
    [SerializeField] TMP_InputField[] letterBoxes;
    Logic logical;
    int choice;
    public string guess;
    bool lastFilled = false;
   void Start()
    {
        choice = 0; 
        logical = GetComponent<Logic>();
    }
    public void DisplayLetter(string name)
    {
       if (letterBoxes[choice].GetComponent<TMP_InputField>().text == "")
       {
           letterBoxes[GetBoxOfChoice()].GetComponent<TMP_InputField>().text = name;
       }
       else
       {
           choice = GetBoxOfChoice();
           letterBoxes[choice].GetComponent<TMP_InputField>().text = name;
       }
    }
    int GetBoxOfChoice()
    {
        switch (logical.GetRow())
        {
            case 0:
                if (choice < 4)
                {
                    return choice++;
                }
                else 
                {
                    lastFilled = true;
                    return choice;
                }
            case 1:
                if (choice < 9 && !lastFilled)
                {
                    return choice++;
                }
                else
                {
                    lastFilled = true;
                    return choice;
                }
            case 2:
                if (choice < 14 && !lastFilled)
                {
                    return choice++;
                }
                else
                {
                    lastFilled = true;
                    return choice;
                }
            case 3:
                if (choice < 19 && !lastFilled)
                {
                    return choice++;
                }
                else
                {
                    lastFilled = true;
                    return choice;
                }
            case 4:
                if (choice < 24 && !lastFilled)
                {
                    return choice++;
                }
                else
                {
                    lastFilled = true;
                    return choice;
                }
            default:
                return 0;
        }
    }
    public bool GetFilledBox()
    {
        return lastFilled;
    }
    public void SetFilledBox()
    {
        lastFilled = false;
        choice++;
    }
    public void RemoveLetter()
    {
        if (choice % 5 == 0)
        {
            letterBoxes[choice].GetComponent<TMP_InputField>().text = "";
        }
        else if ((choice == 4 || choice == 9 || choice == 14 || choice == 19 || choice == 24) && lastFilled == true)
        {
            letterBoxes[choice].GetComponent<TMP_InputField>().text = "";
            lastFilled = false;
        }
        else if ((choice == 4 || choice == 9 || choice == 14 || choice == 19 || choice == 24))
        {
            letterBoxes[--choice].GetComponent<TMP_InputField>().text = "";
        }
        else 
        {
            letterBoxes[--choice].GetComponent<TMP_InputField>().text = "";
        }
    }

    public string FormWord()
    {
            switch (logical.GetRow())
            {
                case 0:
                    guess = "";
                    for (int k = 0; k < 5; k++)
                    {
                        guess += string.Concat(letterBoxes[k].GetComponent<TMP_InputField>().text);
                    }
                    return guess;
                case 1:
                    guess = "";
                    for (int k = 5; k < 10; k++)
                    {
                         guess += string.Concat(letterBoxes[k].GetComponent<TMP_InputField>().text);
                }
                    return guess;
                case 2:
                    guess = "";
                    for (int k = 10; k < 15; k++)
                    {
                        guess += string.Concat(letterBoxes[k].GetComponent<TMP_InputField>().text);
                    }
                    return guess;
                case 3:
                    guess = "";
                    for (int k = 15; k < 20; k++)
                    {
                        guess += string.Concat(letterBoxes[k].GetComponent<TMP_InputField>().text);
                    }
                    return guess;
                case 4:
                    guess = "";
                    for (int k = 20; k < 25; k++)
                    {
                       guess += string.Concat(letterBoxes[k].GetComponent<TMP_InputField>().text);
                    }
                    return guess;
                default:
                    return null;
            }
        }
       
}
