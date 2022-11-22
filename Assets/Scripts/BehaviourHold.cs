using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BehaviourHold : MonoBehaviour
{
    private List<KeyCode> inputList = new List<KeyCode>();
    private KeyCode inputSelected;
    private GameObject gameController;
    private GameObject balanceBall;
    private Vector3 randomPos;
    private Vector2 startingPos;
    private int safetyNet;
    public float positiveBoostToBallPosition;
    public float negativeBoostToBallPosition;
    public bool onWait = true;
    public float amountShake;

    void Start()
    {
        gameController = GameObject.Find("GameController");
        balanceBall = GameObject.Find("BalanceBall");
        startingPos = transform.position;

        //Countdown to decrease score if button left unpressed
        StartCoroutine(Countdown());

        
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
        inputList.Add(KeyCode.CapsLock);





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
                balanceBall.transform.position = new Vector2(balanceBall.transform.position.x + positiveBoostToBallPosition, balanceBall.transform.position.y);
        }

        //Decrease score if left unpressed 2 secs after creating object
        if (onWait == false && balanceBall != null)
        {
            balanceBall.transform.position = new Vector2(balanceBall.transform.position.x - negativeBoostToBallPosition, balanceBall.transform.position.y);

        }

        //Change colour back to red when the key is unpressed
        if (Input.GetKeyUp(inputSelected))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            onWait=false;
        }

    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(2);
        onWait = false;
    }

}
