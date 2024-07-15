using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    const string claimTimeKey = "LastClaimTime";
    const string dayKey = "CurrentDay";
    const string coinsKey = "Coins";
    const string selectedCharacterKey = "Character";
    const string selectedMusicKey = "Music";
    const string followersKey = "Followers";
    const string likesKey = "Likes";

    public string GetClaimTimeKey() { return claimTimeKey; }
    public string GetDayKey() { return dayKey; }
    public string GetCoinsKey() { return coinsKey; }
    public string GetCharacterKey() { return selectedCharacterKey; }
    public string GetMusicKey() { return selectedMusicKey; }
    public string GetFollowersKey() { return followersKey; }
    public string GetLikesKey() {  return likesKey; }
    // Save a string value with a specific key
    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    // Load a string value with a specific key
    public string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    // Save an integer value with a specific key
    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // Load an integer value with a specific key
    public int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    // Save a float value with a specific key
    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    // Load a float value with a specific key
    public float LoadFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    // Check if a key exists
    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    // Delete a specific key
    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

    // Delete all saved keys
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
