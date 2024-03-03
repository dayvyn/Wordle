using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class Logic : MonoBehaviour
{
    [SerializeField] Button[] alphabetBoxes;
    Display displayScript;
    int row;
    public string answer;
    string guessedWord;
    char[] letterArray;
    char[] answerArray;
    //Making 2D array to assign values to every Box so it can accurately locate each one
    int[,] inputBoxes = new int[5,5];
    int num;
    [SerializeField] TextAsset allowedWordsFile;
    [SerializeField] TextAsset possibleWordsFile;
    public string[] allowedWords, possiblewords;
    //Using this since System has its own Random Class and Unity doesn't like that
    System.Random random = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        //Start calls, reads every line of the file and puts it into the created string array
        allowedWords = System.IO.File.ReadAllLines(allowedWordsFile.name + ".txt");
        possiblewords = System.IO.File.ReadAllLines(possibleWordsFile.name + ".txt");
        displayScript = GetComponent<Display>();
        alphabetBoxes = GameObject.FindObjectsByType<Button>(FindObjectsSortMode.InstanceID);
        //Assigning values to every index in 2D array
        Set2DArray();
        //Getting random answer
        answer = possiblewords[random.Next(possiblewords.Length)];
        //Setting names for each and every button
        for (int i = 0; i < alphabetBoxes.Length; i++)
        {
            SetNames(i);
        }
    }
    //Adding basic listener if its a letter and specific if "Back" or "Enter"
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
        //If the word is 5 letters
        if (displayScript.GetFilledBox() == true)
        {
            //Checking if guessedWord exists in words allowed or possible words, returns -1 if not
            guessedWord = displayScript.FormWord().ToLower();
            int index = Array.IndexOf(allowedWords, guessedWord);
            int index1 = Array.IndexOf(possiblewords, guessedWord);
            if (index != -1 || index1 != -1)
            {
                //separating each word into arrays
                letterArray = guessedWord.ToCharArray();
                answerArray = answer.ToCharArray();
                //Two distinct dictionaries that implement the same keys but differing values to ensure that only the amount of letters that exist in the word show up in either yellow or green
                Dictionary<char, int> letterFrequency = new Dictionary<char, int>();
                Dictionary<char, int> letterCounter = new Dictionary<char, int>();
                foreach (char c in answer)
                {
                    //If key exists, then add it, if it already does, then add to the frequency
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
                //Check for exact copies first
                for (int h = 0; h < letterArray.Length; h++)
                {
                    if (letterArray[h] == answerArray[h] && letterCounter[letterArray[h]] != letterFrequency[letterArray[h]])
                    {
                        displayScript.ChangeToGreen(inputBoxes[GetRow(), h]);
                        letterCounter[letterArray[h]] += 1;
                    }
                }
                //Then check for correct letter and wrong positioning
                for (int h = 0; h < letterArray.Length; h++)
                {
                    if (answer.Contains(letterArray[h]) && letterCounter[letterArray[h]] != letterFrequency[letterArray[h]])
                    {
                        displayScript.ChangeToYellow(inputBoxes[GetRow(), h]);
                        letterCounter[letterArray[h]] += 1;
                    }
                }
                //Then give the rest to the Old dead gods
                for (int h = 0; h < letterArray.Length; h++)
                {
                    if (!answer.Contains(letterArray[h]))
                    {
                        displayScript.ChangeToGrey(inputBoxes[GetRow(), h]);
                    }
                }
                //If correct, send it to the finish screen, if not send it to the next row
                if (guessedWord == answer)
                {
                    displayScript.ChangeDisplay(true);
                }
                row++;
                //If after 5 guesses (I didn't know it was 6 until too late) they don't have it right, then send them to finish screen
                if (row == 5)
                {
                    displayScript.ChangeDisplay(false);
                }
                //Set filled back to false
                displayScript.SetFilledBox();
            }
            
        }
    }
    //Giving an individual value to each index of array to miror my grid of InputBoxes
    void Set2DArray()
    {
        for (int j = 0; j < 5; j++)
        {
            for (int p = 0; p < 5; p++)
            {
                inputBoxes[j, p] = num++;
            }
        }
    }
    public Button[] GetAlphabetBoxes()
    {
        return alphabetBoxes;
    }
    public string GetAnswer()
    {
        return answer;
    }
}