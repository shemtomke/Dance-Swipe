using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour
{
    public Text expressionTxt1, expressionTxt2;
    [NonReorderable]
    public List<Expression> expressions;

    ScoreManager scoreManager;
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();


        expressionTxt1.text = "";
        expressionTxt2.text = "";
    }

    public void ExpressionMessage(string firstMessage, string secondMessage, int pointScore)
    {
        expressionTxt1.text = "" + firstMessage;
        expressionTxt2.text = "" + secondMessage;
        scoreManager.currentScore += pointScore;
    }
}
