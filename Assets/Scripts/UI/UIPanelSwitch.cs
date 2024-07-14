using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSwitch : MonoBehaviour
{
    // SideBySide From SamePanel
    public bool samePanel;

    [Serializable]
    public class PanelProperties
    {
        public GameObject panel;
        public Button panelButton;
        public bool isClicked = false;
    }

    [NonReorderable]
    public List<PanelProperties> panelProperties;

    private void Start()
    {
        // Ensure only the first panel is active at the start -> Only For Same Panel
        if(samePanel)
        {
            for (int i = 0; i < panelProperties.Count; i++)
            {
                if (i == 0)
                {
                    panelProperties[i].panel.SetActive(true);
                    panelProperties[i].panelButton.interactable = false; // Disable the button for the active panel
                }
                else
                {
                    panelProperties[i].panel.SetActive(false);
                    panelProperties[i].panelButton.interactable = true;
                }
            }
        }

        // Add click listeners to buttons
        foreach (var panelProperty in panelProperties)
        {
            panelProperty.panelButton.onClick.AddListener(() => OnPanelButtonClicked(panelProperty));
        }
    }

    private void OnPanelButtonClicked(PanelProperties clickedPanel)
    {
        if(samePanel)
        {
            // Deactivate all panels and enable all buttons
            foreach (var panelProperty in panelProperties)
            {
                panelProperty.panel.SetActive(false);
                panelProperty.panelButton.interactable = true;
            }

            // Activate the clicked panel and disable its button
            clickedPanel.panel.SetActive(true);
            clickedPanel.panelButton.interactable = false;
        }
        else
        {
            // Deactivate all panels
            foreach (var panelProperty in panelProperties)
            {
                panelProperty.panel.SetActive(false);
            }

            // Activate the clicked panel and disable its button
            clickedPanel.panel.SetActive(true);
        }
    }
}
