using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        Debug.Log("Game is starting");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }

}
