using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Challenge
{
    public string challengeName;
    public ChallengeType challengeType;
    public RewardType rewardType;
    public bool isCompleted;
    public int target;
    public int rewardPoints;

    public Challenge(string name, ChallengeType challengeType, int reward, int target)
    {
        challengeName = name;
        this.challengeType = challengeType;
        rewardPoints = reward;
        this.target = target;
    }
}
