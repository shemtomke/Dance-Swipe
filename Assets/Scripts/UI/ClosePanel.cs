using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
    public GameObject parentPanel;
    Button closeButton;

    private void Start()
    {
        closeButton = GetComponent<Button>();

        closeButton.onClick.AddListener(() =>
        {
            parentPanel.SetActive(false);
        });
    }
}
