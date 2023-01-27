using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public List<GameObject> buttonsTutorial = new List<GameObject>();

    public GameObject gameController;

    public int buttonColorChanged;

    void Start()
    {
        buttonColorChanged = -1;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //For the Pause menu
            if (SceneManager.GetActiveScene().name == "Main")
            {
                GetComponent<Image>().color = Color.green;
                SceneManager.LoadScene(1);
            } 

            //For the Start Menu
            else
            {
                buttonColorChanged += 1;
                GetComponent<Image>().color = Color.green;
                buttonsTutorial[buttonColorChanged].GetComponent<Image>().color = Color.green;

                //Play SFX
                GetComponent<AudioSource>().Play();
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            GetComponent<Image>().color = Color.white;
        }

        if (buttonColorChanged > 5)
            gameController.GetComponent<MainMenu>().PlayGame();

    }

}
