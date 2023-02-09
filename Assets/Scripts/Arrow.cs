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

        //WrongSwipe();

        EntersCircle();
        InsideCircle();
        OutsideCircle();
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

    void EntersCircle()
    {
        if((transform.position.x <= 1 && transform.position.x > 0.22) && !isSwiped)
        {
            //good
            if (swipe.currentPos == arrowPos)
            {
                swipe.ExpressionMessage("GOOD TRIAL", "YOU CAN DO BETTER!", 1);
                isSwiped = true;
            }
            else
            {
                swipe.ExpressionMessage("WELL!", "YOU ARE A JOKER!", 0);
                isSwiped = true;
            }
        }

        //Swiped before the arrow reaching the circle pos
        if(transform.position.x > 1 && !isSwiped)
        {
            //BAD
            if (swipe.currentPos == arrowPos)
            {
                swipe.ExpressionMessage("POOR", "ARE YOU KIDDING ME!", 0);
                isSwiped = true;
            }
        }
    }
    void InsideCircle()
    {
        if(transform.position.x < 0.22 && transform.position.x > -0.22 && !isSwiped)
        {
            //Excellent
            if (swipe.currentPos == arrowPos)
            {
                swipe.ExpressionMessage("EXCELLENT!", "THAT'S IT!", 10);
                isSwiped = true;
            }
            else
            {
                swipe.ExpressionMessage("TRY AGAIN!", "YOU GOTTA BE KIDDING ME", 0);
                isSwiped = true;
            }
        }
    }
    void OutsideCircle()
    {
        if ((transform.position.x < -1 || (transform.position.x >= -1 && transform.position.x < -0.22)) && !isSwiped)
        {
            //Terrible
            if (swipe.currentPos == arrowPos)
            {
                swipe.ExpressionMessage("POOR", "ARE YOU KIDDING ME!", 0);
                isSwiped = true;
            }
        }
    }
    void WrongSwipe()
    {
        //Wrong Swipe
        if (swipe.currentPos != arrowPos)
        {
            swipe.ExpressionMessage("POOR", "ARE YOU KIDDING ME!", 0);
            isSwiped = true;
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Circle") && !isSwiped)
        {
            //swipe only when it's inside the circle
            if (swipe.currentPos == arrowPos)
            {
                swipe.ExpressionMessage("GOOD TRIAL", "YOU CAN DO BETTER!", 1);
                isSwiped = true;
                transform.position = new Vector3(transform.position.x + 0.02f, transform.position.y + 0.02f, transform.position.z + 0.02f);
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
                swipe.ExpressionMessage("EXCELLENT!", "THAT'S IT!", 10);
                isSwiped = true;
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
                swipe.ExpressionMessage("POOR", "ARE YOU KIDDING ME!", 0);
                isSwiped = true;
            }
        }
    }*/
    /*On entering the circle --> excelent
     * if you do a wrong direction then wrong points/text motivation regardless of where you do it
     * if you do it correct but on enter of the circle then --> To early! (Good)
     * if you do it exiting the circle --> To late! (Poor)
     * You only get a chance to swipe once 
     * when swiping an arrow then it should glow
     */
    /*
     * x - 0.8 -> entry to the circle
     * x - 0 -> inside the circle
     * x - -0.8 -> outside the circle
     */

}
