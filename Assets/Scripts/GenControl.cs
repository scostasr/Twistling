using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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



    void Update()
    {
        //Restart game when press R
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(1);


        //Game win and gameover conditions      
        if (isGameOver == true)
        {
            SceneManager.LoadScene(2);
        }

        if (isWin == true)
        {
            SceneManager.LoadScene(3);
        }
    }
}
