using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeUI : MonoBehaviour
{
    public Text challengeNameText;
    public Text rewardAmountText;
    public Text targetText;
    public Button claimButton;
    public Slider progressSlider;

    public Challenge challenge;

    ChallengeManager challengeManager;
    private void Start()
    {
        challengeManager = FindObjectOfType<ChallengeManager>();

        claimButton.onClick.AddListener(ClaimChallenge);

        challengeNameText.text = challenge.challengeName;
        progressSlider.minValue = 0;
        progressSlider.maxValue = challenge.target;
    }
    private void Update()
    {
        progressSlider.value = challengeManager.GetCurrentChallengeProgress(challenge.challengeType);
        targetText.text = progressSlider.value + "/" + progressSlider.maxValue;
        claimButton.interactable = progressSlider.value >= challenge.target;
    }
    void ClaimChallenge()
    {
        // Reward UI
        challengeManager.CompleteChallenge(challenge);

        // Call the Challenge Generation -> To Generate a new Challenge

    }
}
