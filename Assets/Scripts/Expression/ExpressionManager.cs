using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour
{
    public List<GameObject> comboEffects;
    public GameObject commentPrefab;
    public Transform commentPosition;
    public GameObject emojiPrefab;
    [NonReorderable]
    public List<Sprite> positiveEmoji;
    [NonReorderable]
    public List<Sprite> negativeEmoji;
    [NonReorderable]
    public List<Expression> expression;
    [NonReorderable]
    public List<Expression> comboExpression;
    [NonReorderable]
    public List<Expression> goodExpression;
    [NonReorderable]
    public List<Expression> badExpression;

    Likes likes;
    private void Start()
    {
        likes = FindObjectOfType<Likes>();
    }
    public void ShowCommentMessage(int pointScore, bool isGood)
    {
        StartCoroutine(ShowExpressionMessage(pointScore, isGood));
    }
    public void PerformCombo(int pointScore, int clickedButtons)
    {
        StartCoroutine(ShowExpressionMessage(pointScore, clickedButtons));
    }
    private IEnumerator ShowExpressionMessage(int pointScore, bool isGood)
    {
        GenerateEmoji(isGood);
        likes.AddLikes(pointScore);

        yield return null;
    }
    private IEnumerator ShowExpressionMessage(int pointScore, int clickedButtons)
    {
        GameObject comment = Instantiate(commentPrefab);
        comment.transform.SetParent(commentPosition, false);

        UserComment userComment = comment.GetComponent<UserComment>();

        int randomExpression = Random.Range(0, comboExpression.Count);
        userComment.commentText.text = comboExpression[randomExpression].expression;

        likes.AddLikes(pointScore * clickedButtons);

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        Destroy(comment);
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
