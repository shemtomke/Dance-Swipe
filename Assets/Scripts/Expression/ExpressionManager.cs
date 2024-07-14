using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour
{
    [Header("Combo")]
    public List<GameObject> comboEffects;
    public Vector3 comboPosition;

    [Header("Emojis")]
    public GameObject emojiPrefab;
    [NonReorderable]
    public List<Sprite> positiveEmoji;
    [NonReorderable]
    public List<Sprite> negativeEmoji;

    [Header("Comments")]
    public GameObject commentPrefab;
    public Transform commentPosition;
    [NonReorderable]
    public List<Expression> goodExpression;
    [NonReorderable]
    public List<Expression> badExpression;

    SocialMetricsManager metricsManager;
    private void Start()
    {
        metricsManager = FindObjectOfType<SocialMetricsManager>();
    }
    public GameObject Combo()
    {
        int randomComboIndex = Random.Range(0, comboEffects.Count);
        GameObject combo = Instantiate(comboEffects[randomComboIndex]);
        combo.transform.position = comboPosition;

        return combo;
    }
    public GameObject RandomComment(bool isPositive)
    {
        GameObject comment = Instantiate(commentPrefab);
        comment.transform.SetParent(commentPosition, false);

        UserComment userComment = comment.GetComponent<UserComment>();

        if (isPositive)
        {
            int randomExpression = Random.Range(0, goodExpression.Count);
            userComment.commentText.text = goodExpression[randomExpression].expression;
        }
        else
        {
            int randomExpression = Random.Range(0, badExpression.Count);
            userComment.commentText.text = badExpression[randomExpression].expression;
        }
        
        return comment;
    }
    // Comment & Emoji
    public void ShowCommentMessage(int pointScore, bool isGood)
    {
        StartCoroutine(ShowExpressionMessage(pointScore, isGood));
    }
    private IEnumerator ShowExpressionMessage(int pointScore, bool isGood)
    {
        GameObject comment = RandomComment(isGood);

        GenerateEmoji(isGood);
        metricsManager.AddLikes(pointScore);

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        Destroy(comment);
    }
    // Combo
    public void PerformCombo(int pointScore, int clickedButtons)
    {
        StartCoroutine(ShowCombo(pointScore, clickedButtons));
    }
    private IEnumerator ShowCombo(int pointScore, int clickedButtons)
    {
        GameObject combo = Combo();

        metricsManager.AddLikes(pointScore * clickedButtons);

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        Destroy(combo);
    }
    public Sprite SelectEmoji(bool isPositive)
    {
        if (isPositive)
        {
            int randomndex = Random.Range(0, positiveEmoji.Count);
            return positiveEmoji[randomndex];
        }
        else
        {
            int randomndex = Random.Range(0, negativeEmoji.Count);
            return positiveEmoji[randomndex];
        }
    }
    public void GenerateEmoji(bool isPositive)
    {
        GameObject emojiObj = Instantiate(emojiPrefab, emojiPrefab.transform.position, Quaternion.identity);

        Emoji emoji = emojiObj.GetComponent<Emoji>();
        emoji.sprite = SelectEmoji(isPositive);
    }
}
