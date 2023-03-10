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
        Debug.Log(isSwiped);
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
            /*else
            {
                swipe.ExpressionMessage("WELL!", "YOU ARE A JOKER!", 0);
                isSwiped = true;
            }*/
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
            /*else
            {
                swipe.ExpressionMessage("TRY AGAIN!", "YOU GOTTA BE KIDDING ME", 0);
                isSwiped = true;
            }*/
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
}
