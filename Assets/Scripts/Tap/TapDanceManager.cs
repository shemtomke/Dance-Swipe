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

    [NonReorderable]
    public List<TapButton> tapButtons;
    [NonReorderable]
    public List<TapPositions> tapPositions = new List<TapPositions>();
    private Dictionary<Button, TapPositions> buttonToTapPositionMap = new Dictionary<Button, TapPositions>();
    public Button tapButtonPrefab;
    public float waitTapButton;
    int count = 0;

    Player player;
    ExpressionManager expressionManager;
    SocialMetricsManager socialMetricsManager;

    [Header("Combo Properties")]
    public float comboTimer = 0f;
    public float comboTimeFrame = 1f; // 1 second for the combo
    public int comboCount = 0;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        expressionManager = FindObjectOfType<ExpressionManager>();
        socialMetricsManager = FindObjectOfType<SocialMetricsManager>();

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
            while (!GameManager.Instance.isStartGame)
            {
                yield return null;
            }

            Button newButton = null;
            TapPositions selectedPosition = null;

            // Keep trying to find an available position until one is found
            while (selectedPosition == null)
            {
                newButton = Instantiate(tapButtonPrefab);
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

            yield return new WaitForSeconds(waitTapButton);

            count++;

            // Check if the music is still playing
            if (!AudioManager.Instance.selectedMusic.isPlaying)
            {
                Debug.Log("Game Over");
                socialMetricsManager.CalculateFollowers();
                GameManager.Instance.isGameOver = true;
                GameManager.Instance.isStartGame = false;
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
                ResetCombo();
            }
        }
    }
    public void OnTapButtonClicked()
    {
        if (comboTimer > 0)
        {
            comboCount++;
            if (comboCount >= 3)
            {
                Debug.Log("Combo!");
                expressionManager.PerformCombo(10, comboCount);
                //ResetCombo();
            }
            else
            {
                comboTimer = comboTimeFrame; // Reset the timer
            }
        }
        else
        {
            comboCount = 1;
            comboTimer = comboTimeFrame; // Start the timer
        }
    }
    void ResetCombo()
    {
        comboCount = 0;
        comboTimer = 0;
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
