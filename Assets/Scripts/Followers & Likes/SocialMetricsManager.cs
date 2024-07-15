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

    private void Start()
    {
        LoadData();
    }
    void Update()
    {
        followersText.text = followers.ToString("N0");
        likesText.text = likes.ToString("N0");
    }
    void LoadData()
    {
        var saveManager = SaveManager.Instance;
        saveManager.LoadInt(saveManager.GetFollowersKey());
        saveManager.LoadInt(saveManager.GetLikesKey());
    }
    void SaveData()
    {
        var saveManager = SaveManager.Instance;
        saveManager.SaveInt(saveManager.GetFollowersKey(), followers);
        saveManager.SaveInt(saveManager.GetLikesKey(), likes);
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

        SaveData();
    }
    public void AddFollowers(int newFollowers)
    {
        followers += newFollowers;

        SaveData();
    }
    public void RemoveRandomFollowers(int min, int max)
    {
        int randomFollowers = UnityEngine.Random.Range(min, max);
        followers = Mathf.Max(0, followers - randomFollowers);

        SaveData();
    }
    public void RemoveFollowers(int newFollowers)
    {
        followers = Mathf.Max(0, followers - newFollowers);

        SaveData();
    }
    // Calculate on gameover
    public void CalculateFollowers()
    {
        // For each 100 likes, you get 1 follower
        currentFollowers = currentLikes / 100;
        followers += currentFollowers;

        SaveData();
    }
    public int GetAllLikes()
    {
        return likes;
    }
    public void AddLikes(int newLikes)
    {
        likes = Math.Max(0, likes + newLikes);

        currentLikes += newLikes;

        SaveData();
    }
    public void ReduceLikes(int newLikes)
    {
        likes = Math.Max(0, likes - newLikes);
        currentLikes -= newLikes;

        SaveData();
    }
}
