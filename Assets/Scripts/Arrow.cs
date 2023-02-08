using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Swipe swipe;
    public int arrowPos;
    bool isSwiped;

    private void Start()
    {
        isSwiped = false;
        swipe = FindObjectOfType<Swipe>();
    }
    private void Update()
    {
        MoveArrow();
        DestroyArrow();
    }
    void MoveArrow()
    {
        //move arrow from left to right
        transform.Translate(-0.01f, 0, 0);
    }
    void DestroyArrow()
    {
        if(transform.position.x < -5) //check screen size -> x axis
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Circle") && !isSwiped)
        {
            //swipe only when it's inside the circle
            if (swipe.currentPos == arrowPos)
            {
                swipe.ExpressionMessage("EXCELLENT!", "THAT'S IT!", 10);
                isSwiped = true;
            }
            else
            {
                swipe.ExpressionMessage("TERRIBLE", "TRY AGAIN!", -3);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Circle") && !isSwiped)
        {
            //swipe only when it's inside the circle
            if (swipe.currentPos == arrowPos)
            {
                swipe.ExpressionMessage("GOOD", "THAT'S IT!", 7);
                isSwiped = true;
            }
            else
            {
                swipe.ExpressionMessage("BAD", "YOU CAN DO BETTER!", -5);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Circle") && !isSwiped)
        {
            //swipe only when it's inside the circle
            if (swipe.currentPos == arrowPos)
            {
                swipe.ExpressionMessage("GOOD", "THAT'S FAIR!", 5);
                isSwiped = true;
            }
            else
            {
                swipe.ExpressionMessage("POOR", "ARE YOU KIDDING ME!", -8);
            }
        }
    }
}
