using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public bool isClicked = false;
    public GameObject childUI;
    public Button childButton, parentButton;

    // 2 children
    public List<GameObject> childrenUI;
    public List<GameObject> childrenButton;
    private void Start()
    {
        childButton.onClick.AddListener(() =>
        {
            ClickStatus();
        });
        parentButton.onClick.AddListener(() =>
        {
            ClickStatus();
        });
    }
    private void Update()
    {
        childUI.SetActive(isClicked);
    }
    public void ClickStatus()
    {
        isClicked = !isClicked;
    }
}
