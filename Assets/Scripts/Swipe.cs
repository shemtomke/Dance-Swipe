using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public Transform entryPoint, exitPoint;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

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
    }
    private void Update()
    {
        AndroidSwipe();
        StandaloneSwipe();
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
                    Debug.Log("up swipe");
                    
                }
                //swipe down
                if ((currentSwipe.y < 0) && (currentSwipe.x > -0.5f) && (currentSwipe.x < 0.5f))
                {
                    Debug.Log("down swipe");

                }
                //swipe left
                if ((currentSwipe.x < 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
                {
                    Debug.Log("left swipe");

                }
                //swipe right
                if ((currentSwipe.x > 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
                {
                    Debug.Log("right swipe");

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
                Debug.Log("up swipe");
                currentPos = 0;
            }
            //swipe down
            if ((currentSwipe.y < 0) && (currentSwipe.x > -0.5f) && (currentSwipe.x < 0.5f))
            {
                Debug.Log("down swipe");
                currentPos = 1;
            }
            //swipe left
            if ((currentSwipe.x < 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
            {
                Debug.Log("left swipe");
                currentPos = 2;
            }
            //swipe right
            if ((currentSwipe.x > 0) && (currentSwipe.y > -0.5f) && (currentSwipe.y < 0.5f))
            {
                Debug.Log("right swipe");
                currentPos = 3;
            }
        }
    }
}
