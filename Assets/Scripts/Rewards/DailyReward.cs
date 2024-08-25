using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    // Every Day user will get 5 coins when opening the app
    public Text dayText; // Day 1, 2, etc.
    public Button claimButton;
    public Text coinsMultiplierText; // x 20 coins
    public Text waitTimeText;

    private int rewardCoins = 2;
    private int rewardMultiplier = 1;
    private int currentDay = 1;
    private DateTime lastClaimTime;
    private TimeSpan claimCooldown = new TimeSpan(24, 0, 0); // 24 hours

    CoinsManager coinsManager;
    private void Start()
    {
        coinsManager = FindObjectOfType<CoinsManager>();

        var saveManager = SaveLoad.Instance;
        var claimTime = saveManager.GetClaimTimeKey();
        var day = saveManager.GetDayKey();
        // Load last claim time and current day from player prefs (persistent storage)
        if (saveManager.HasKey(claimTime))
        {
            lastClaimTime = DateTime.Parse(saveManager.LoadString(claimTime));
            currentDay = saveManager.LoadInt(claimTime, 1);
        }
        else
        {
            lastClaimTime = DateTime.Now;
            currentDay = 1;
        }

        claimButton.onClick.AddListener(ClaimDailyReward);
        UpdateUI();
        StartCoroutine(UpdateWaitTime());
    }

    private void UpdateUI()
    {
        dayText.text = $"Day {currentDay}";
        coinsMultiplierText.text = $"x {rewardMultiplier * rewardCoins} coins";
    }

    private void ClaimDailyReward()
    {
        DateTime now = DateTime.Now;
        if (now - lastClaimTime >= claimCooldown)
        {
            // Claim the reward
            int coins = rewardMultiplier * rewardCoins;
            coinsManager.AddCoins(coins);

            // Update the last claim time and increment the day
            lastClaimTime = now;
            currentDay++;
            rewardMultiplier++;

            // Save the current state
            var saveManager = SaveLoad.Instance;
            saveManager.SaveString(saveManager.GetClaimTimeKey(), lastClaimTime.ToString());
            saveManager.SaveInt(saveManager.GetDayKey(), currentDay);

            UpdateUI();
            StartCoroutine(UpdateWaitTime());
        }
    }

    private IEnumerator UpdateWaitTime()
    {
        while (true)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeUntilNextClaim = (lastClaimTime + claimCooldown) - now;

            if (timeUntilNextClaim <= TimeSpan.Zero)
            {
                claimButton.interactable = true;
            }
            else
            {
                waitTimeText.text = $"{timeUntilNextClaim.Hours:D2}:{timeUntilNextClaim.Minutes:D2}:{timeUntilNextClaim.Seconds:D2}";
                claimButton.interactable = false;
            }
            yield return new WaitForSeconds(1); // Update every second
        }
    }
}
