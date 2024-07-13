using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject tapScreenUI;
    [SerializeField] GameObject homeUI;
    [SerializeField] GameObject inGameUI;

    public RectTransform tapArea;

    public bool isGameOver = false;
    public bool isWin = false;
    public bool isStartGame = false;

    TapDanceManager tapDanceManager;
    private void Start()
    {
        tapDanceManager = FindObjectOfType<TapDanceManager>();

        isStartGame = false;
        homeUI.SetActive(!isStartGame);
        inGameUI.SetActive(isStartGame);
    }
    private void Update()
    {
        // Check if the screen is tapped or the left mouse button is clicked
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && tapScreenUI.activeInHierarchy && IsTappedInTapArea())
        {
            StartGame();
        }

        GameOver();
    }
    void StartGame()
    {
        AudioManager.Instance.PlaySelectedMusic();
        isStartGame = true;
        isGameOver = false;
        tapScreenUI.SetActive(!isStartGame);
        homeUI.SetActive(!isStartGame);
        inGameUI.SetActive(isStartGame);
    }
    bool IsTappedInTapArea()
    {
        // Check if any touches exist
        if (Input.touchCount > 0)
        {
            // Check each touch
            foreach (Touch touch in Input.touches)
            {
                // Check if the touch is within the tap area
                if (RectTransformUtility.RectangleContainsScreenPoint(tapArea, touch.position))
                {
                    return true;
                }
            }
        }
        // Check if the mouse button is clicked within the tap area
        else if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse position is within the tap area
            if (RectTransformUtility.RectangleContainsScreenPoint(tapArea, Input.mousePosition))
            {
                return true;
            }
        }

        return false;
    }
    void GameOver()
    {
        if (isGameOver)
        {
            isStartGame = false;
        }
        //show gameover screen
        gameOverUI.SetActive(isGameOver);
    }
    public void Retry()
    {
        StartGame();
        tapDanceManager.GenerateTapButtons();
    }
}
