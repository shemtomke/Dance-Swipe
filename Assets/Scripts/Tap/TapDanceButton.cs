using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapDanceButton : MonoBehaviour
{
    public Button button;
    public Image imageFillCountdown;
    public Text timerText;
    public float tapTimer; // Adjust for Difficulty // High is Easy, Low is Hard
    public float minTapTimer, maxTapTimer;

    public bool isClicked = false;

    private float initialTapTimer;
    Player player;
    TapDanceManager tapDanceManager;
    AnimationManager animationManager;
    ExpressionManager expressionManager;
    private void Awake()
    {
        tapTimer = maxTapTimer;
        initialTapTimer = tapTimer;
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        animationManager = FindObjectOfType<AnimationManager>();
        tapDanceManager = FindObjectOfType<TapDanceManager>();
        expressionManager = FindObjectOfType<ExpressionManager>();

        button.onClick.AddListener(() =>
        {
            TapButton();
        });
    }
    private void Update()
    {
        timerText.text = Mathf.CeilToInt(tapTimer).ToString();
        imageFillCountdown.fillAmount = tapTimer / initialTapTimer;

        tapTimer -= Time.deltaTime;

        if (tapTimer <= 0)
        {
            if (!isClicked)
            {
                expressionManager.ShowCommentMessage(-5, false);
                tapDanceManager.OnTapButtonDestroyed(button);
            }
            Destroy(gameObject);
        }
    }
    public void TapButton()
    {
        //tapDanceManager.DeductTimer(tapTimer);
        tapDanceManager.OnTapButtonClicked();
        isClicked = true;

        expressionManager.ShowCommentMessage(10, true);

        var currentDanceState = animationManager.currentPlayerState;
        var newDanceState = animationManager.GetRandomAnimation();
        var playerAnimator = player.anim;

        animationManager.SyncAnimationState(playerAnimator, newDanceState, currentDanceState);

        Destroy(gameObject);

        tapDanceManager.OnTapButtonDestroyed(button);
    }
}
