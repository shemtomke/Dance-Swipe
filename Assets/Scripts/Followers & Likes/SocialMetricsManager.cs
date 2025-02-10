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
        followersText.text = FormatNumber(followers);
        likesText.text = FormatNumber(likes);
    }
    public string FormatNumber(int value)
    {
        float num = value;
        string suffix = "";

        if (num >= 1_000_000_000) // Billions (B)
        {
            num /= 1_000_000_000f;
            suffix = "B";
        }
        else if (num >= 1_000_000) // Millions (M)
        {
            num /= 1_000_000f;
            suffix = "M";
        }
        else if (num >= 1_000) // Thousands (k)
        {
            num /= 1_000f;
            suffix = "K";
        }

        // Format the number with one decimal place and remove .0 if it's a whole number
        return num.ToString(num % 1 == 0 ? "0" : "0.0") + suffix;
    }
    void LoadData()
    {
        var saveManager = SaveLoad.Instance;
        followers = saveManager.LoadInt(saveManager.GetFollowersKey());
        likes = saveManager.LoadInt(saveManager.GetLikesKey());
    }
    void SaveData()
    {
        var saveManager = SaveLoad.Instance;
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
