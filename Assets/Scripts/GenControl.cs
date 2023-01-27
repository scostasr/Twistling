using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenControl : MonoBehaviour
{

    public int blocksDestroyed;
    public int blocksOnScreen;
    public int blocksHoldOnScreen;
    public bool isGameOver = false;
    public bool isWin = false;
    public List<KeyCode> keysUsed = new List<KeyCode>();
    public readonly List<KeyCode> inputList = new List<KeyCode>();
    public bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject buttonRestart;

    private void Awake()
    {
        BuildInputList();
    }

    void Update()
    {

        //Game win and gameover conditions      
        if (isGameOver == true)
        {
            SceneManager.LoadScene(2);
        }

        if (isWin == true)
        {
            SceneManager.LoadScene(3);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == false)
            {
                PauseGame();
            }
        }

        if (gameIsPaused == true && Input.GetKeyDown(KeyCode.Return))
        {
            //buttonRestart.GetComponent<Image>().color = Color.green;
            ResumeGame();
        }

    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuiteGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }


    private void BuildInputList()
    {
        inputList.Add(KeyCode.A);
        inputList.Add(KeyCode.B);
        inputList.Add(KeyCode.C);
        inputList.Add(KeyCode.D);
        inputList.Add(KeyCode.E);
        inputList.Add(KeyCode.F);
        inputList.Add(KeyCode.G);
        inputList.Add(KeyCode.H);
        inputList.Add(KeyCode.I);
        inputList.Add(KeyCode.J);
        inputList.Add(KeyCode.K);
        inputList.Add(KeyCode.L);
        inputList.Add(KeyCode.M);
        inputList.Add(KeyCode.N);
        inputList.Add(KeyCode.O);
        inputList.Add(KeyCode.P);
        inputList.Add(KeyCode.Q);
        inputList.Add(KeyCode.S);
        inputList.Add(KeyCode.T);
        inputList.Add(KeyCode.U);
        inputList.Add(KeyCode.V);
        inputList.Add(KeyCode.W);
        inputList.Add(KeyCode.X);
        inputList.Add(KeyCode.Y);
        inputList.Add(KeyCode.Z);
        inputList.Add(KeyCode.Space);
        inputList.Add(KeyCode.Return);
        inputList.Add(KeyCode.UpArrow);
        inputList.Add(KeyCode.DownArrow);
        inputList.Add(KeyCode.RightArrow);
        inputList.Add(KeyCode.LeftArrow);
        inputList.Add(KeyCode.Tab);
        inputList.Add(KeyCode.Alpha1);
        inputList.Add(KeyCode.Alpha2);
        inputList.Add(KeyCode.Alpha3);
        inputList.Add(KeyCode.Alpha4);
        inputList.Add(KeyCode.Alpha5);
        inputList.Add(KeyCode.Alpha6);
        inputList.Add(KeyCode.Alpha7);
        inputList.Add(KeyCode.Alpha8);
        inputList.Add(KeyCode.Alpha9);
        inputList.Add(KeyCode.Alpha0);
    }
}
