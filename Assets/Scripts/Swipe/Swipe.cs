using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public ArrowState arrowState;
    public Circle circle;
    private void Start()
    {
        
    }
    private void Update()
    {
        AndroidSwipe();
        ArrowsStandalone();
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
                    arrowState = ArrowState.Up;
                }
                //swipe down
                if ((currentSwipe.y < 0) && (currentSwipe.x > -0.5f) && (currentSwipe.x < 0.5f))
                {
                    arrowState = ArrowState.Down;
                }
                //swipe left
                if ((currentSwipe.x < 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
                {
                    arrowState = ArrowState.Left;
                }
                //swipe right
                if ((currentSwipe.x > 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
                {
                    arrowState = ArrowState.Right;
                }

                circle.StartAnim();
            }
        }
    }
    void ArrowsStandalone()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            arrowState = ArrowState.Up;
            circle.StartAnim();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrowState = ArrowState.Down;
            circle.StartAnim();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            arrowState = ArrowState.Left;
            circle.StartAnim();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            arrowState = ArrowState.Right;
            circle.StartAnim();
        }
    }
}
