using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class BehaviourMouse : MonoBehaviour
{
    private GameObject gameController;
    private GameObject balanceBall;
    private Vector3 randomPos;
    private int safetyNet;
    public bool onWait = true;
    public Sprite mouseWhite, mouseGreen, mouseRed;
    public float positiveBoostToBallPosition;
    public float negativeBoostToBallPosition;
    public float timeDelay;

    void Start()
    {
        gameController = GameObject.Find("GameController");
        balanceBall = GameObject.Find("BalanceBall");
        StartCoroutine(Countdown());

    }


    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Change colour to green when the key is pressed
            GetComponent<SpriteRenderer>().sprite = mouseGreen;

            //Add constant points to score while pressing
            balanceBall.transform.position = new Vector2(balanceBall.transform.position.x + positiveBoostToBallPosition, balanceBall.transform.position.y);
        }

        //Decrease score if unpressed after X secs after creating object
        else if (onWait == false && balanceBall != null)
        {
            balanceBall.transform.position = new Vector2(balanceBall.transform.position.x - negativeBoostToBallPosition * 0.001f, balanceBall.transform.position.y);

            //Change colour back to red when the key is unpressed
            GetComponent<SpriteRenderer>().sprite = mouseRed;

        }

            
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(timeDelay);
        onWait = false;
    }

}
