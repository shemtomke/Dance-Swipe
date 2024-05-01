using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] GameObject gameOverUI;

    public bool isGameOver = false;
    public bool isWin = false;

    void GameOver()
    {
        //show gameover screen

    }
    void Win()
    {
        //show win screen

    }
    void ExitGame()
    {
        //quit game

    }
    void ReplayGame()
    {

    }
}
