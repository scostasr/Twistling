using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BehaviourHold : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameObject gameController;
    private GameObject balanceBall;
    private GenControl genControl;
    private Color color_original;

    public float life;
    public float lifeDamageHit;
    public float positiveBoostToBallPosition;
    public float negativeBoostToBallPosition;
    public float timeDelay;

    public bool onKeyPressed = false;
    public bool canTakeDamage = false;
    public bool onWait = true;

    private KeyCode inputSelected;
    private int safetyNet;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.Find("GameController");
        balanceBall = GameObject.Find("BalanceBall");
        genControl = gameController.GetComponent<GenControl>();
    }

    void Start()
    {
        //Store original color
        color_original = spriteRenderer.color;

        //Select one input randomly, that it is not already in the list of used keys
        int randomNum = Random.Range(0, genControl.inputList.Count);
        inputSelected = genControl.inputList[randomNum];

        while (genControl.keysUsed.Contains(inputSelected) == true)
        {
            Debug.Log("Key already used");
            randomNum = Random.Range(0, genControl.inputList.Count);
            inputSelected = genControl.inputList[randomNum];
            safetyNet++;

            if (safetyNet >= 20)
            {
                Debug.Log("No key found");
                break;
            }

        }

        //Add the input selected to the list of used keys in GameController
        genControl.keysUsed.Add(inputSelected);

        //Display key code as text
        string inputName = inputSelected.ToString();
        string inputName_corrected = inputName.Replace("Alpha", "");
        transform.Find("InputText").GetComponent<TextMeshPro>().text = inputName_corrected;

        StartCoroutine(Countdown());
    }


        void Update()
    {

        if (Input.GetKey(inputSelected))
        {
            //Change colour to green when the key is pressed
            GetComponent<SpriteRenderer>().color = Color.green;

            onWait = true;

            //Give an extra positive boost to the balance ball position, to increase game feel and give more room to the player
            if (balanceBall != null)
                balanceBall.transform.position = new Vector2(balanceBall.transform.position.x + positiveBoostToBallPosition * Time.deltaTime, balanceBall.transform.position.y);
        }

        //Decrease score if left unpressed 2 secs after creating object
        if (onWait == false && balanceBall != null)
        {
            balanceBall.transform.position = new Vector2(balanceBall.transform.position.x - negativeBoostToBallPosition * Time.deltaTime, balanceBall.transform.position.y);

        }

        //Change colour back to red when the key is unpressed
        if (Input.GetKeyUp(inputSelected))
        {
            spriteRenderer.color = color_original;
            onWait = false;
        }

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(timeDelay);
        onWait = false;
    }

}
