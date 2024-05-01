using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public ArrowState arrowState;
    bool isSwiped;

    Swipe swipe;
    ExpressionManager expressionManager;
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        swipe = FindObjectOfType<Swipe>();
        expressionManager = FindObjectOfType<ExpressionManager>();

        isSwiped = false;
    }
    private void Update()
    {
        MoveArrow();
        ArrowSwipe();

        DestroyArrow();
    }
    void MoveArrow()
    {
        // Move the object from right to left
        transform.Translate(Vector3.left * ArrowManager.Instance.arrowSpeed * Time.deltaTime);
    }
    void DestroyArrow()
    {
        if(transform.position.x < -5) //check screen size -> x axis
        {
            Destroy(this.gameObject);
        }
    }
    void ArrowSwipe()
    {
        BeforeEnteringCircle();
        //CircleBorder();
        InsideCircle();
        ExitingCircle();
    }
    void BeforeEnteringCircle()
    {
        //Swiped before the arrow reaching the circle pos
        if (transform.position.x >= 1 && !isSwiped)
        {
            //BAD
            if (swipe.arrowState == arrowState)
            {
                expressionManager.ExpressionMessage("POOR", "ARE YOU KIDDING ME!", 0);
                isSwiped = true;
            }

            swipe.arrowState = ArrowState.None;
        }
    }
    void CircleBorder()
    {
        if (((transform.position.x >= -1 && transform.position.x <= -0.22) || (transform.position.x >= 0.22 && transform.position.x <= 1)) && !isSwiped)
        {
            //good
            if (swipe.arrowState == arrowState)
            {
                expressionManager.ExpressionMessage("GOOD TRIAL", "YOU CAN DO BETTER!", 1);
                isSwiped = true;
                player.TriggerAnimation(arrowState.ToString().ToLower());
            }
            else
            {
                expressionManager.ExpressionMessage("WELL!", "YOU ARE A JOKER!", 0);
                isSwiped = true;
            }

            swipe.arrowState = ArrowState.None;
        }
    }
    void InsideCircle()
    {
        if(transform.position.x <= 0.22 && transform.position.x >= -0.22 && !isSwiped)
        {
            //Excellent
            if (swipe.arrowState == arrowState)
            {
                expressionManager.ExpressionMessage("EXCELLENT!", "THAT'S IT!", 10);
                isSwiped = true;
                player.TriggerAnimation(arrowState.ToString().ToLower());
            }
            else
            {
                expressionManager.ExpressionMessage("TRY AGAIN!", "YOU GOTTA BE KIDDING ME", 0);
                isSwiped = true;
            }

            swipe.arrowState = ArrowState.None;
        }
    }
    void ExitingCircle()
    {
        if (transform.position.x < -1 && !isSwiped)
        {
            expressionManager.ExpressionMessage("POOR", "ARE YOU KIDDING ME!", 0);
            isSwiped = true;
            swipe.arrowState = ArrowState.None;
        }
    }
}
