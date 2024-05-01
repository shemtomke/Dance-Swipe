using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE : " + currentScore;
    }
    public int GetScore()
    {
        if(GameManager.Instance.isGameOver || GameManager.Instance.isWin)
            return currentScore;
        return 0;
    }
}
