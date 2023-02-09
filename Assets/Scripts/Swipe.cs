using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    public Text expressionTxt1, expressionTxt2, score;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public int currentScore;
    public int currentPos; //should match with the arrow array...
    /// <summary>
    /// 0 - up
    /// 1 - down
    /// 2 - left
    /// 3 - right
    /// </summary>
    private void Start()
    {
        currentPos = -1;
        expressionTxt1.text = "";
        expressionTxt2.text = "";
        score.text = "SCORE : " + currentScore;
    }
    private void Update()
    {
        AndroidSwipe();
        StandaloneSwipe();
        ArrowsStandalone();
    }
    public void ExpressionMessage(string firstMessage, string secondMessage, int pointScore)
    {
        expressionTxt1.text = "" + firstMessage;
        expressionTxt2.text = "" + secondMessage;
        currentScore += pointScore;
        score.text = "SCORE : " + currentScore;
    }
    void AndroidSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if((currentSwipe.y > 0) && (currentSwipe.x > -0.5f) && (currentSwipe.x < 0.5f))
                {
                    currentPos = 0;
                }
                //swipe down
                if ((currentSwipe.y < 0) && (currentSwipe.x > -0.5f) && (currentSwipe.x < 0.5f))
                {
                    currentPos = 1;
                }
                //swipe left
                if ((currentSwipe.x < 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
                {
                    currentPos = 2;
                }
                //swipe right
                if ((currentSwipe.x > 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
                {
                    currentPos = 3;
                }
            }
        }
    }

    void StandaloneSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if ((currentSwipe.y > 0) && (currentSwipe.x > -0.5f) && (currentSwipe.x < 0.5f))
            {
                currentPos = 0;
            }
            //swipe down
            if ((currentSwipe.y < 0) && (currentSwipe.x > -0.5f) && (currentSwipe.x < 0.5f))
            {
                currentPos = 1;
            }
            //swipe left
            if ((currentSwipe.x < 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
            {
                currentPos = 2;
            }
            //swipe right
            if ((currentSwipe.x > 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
            {
                currentPos = 3;
            }
        }
    }
    void ArrowsStandalone()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentPos = 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentPos = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentPos = 2;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentPos = 3;
        }
    }
}
