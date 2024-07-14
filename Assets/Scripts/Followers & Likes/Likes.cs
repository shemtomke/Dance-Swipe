using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Likes : MonoBehaviour
{
    public Text likesText;

    SocialMetricsManager metricsManager;
    private void Start()
    {
        metricsManager = FindObjectOfType<SocialMetricsManager>();
    }
    private void Update()
    {
        likesText.text = metricsManager.GetCurrentLikes().ToString();
    }
}
