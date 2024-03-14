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
        StartCoroutine(InstantiateArrows());
    }
    private void Update()
    {
        currentArrow = Random.Range(0, arrows.Length);
    }
    //Instantiate arrows
    IEnumerator InstantiateArrows()
    {
        var myNewArrow =Instantiate(arrows[currentArrow], transform.position, Quaternion.identity);
        myNewArrow.transform.position = new Vector3(3.9f, -3.9f, transform.position.z);
        myNewArrow.transform.parent = gameObject.transform;

        yield return new WaitForSeconds(2f);

        StartCoroutine(InstantiateArrows());
    }
}
