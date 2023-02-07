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
        if(swipe.currentPos == arrowPos)
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed");
        }
    }
}
