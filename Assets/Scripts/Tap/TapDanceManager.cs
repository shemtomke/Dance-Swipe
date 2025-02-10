using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static TapDanceManager;

public class TapDanceManager : MonoBehaviour
{
    [Serializable]
    public class TapPositions
    {
        public Transform tapTransform;
        public bool isUsed = false;
    }

    [Serializable]
    public class TapButton
    {
        public GameObject tapButtonPrefab;

    }

    public List<TapButton> tapButtons;
    public List<TapPositions> tapPositions = new List<TapPositions>();
    private Dictionary<Button, TapPositions> buttonToTapPositionMap = new Dictionary<Button, TapPositions>();
    public Button tapButtonPrefab;
    public float waitTapButton;
    public float decreaseTimerInterval;
    public float minWaitTime;
    int count = 0;

    Player player;
    ExpressionManager expressionManager;
    SocialMetricsManager socialMetricsManager;
    AnimationManager animationManager;
    GameManager gameManager;

    [Header("Combo Properties")]
    public float comboTimer = 0f;
    public float comboTimeFrame = 1f; // 1 second for the combo
    public int comboCount = 0;
    private bool isComboActive = false; // Flag to track combo state
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        gameManager = FindFirstObjectByType<GameManager>();
        animationManager = FindFirstObjectByType<AnimationManager>();
        expressionManager = FindFirstObjectByType<ExpressionManager>();
        socialMetricsManager = FindFirstObjectByType<SocialMetricsManager>();

        GenerateTapButtons();
    }
    private void Update()
    {
        UpdateComboTimer();
    }

    public void GenerateTapButtons()
    {
        StartCoroutine(StartGeneratingTapButtons());
    }

    // Generate Tap Buttons for tapping to play
    IEnumerator StartGeneratingTapButtons()
    {
        while (true)
        {
            // Wait until the game has started
            while (!gameManager.isStartGame)
            {
                yield return null;
            }

            Button newButton = null;
            TapPositions selectedPosition = null;

            // Keep trying to find an available position until one is found
            while (selectedPosition == null)
            {
                newButton = Instantiate(tapButtonPrefab);

                // Random Tap Buttons

                newButton.gameObject.name = "Tap Dance " + count;

                selectedPosition = CheckAvailablePosition();

                if (selectedPosition == null)
                {
                    // If no position is available, destroy the button and try again
                    Destroy(newButton.gameObject);
                    yield return null; // Wait for next frame before trying again
                }
            }

            // Update the button's position
            newButton.transform.SetParent(selectedPosition.tapTransform, false);

            // Map the button to the selected tap position
            buttonToTapPositionMap[newButton] = selectedPosition;

            // Adjust the wait time ensuring it doesn't go below the minimum wait time
            waitTapButton = Mathf.Max(minWaitTime, waitTapButton - decreaseTimerInterval); // Decrease by 0.1 seconds for each iteration

            // Min Wait Time is the current dance style minize 2 secs or 1 - can be random
            //float waitTime = 0.25f * animationManager.GetCurrentStateTime();
            //waitTapButton = waitTime;

            yield return new WaitForSeconds(waitTapButton);

            count++;

            // Check if the music is still playing
            if (!AudioManager.Instance.selectedMusic.isPlaying)
            {
                Debug.Log("Game Over");
                socialMetricsManager.CalculateFollowers();
                gameManager.isGameOver = true;
                gameManager.isStartGame = false;
                break;
            }
        }
    }

    TapPositions CheckAvailablePosition()
    {
        // Filter out the used positions
        var availablePositions = tapPositions.Where(tapPosition => !tapPosition.isUsed).ToList();

        // Check if there are any available positions left
        if (availablePositions.Count > 0)
        {
            // Select a random available position
            int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);
            var selectedPosition = availablePositions[randomIndex];

            // Mark the selected position as used
            selectedPosition.isUsed = true;

            return selectedPosition;
        }
        else
        {
            return null;
        }
    }

    void UpdateComboTimer()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                PerformCombo(); // Perform the combo when the timer ends
                ResetCombo();
            }
        }
    }

    public void OnTapButtonClicked()
    {
        if (comboTimer > 0)
        {
            // Increase combo count if the timer is active
            comboCount++;
        }
        else
        {
            // Start a new combo
            comboCount = 1;
            comboTimer = comboTimeFrame;
            isComboActive = true;
        }
    }
    void PerformCombo()
    {
        if (isComboActive)
        {
            if (comboCount >= 2)
            {
                expressionManager.PerformCombo(10, comboCount);
                Debug.Log("Combo Count : " + comboCount);
            }
            isComboActive = false; // Reset combo state
        }
    }
    void ResetCombo()
    {
        comboCount = 0;
        comboTimer = 0;
        isComboActive = false;
    }
    public void OnTapButtonDestroyed(Button button)
    {
        // Check if the button is in the map
        if (buttonToTapPositionMap.TryGetValue(button, out var tapPosition))
        {
            // Reset the tap position's isUsed status
            tapPosition.isUsed = false;

            // Remove the button from the map
            buttonToTapPositionMap.Remove(button);
        }
    }
}
