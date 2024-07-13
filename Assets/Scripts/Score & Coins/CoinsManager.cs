using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public Text coinsText;
    public int coins;

    Followers followers;
    private void Start()
    {
        followers = FindObjectOfType<Followers>();
    }
    public void ConvertToCoins()
    {
        // for each 100 points is 1 coin
        if(GameManager.Instance.isGameOver || GameManager.Instance.isWin)
        {
            //followers.AddFollowers()
            //int score = scoreManager.GetScore();

            //coins += Mathf.FloorToInt(score / 100f);
        }
    }
}
