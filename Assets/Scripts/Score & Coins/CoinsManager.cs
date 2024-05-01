using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public Text coinsText;
    public int coins;

    ScoreManager scoreManager;
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    public void ConvertToCoins()
    {
        // for each 100 points is 1 coin
        if(GameManager.Instance.isGameOver || GameManager.Instance.isWin)
        {
            int score = scoreManager.GetScore();
            coins += Mathf.FloorToInt(score / 100f);
        }
    }
}
