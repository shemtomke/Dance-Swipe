using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Button tapScreenButton;
    [SerializeField] GameObject homeUI;
    [SerializeField] GameObject inGameUI;

    public bool isGameOver = false;
    public bool isWin = false;
    public bool isStartGame = false;

    TapDanceManager tapDanceManager;
    SocialMetricsManager socialMetricsManager;
    MusicManager musicManager;
    private void Start()
    {
        tapDanceManager = FindFirstObjectByType<TapDanceManager>();
        musicManager = FindFirstObjectByType<MusicManager>();
        socialMetricsManager = FindFirstObjectByType<SocialMetricsManager>();

        isStartGame = false;
        homeUI.SetActive(!isStartGame);
        inGameUI.SetActive(isStartGame);

        tapScreenButton.onClick.AddListener(() =>
        {
            StartGame();
        });
    }
    private void Update()
    {
        GameOver();
    }
    public string FormatNumber(int amount)
    {
        if (amount >= 10000)
        {
            return (amount / 1000).ToString() + "K";
        }
        else
        {
            return amount.ToString("N0");
        }
    }
    void StartGame()
    {
        socialMetricsManager.InitializeSocialMetrics();
        musicManager.StartMusicTimer();

        AudioManager.Instance.PlaySelectedMusic();

        isStartGame = true;
        isGameOver = false;

        tapScreenButton.gameObject.SetActive(!isStartGame);
        homeUI.SetActive(!isStartGame);
        inGameUI.SetActive(isStartGame);
    }
    void GameOver()
    {
        if (isGameOver)
        {
            isStartGame = false;
            Debug.Log("Is Game Over!");
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
