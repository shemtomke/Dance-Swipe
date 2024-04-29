using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public List<AnimationClip> danceAnimations;

    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        // Get the current state information of the Animator
        AnimatorStateInfo stateInfo = player.anim.GetCurrentAnimatorStateInfo(0);

        // Get current animator state -> Up/Down/Left/Right

        // Change the motion Clip to be random from the dance animation clips
        // AnimatorState animatorState = player.anim;

        // Check if the Animator is playing a motion
        if (stateInfo.IsName("Up"))
        {
            Debug.Log("Motion Up is active");
        }
        if (stateInfo.IsName("Down"))
        {
            Debug.Log("Motion Down is active");
        }
        if (stateInfo.IsName("Left"))
        {
            Debug.Log("Motion Left is active");
        }
        if (stateInfo.IsName("Right"))
        {
            Debug.Log("Motion Right is active");
        }
    }
    int RandomAnimationClip()
    {
        // Generate a random index within the range of danceAnimations indices
        int randomIndex = Random.Range(0, danceAnimations.Count);
        return randomIndex;
    }
}
