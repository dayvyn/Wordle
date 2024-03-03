using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Display : MonoBehaviour
{
    [SerializeField] TMP_InputField[] letterBoxes;
    Logic logical;
    int choice;
    string guess;
    bool lastFilled = false;
    public GameObject[] gamePanels;
    public GameObject endPanel;
    [SerializeField] TMP_Text verdict;
   void Start()
    {
        choice = 0; 
        logical = GetComponent<Logic>();
    }
    //Pushes name of Button to the text of the InputField
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
    //choice cannot go past the row until SetFilledGox() allows it
    //There is a much better way to do this, I'm sure, but it works
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
    //There is a better way of doing this, but I'm not sure exactly. Using modulo seems like a good alternative since its adding 5 each time, but I'm unsure how to do it with an initial offset of 4
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

    //Forming the word from getting the text from each InputField
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
    public void ChangeToGreen(int pos)
    {
        letterBoxes[pos].GetComponent<Image>().color = Color.green;
    }
    public void ChangeToYellow(int pos)
    {
        letterBoxes[pos].GetComponent<Image>().color = Color.yellow;
    }
    public void ChangeToGrey(int pos)
    {
        if(letterBoxes[pos].GetComponent<Image>().color != Color.green && letterBoxes[pos].GetComponent<Image>().color != Color.yellow)
        {
            letterBoxes[pos].GetComponent<Image>().color = Color.grey;
        }
        
    }
    public void ChangeDisplay(bool correct)
    {
       StartCoroutine(DisplayChange(correct));
    } 
    IEnumerator DisplayChange(bool correct)
    {
        //Can't keep spamming buttons after you're finished
        foreach (Button letter in logical.GetAlphabetBoxes())
        {
            letter.interactable = false;
        }
        yield return new WaitForSeconds(1);
        foreach (GameObject panel in gamePanels)
        {
            panel.SetActive(false);
        }
        endPanel.SetActive(true);
        if (correct)
        {
            verdict.text = "Congratulations!\nYour answer\n was:\n" + logical.GetAnswer();
        }
        else
        {
            verdict.text = "Sorry...\nYour answer\n was:\n" + logical.GetAnswer();
        }

    }
}
