using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [NonReorderable]
    public List<DanceAnimation> allDanceAnimations;

    public Animator danceStyleAnimator;
    public string currentPlayerState, currentDanceStyleState;
    float currentDanceStateTime;

    Player player;
    ShopManager shopManager;
    MusicManager musicManager;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        musicManager = FindObjectOfType<MusicManager>();
        shopManager = FindObjectOfType<ShopManager>();

    }
    private void Update()
    {
        
    }
    // Synchronous Animation Change
    public void SyncAnimationState(Animator animator, string newState, string currentState)
    {
        if (currentState == newState) return;

        // Wait till the dance move is completed to transition
        
        animator.CrossFadeInFixedTime(newState, 0.25f);
        currentState = newState;
    }
    // UnSynchronous Animation Change 
    public void UnSyncAnimationState(Animator animator, string newState, string currentState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
    DanceAnimation GetCurrentAnimation()
    {
        return allDanceAnimations[1];
    }
    public float GetCurrentStateTime() { return currentDanceStateTime; }
    void UpdateCurrentStateTime(float time) { currentDanceStateTime = time; }
    public string GetRandomAnimation()
    {
        // Create a list to hold unlocked animations
        List<AnimationClip> unlockedAnimations = new List<AnimationClip>();

        // Populate the list with unlocked animations
        foreach (var animation in allDanceAnimations)
        {
            if (!animation.isLocked && animation.musicType == musicManager.GetMusicType())
            {
                unlockedAnimations.Add(animation.clip);
            }
        }

        // If there are no unlocked animations, return null
        if (unlockedAnimations.Count == 0)
        {
            return null;
        }

        // Select a random animation from the list of unlocked animations
        int randomIndex = Random.Range(0, unlockedAnimations.Count);
        UpdateCurrentStateTime(unlockedAnimations[randomIndex].length);
        return unlockedAnimations[randomIndex].name;
    }
}
