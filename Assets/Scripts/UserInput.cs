using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInput : MonoBehaviour
{
    //Restart the scene (There is a more efficient way of doing this, I just don't know how without making it unnecessarily complex)
    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
