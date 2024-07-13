using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Likes : MonoBehaviour
{
    public Text likesText;
    private int currentLikes;

    public int lowLikes;
    public int midLikes;
    public int highLikes;
    private void Start()
    {
        currentLikes = 0;
    }

    void Update()
    {
        likesText.text = currentLikes.ToString();
    }
    public int GetLikes()
    {
        return currentLikes;
    }
    public void AddLikes(int newLikes)
    {
        currentLikes = Math.Max(0, currentLikes + newLikes);
    }
    public void ReduceLikes(int newLikes)
    {
        currentLikes = Math.Max(0, currentLikes - newLikes);
    }
}
