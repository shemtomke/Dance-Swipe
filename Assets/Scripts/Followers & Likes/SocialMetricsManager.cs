using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialMetricsManager : MonoBehaviour
{
    public Text likesText, followersText;

    private int currentFollowers, currentLikes; //called on start of playing the game
    private int followers, likes;

    public int lowLikes;
    public int midLikes;
    public int highLikes;
    void Update()
    {
        followersText.text = followers.ToString();
        likesText.text = likes.ToString();
    }
    public void InitializeSocialMetrics()
    {
        currentFollowers = 0;
        currentLikes = 0;
    }
    public int GetCurrentFollowers()
    {
        return currentFollowers;
    }
    public int GetCurrentLikes()
    {
        return currentLikes;
    }
    public void SetCurrentLikes(int curLikes) { currentLikes = curLikes; }
    public int GetAllFollowers()
    {
        return followers;
    }
    public void AddRandomFollowers(int min, int max)
    {
        int randomFollowers = UnityEngine.Random.Range(min, max);
        followers += randomFollowers;
    }
    public void AddFollowers(int newFollowers)
    {
        followers += newFollowers;
    }
    public void RemoveRandomFollowers(int min, int max)
    {
        int randomFollowers = UnityEngine.Random.Range(min, max);
        followers = Mathf.Max(0, followers - randomFollowers);
    }
    public void RemoveFollowers(int newFollowers)
    {
        followers = Mathf.Max(0, followers - newFollowers);
    }
    // Calculate on gameover
    public void CalculateFollowers()
    {
        // For each 100 likes, you get 1 follower
        currentFollowers = currentLikes / 100;
        followers += currentFollowers;
    }
    public int GetAllLikes()
    {
        return likes;
    }
    public void AddLikes(int newLikes)
    {
        likes = Math.Max(0, likes + newLikes);

        currentLikes += newLikes;
    }
    public void ReduceLikes(int newLikes)
    {
        likes = Math.Max(0, likes - newLikes);
        currentLikes -= newLikes;
    }
}
