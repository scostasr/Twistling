using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class BehaviourPress : MonoBehaviour
{

    public float life;
    public float lifeDamageHit;
    private List<KeyCode> inputList = new List<KeyCode>();
    private KeyCode inputSelected;
    private GameObject gameController;
    private GameObject balanceBall;
    public float positiveBoostToBallPosition;
    public float negativeBoostToBallPosition;
    public float timeDelay;
    private int safetyNet;
    private Vector3 randomPos;
    public bool onKeyPressed = false;
    public bool canTakeDamage = false;


    void Start()
    {
        gameController = GameObject.Find("GameController");
        balanceBall = GameObject.Find("BalanceBall");

        //Add ++ to variable blocksOnScreen from GenControl component
        gameController.GetComponent<GenControl>().blocksOnScreen++;
        

        #region Input selection
        //List of all inputs
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


        //Select one input randomly, that it is not already in the list of used keys
        int randomNum = Random.Range(0, inputList.Count);
        inputSelected = inputList[randomNum];

        while (gameController.GetComponent<GenControl>().keysUsed.Contains(inputSelected) == true)
        {
            Debug.Log("Key already used");
            randomNum = Random.Range(0, inputList.Count);
            inputSelected = inputList[randomNum];
            safetyNet++;

            if (safetyNet >= 20)
            {
                Debug.Log("No key found");
                break;
            }

        }

        //Add the input selected to the list of used keys in GameController
        gameController.GetComponent<GenControl>().keysUsed.Add(inputSelected);

        //Display key code as text
        string inputName = inputSelected.ToString();
        string inputName_corrected = inputName.Replace("Alpha", "");
        transform.Find("InputText").GetComponent<TextMeshPro>().text = inputName_corrected;

        #endregion

        StartCoroutine(CountdownForDamage());

    }


    void Update()
    {

        //Detect damage when key pressed, and stop decreasing balanceball movement
        if (Input.GetKeyDown(inputSelected))
        {
            onKeyPressed = true;
            life -= lifeDamageHit;

            //Change sprite color when button is pressed
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        }

        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            onKeyPressed = false;  
        }

        //Decrease score if left unpressed for longer than 2 secs after creating the object
        if (canTakeDamage == true && balanceBall != null)
        {
            balanceBall.transform.position = new Vector2(balanceBall.transform.position.x - negativeBoostToBallPosition * 0.001f, balanceBall.transform.position.y);
        }


        //Destroy object if life < 0
        if (life <= 0)
        {

            //Give an extra positive boost to the balance ball position, to increase game feel and give more room to the player
            if (balanceBall != null)
                balanceBall.transform.position = new Vector2(balanceBall.transform.position.x + positiveBoostToBallPosition, balanceBall.transform.position.y);

            //++ to blocksDestroyed variable, -- to blocksOnScreen variable
            gameController.GetComponent<GenControl>().blocksDestroyed++;
            gameController.GetComponent<GenControl>().blocksOnScreen--;

            //Eliminate the key code used from the list on GameController
            gameController.GetComponent<GenControl>().keysUsed.Remove(inputSelected);

            Destroy(gameObject);
        }
    }

    IEnumerator CountdownForDamage()
    {
        yield return new WaitForSeconds(timeDelay);
        canTakeDamage = true;
    }



}
