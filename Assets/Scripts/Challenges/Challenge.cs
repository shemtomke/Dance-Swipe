using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Challenge
{
    public string challengeName;
    public string description;
    public bool isCompleted;
    public int rewardPoints;

    public Challenge(string name, string desc, int reward)
    {
        challengeName = name;
        description = desc;
        isCompleted = false;
        rewardPoints = reward;
    }
}
