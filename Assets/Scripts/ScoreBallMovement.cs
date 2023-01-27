using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBallMovement : MonoBehaviour
{
    private Vector2 targetPosition;

    private SpriteRenderer spriteRenderer;

    public GameObject gameController;
    public Sprite chicken_flying, chicken_idle;

    public float positiveBoostToBall, speedPositiveBoost, positiveBoostDuration;
    private float duration;
    public bool flying = false;
    public bool onNegativeBoost = false;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (flying == true & duration > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speedPositiveBoost * Time.deltaTime);
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                flying = false;
                spriteRenderer.sprite = chicken_idle;
            }
        }

    }

    public void PositiveBoost()
    {
        Debug.Log("start");
        duration = positiveBoostDuration;
        flying = true;
        spriteRenderer.sprite = chicken_flying;
        onNegativeBoost = false;

        targetPosition = new Vector2(transform.position.x + positiveBoostToBall, transform.position.y);

        //Play audio chicken
        GetComponent<AudioSource>().Play();

    }



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
