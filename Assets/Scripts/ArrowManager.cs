using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public Swipe swipe;

    public GameObject[] arrows;

    int currentArrow;

    private void Start()
    {
        RandomArrows();
        
    }
    private void Update()
    {
        MoveArrows();
    }
    void RandomArrows()
    {
        currentArrow = Random.Range(0, arrows.Length);
    }
    void MoveArrows()
    {
        //move arrows from left to right
        arrows[currentArrow].transform.Translate(-0.1f, 0, 0);
    }
    public void DestroyArrow(GameObject arrowObject)
    {
        //destroy arrow when you swipe with its direction
        if (swipe.currentPos == arrows[currentArrow])
        {
            Destroy(arrowObject);
        }
    }
}
