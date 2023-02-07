using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Swipe swipe;
    public int arrowPos;

    private void Start()
    {
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Circle"))
        {
            Debug.Log("Triggered");

            //swipe only when it's inside the circle
            /*if (swipe.currentPos == arrowPos)
            {
                Debug.Log("Swiped Correct!");
            }*/
        }
    }
}
