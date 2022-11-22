using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBallMovement : MonoBehaviour
{

    public GameObject gameController;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GameOverTrigger")
        {
            Debug.Log("Game Over");
            gameController.GetComponent<GenControl>().isGameOver = true;

            Destroy(gameObject);
            
        }

        else if (collision.gameObject.name == "WinTrigger")
        {
            Debug.Log("You win");
            gameController.GetComponent<GenControl>().isWin = true;

            Destroy(gameObject);
            
        }
    }

}
