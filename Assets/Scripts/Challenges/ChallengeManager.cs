using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public List<Challenge> challenges = new List<Challenge>();

    public void AddChallenge(Challenge newChallenge)
    {
        challenges.Add(newChallenge);
    }
    public void CompleteChallenge(Challenge challenge)
    {
        if (!challenge.isCompleted)
        {
            challenge.isCompleted = true;
            // Grant rewards or update the game state as needed
            Debug.Log($"Challenge completed: {challenge.challengeName}. Reward: {challenge.rewardPoints} points");
        }
    }
    public void CheckAllChallenges()
    {
        foreach (var challenge in challenges)
        {
            //if (/* condition to check if challenge is completed */)
            //{
            //    CompleteChallenge(challenge);
            //}
        }
    }
}
