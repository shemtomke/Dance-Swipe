using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [NonReorderable]
    public List<DanceAnimation> allDanceAnimations;

    public Animator danceStyleAnimator;
    public string currentPlayerState, currentDanceStyleState;
    Player player;
    ShopManager shopManager;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        
    }
    public void ChangeAnimationState(Animator animator, string newState, string currentState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
    public string GetRandomAnimation()
    {
        // Create a list to hold unlocked animations
        List<AnimationClip> unlockedAnimations = new List<AnimationClip>();

        // Populate the list with unlocked animations
        foreach (var animation in allDanceAnimations)
        {
            if (!animation.isLocked)
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
        return unlockedAnimations[randomIndex].name;
    }
}
