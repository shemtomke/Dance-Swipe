using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Followers : MonoBehaviour
{
    public Text followersText;

    SocialMetricsManager metricsManager;
    private void Start()
    {
        metricsManager = FindObjectOfType<SocialMetricsManager>();
    }
    private void Update()
    {
        // Show New Followers
        followersText.text = metricsManager.GetCurrentFollowers().ToString();
    }
}
