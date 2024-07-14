using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    // Every Day user will get 5 coins when opening the app
    public Text dayText; //Day 1, 2
    public Button claimButton;
    public Text coinsMultiplierText; // x 20 coins 
    public Text waitTimeText;

    int rewardCoins;
    int rewardMultiplier;
    private void Start()
    {
        claimButton.onClick.AddListener(() =>
        {
            ClaimDailyReward();
        });
    }
    public void ClaimDailyReward()
    {

    }
    // Track Remaining Time -> 24 hours
    // Update Day from Day 1 to endless

}
