using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public GameObject challengeUIPrefab;
    public GameObject challengeUIContainer;
    public GameObject newChallengeNotification;

    [NonReorderable]
    public List<Challenge> challenges = new List<Challenge>();

    private int unlockCharacterTarget = 2;
    private int unlockMusicTarget = 1;
    private int unlockDanceStyleTarget = 1;
    private int likesTarget = 20;
    private int followersTarget = 5;
    private int completedChallengesTarget = 4;
    private int coinsTarget = 10;

    private int unlockedChallenges = 0;

    private void Start()
    {
        GenerateAllChallenges();
    }

    private void GenerateAllChallenges()
    {
        GenerateChallenges();

        foreach (var challenge in challenges)
        {
            if (!challenge.isCompleted)
            {
                CreateChallengeUI(challenge);
            }
        }
    }

    private void GenerateChallenges()
    {
        unlockedChallenges = SaveLoad.Instance.LoadInt(SaveLoad.Instance.GetCompletedChallengesKey());

        challenges.Clear();

        AddChallenge(new Challenge($"Unlock {unlockCharacterTarget} Characters", ChallengeType.UnlockCharacters, CalculateReward(100, unlockCharacterTarget), unlockCharacterTarget));
        AddChallenge(new Challenge($"Unlock {unlockMusicTarget} Music Tracks", ChallengeType.UnlockMusic, CalculateReward(50, unlockMusicTarget), unlockMusicTarget));
        AddChallenge(new Challenge($"Unlock {unlockDanceStyleTarget} Dance Styles", ChallengeType.UnlockDanceStyle, CalculateReward(75, unlockDanceStyleTarget), unlockDanceStyleTarget));
        AddChallenge(new Challenge($"Reach {likesTarget} Likes", ChallengeType.Likes, CalculateReward(150, likesTarget), likesTarget));
        AddChallenge(new Challenge($"Reach {followersTarget} Followers", ChallengeType.Followers, CalculateReward(200, followersTarget), followersTarget));
        AddChallenge(new Challenge($"Complete {completedChallengesTarget} Challenges", ChallengeType.CompletedChallenges, CalculateReward(250, completedChallengesTarget), completedChallengesTarget));
        AddChallenge(new Challenge($"Collect {coinsTarget} Coins", ChallengeType.Coins, CalculateReward(300, coinsTarget), coinsTarget));

        AdjustChallengeTargets();
    }

    private void AdjustChallengeTargets()
    {
        unlockCharacterTarget = Mathf.Min(unlockCharacterTarget * 2, 10);
        unlockMusicTarget++;
        unlockDanceStyleTarget++;
        likesTarget += UnityEngine.Random.Range(10, 50);
        followersTarget += 5;
        completedChallengesTarget += 3;
        coinsTarget += UnityEngine.Random.Range(10, 50);
    }

    private int CalculateReward(int baseReward, int targetValue)
    {
        return baseReward + (targetValue * 10);
    }

    private void AddChallenge(Challenge newChallenge)
    {
        challenges.Add(newChallenge);
    }

    public void CompleteChallenge(Challenge challenge)
    {
        if (!challenge.isCompleted)
        {
            challenge.isCompleted = true;
            SaveLoad.Instance.SaveInt(SaveLoad.Instance.GetCompletedChallengesKey(), unlockedChallenges);

            Debug.Log($"Challenge completed: {challenge.challengeName}. Reward: {challenge.rewardPoints} points");

            ReplaceCompletedChallenge(challenge);
        }
    }

    private void ReplaceCompletedChallenge(Challenge completedChallenge)
    {
        int challengeIndex = challenges.FindIndex(c => c.challengeType == completedChallenge.challengeType);

        if (challengeIndex != -1)
        {
            challenges.RemoveAt(challengeIndex);
            Challenge newChallenge = GenerateNewChallenge(completedChallenge.challengeType);
            challenges.Add(newChallenge);

            RefreshChallengeUI();
        }
    }

    private Challenge GenerateNewChallenge(ChallengeType type)
    {
        switch (type)
        {
            case ChallengeType.UnlockCharacters:
                return new Challenge($"Unlock {unlockCharacterTarget} Characters", ChallengeType.UnlockCharacters, CalculateReward(100, unlockCharacterTarget), unlockCharacterTarget);
            case ChallengeType.UnlockMusic:
                return new Challenge($"Unlock {unlockMusicTarget} Music Tracks", ChallengeType.UnlockMusic, CalculateReward(50, unlockMusicTarget), unlockMusicTarget);
            case ChallengeType.UnlockDanceStyle:
                return new Challenge($"Unlock {unlockDanceStyleTarget} Dance Styles", ChallengeType.UnlockDanceStyle, CalculateReward(75, unlockDanceStyleTarget), unlockDanceStyleTarget);
            case ChallengeType.Likes:
                return new Challenge($"Reach {likesTarget} Likes", ChallengeType.Likes, CalculateReward(150, likesTarget), likesTarget);
            case ChallengeType.Followers:
                return new Challenge($"Reach {followersTarget} Followers", ChallengeType.Followers, CalculateReward(200, followersTarget), followersTarget);
            case ChallengeType.CompletedChallenges:
                return new Challenge($"Complete {completedChallengesTarget} Challenges", ChallengeType.CompletedChallenges, CalculateReward(250, completedChallengesTarget), completedChallengesTarget);
            case ChallengeType.Coins:
                return new Challenge($"Collect {coinsTarget} Coins", ChallengeType.Coins, CalculateReward(300, coinsTarget), coinsTarget);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public int GetCurrentChallengeProgress(ChallengeType type)
    {
        var saveLoad = SaveLoad.Instance;

        return type switch
        {
            ChallengeType.UnlockCharacters => saveLoad.LoadInt(saveLoad.GetUnlockedCharactersKey()),
            ChallengeType.UnlockMusic => saveLoad.LoadInt(saveLoad.GetUnlockedMusicKey()),
            ChallengeType.UnlockDanceStyle => saveLoad.LoadInt(saveLoad.GetUnlockedDanceStylesKey()),
            ChallengeType.Likes => saveLoad.LoadInt(saveLoad.GetLikesKey()),
            ChallengeType.Followers => saveLoad.LoadInt(saveLoad.GetFollowersKey()),
            ChallengeType.Coins => saveLoad.LoadInt(saveLoad.GetCoinsKey()),
            ChallengeType.CompletedChallenges => saveLoad.LoadInt(saveLoad.GetCompletedChallengesKey()),
            _ => 0,
        };
    }

    private void CreateChallengeUI(Challenge challenge)
    {
        GameObject challengeObj = Instantiate(challengeUIPrefab);
        challengeObj.transform.SetParent(challengeUIContainer.transform, false);

        ChallengeUI challengeUI = challengeObj.GetComponent<ChallengeUI>();
        challengeUI.challenge = challenge;
    }

    private void RefreshChallengeUI()
    {
        // Clear existing UI elements
        foreach (Transform child in challengeUIContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Recreate UI for each challenge
        foreach (var challenge in challenges)
        {
            if (!challenge.isCompleted)
            {
                CreateChallengeUI(challenge);
            }
        }
    }
}
