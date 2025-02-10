using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public List<DanceAnimation> allDanceAnimations;
    public List<AwaitingDance> awaitingDances = new List<AwaitingDance>();
    public Animator danceStyleAnimator;
    public string currentPlayerState, currentDanceStyleState;
    float currentDanceStateTime;

    Player player;
    ShopManager shopManager;
    MusicManager musicManager;
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        musicManager = FindFirstObjectByType<MusicManager>();
        shopManager = FindFirstObjectByType<ShopManager>();
    }
    // Synchronous Animation Change
    public void SyncAnimationState(Animator animator, string newState)
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(newState))
        {
            // If the new state is the same as the current one, don't transition
            return;
        }

        //awaitingDances.Add(new AwaitingDance(newState, false));
        animator.CrossFadeInFixedTime(newState, 0.25f);
    }
    // UnSynchronous Animation Change 
    public void UnSyncAnimationState(Animator animator, string newState, string currentState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
    public void ProcessAwaitingDances(Animator animator)
    {
        if (awaitingDances.Count > 0)
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

            // Check if animation is almost complete
            if (currentState.normalizedTime >= 0.95f && !awaitingDances[0].isCompleted)
            {
                awaitingDances[0].isCompleted = true;
            }

            // If completed, remove from list and transition
            if (awaitingDances[0].isCompleted)
            {
                awaitingDances.RemoveAt(0);

                if (awaitingDances.Count > 0)
                {
                    animator.CrossFadeInFixedTime(awaitingDances[0].danceState, 0.25f);
                }
            }
        }
        else
        {
            // If no awaiting dances, loop the current animation state
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play(currentState.fullPathHash, 0, 0);
        }
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
