using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBallMovement : MonoBehaviour
{

    public GameObject gameController;

    void Start()
    {
        
    }


    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "TopLeft")
        {
            Debug.Log("Game Over");
            //Destroy(gameObject);
        }

        else if (collision.gameObject.name == "TopRight")
        {
            Debug.Log("You win");
            Destroy(gameObject);
        }
    }

}
