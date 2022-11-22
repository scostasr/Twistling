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
    public List<KeyCode> keysUsed = new List<KeyCode>();

    void Start()
    {
        
    }


    void Update()
    {
        //Restart game when press R
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(1);


        //Game win and gameover conditions      

    }
}
