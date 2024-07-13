using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Followers : MonoBehaviour
{
    public Text followersText;
    private int followers;

    Likes likes;

    private void Start()
    {
        likes = FindObjectOfType<Likes>();
    }
    void Update()
    {
        followersText.text = followers.ToString();

        CalculateFollowers(likes.GetLikes());
    }
    public int GetFollowers()
    {
        return followers;
    }
    public void AddRandomFollowers(int min, int max)
    {
        int randomFollowers = Random.Range(min, max);
        followers += randomFollowers;
    }
    public void AddFollowers(int newFollowers)
    {
        followers += newFollowers;
    }
    public void RemoveRandomFollowers(int min, int max)
    {
        int randomFollowers = Random.Range(min, max);
        followers = Mathf.Max(0, followers - randomFollowers);
    }
    public void RemoveFollowers(int newFollowers)
    {
        followers = Mathf.Max(0, followers - newFollowers);
    }
    public void CalculateFollowers(int likes)
    {
        // For each 100 likes, you get 1 follower
        followers = likes / 100;
    }
}
