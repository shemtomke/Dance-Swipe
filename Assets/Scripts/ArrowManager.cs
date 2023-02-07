using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public Swipe swipe;

    public GameObject[] arrows;
    GameObject targetArrow;

    int currentArrow;

    private void Start()
    {
        swipe = FindObjectOfType<Swipe>();
        
    }
    private void Update()
    {
        currentArrow = Random.Range(0, arrows.Length);
    }
    public void DestroyArrow(GameObject arrowObject)
    {
        //destroy arrow when you swipe with its direction
        if (swipe.currentPos == arrows[currentArrow].GetInstanceID())
        {
            //Destroy(arrowObject);
            Debug.Log("Destroy");
        }
    }
    //Instantiate arrows
    IEnumerator InstantiateArrows()
    {
        Instantiate(arrows[currentArrow], transform.parent.position, Quaternion.identity);

        yield return new WaitForSeconds(1);


    }
}
