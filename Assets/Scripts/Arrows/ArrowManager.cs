using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public static ArrowManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Swipe swipe;
    public GameObject[] arrows;

    int currentArrow;
    public int arrowSpeed;

    private void Start()
    {
        swipe = FindObjectOfType<Swipe>();
        StartCoroutine(InstantiateArrows());
    }
    private void Update()
    {
        currentArrow = Random.Range(0, arrows.Length);
    }
    // Instantiate arrows
    IEnumerator InstantiateArrows()
    {
        while (true)
        {
            if (!AudioManager.Instance.selectedMusic.isPlaying)
            {
                Debug.Log("Game Over"); // Add a debug message for game over
                yield break; // Exit the coroutine
            }

            currentArrow = Random.Range(0, arrows.Length);
            var myNewArrow = Instantiate(arrows[currentArrow], transform.position, Quaternion.identity);
            myNewArrow.transform.position = new Vector3(3.9f, -3.5f, transform.position.z);
            myNewArrow.transform.parent = gameObject.transform;

            // Have an incremented/random timer interval
            yield return new WaitForSeconds(3f);
        }
    }
}
