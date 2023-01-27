using System.Collections;
using UnityEngine;
using TMPro;

public class BehaviourPress : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameObject     gameController;
    private GameObject     balanceBall;
    private GenControl     genControl;
    public GameObject conveyor;
    private Color          color_original;

    public float    life;
    public float    lifeDamageHit;
    public float    positiveBoostToBall;
    public float    negativeBoostToBallPosition;
    public float    positiveBoostDuration;
    public float    timeDelay;
    public float    duration;
    public float    speedPositiveBoost;

    public bool     onKeyPressed  = false;
    public bool     canTakeDamage = false;
    public bool IamDead = false;

    public Vector2  targetPosition;

    private KeyCode inputSelected;
    private int     safetyNet;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.Find("GameController");
        balanceBall    = GameObject.Find("BalanceBall");
        conveyor = GameObject.Find("conveyor");
        genControl     = gameController.GetComponent<GenControl>();
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

            if (safetyNet >= 50)
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
            spriteRenderer.color = Color.green;

            //Play SFX
            GetComponent<AudioSource>().Play();

        }

        if (Input.GetKeyUp(inputSelected))
        {
            spriteRenderer.color = color_original;
            onKeyPressed = false;  
        }

        //Decrease score if left unpressed for longer than 2 secs after creating the object
        if (canTakeDamage == true & balanceBall != null)
        {
            if (balanceBall.GetComponent<ScoreBallMovement>().flying == false)
            {
                balanceBall.GetComponent<ScoreBallMovement>().onNegativeBoost = true;
                balanceBall.transform.position = new Vector2(balanceBall.transform.position.x - negativeBoostToBallPosition * Time.deltaTime, balanceBall.transform.position.y);
                conveyor.GetComponent<Animator>().enabled = true;
                conveyor.GetComponent<Animator>().Play("conveyor");
            }
        }


        //Destroy object if life < 0
        if (life <= 0)
        {
            //++ to blocksDestroyed variable, -- to blocksOnScreen variable
            genControl.blocksDestroyed++;
            genControl.blocksOnScreen--;

            //Eliminate the key code used from the list on GameController
            genControl.keysUsed.Remove(inputSelected);

            //Give an extra positive boost to the balance ball position, to increase game feel and give more room to the player
            if (balanceBall != null)
            {
                balanceBall.GetComponent<ScoreBallMovement>().PositiveBoost();
                Destroy(transform.parent.gameObject);
                Destroy(gameObject);
            }

        }
    }

    IEnumerator CountdownForDamage()
    {
        yield return new WaitForSeconds(timeDelay);
        canTakeDamage = true;
    }

}
