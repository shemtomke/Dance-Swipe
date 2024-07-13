using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelDrawer : MonoBehaviour
{
    public Button UIButton;
    public GameObject UIPanel;
    public bool isClicked = false;

    private void Start()
    {
        UIButton.onClick.AddListener(() =>
        {
            isClicked = !isClicked;
        });
    }
    private void Update()
    {
        UIPanel.SetActive(isClicked);
    }
}
