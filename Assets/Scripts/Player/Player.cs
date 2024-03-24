using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject currentPlayer;
    public Animator anim;

    public void TriggerAnimation(string triggerName)
    {
        switch (triggerName)
        {
            case "up":
                anim.SetTrigger("up");
                break;
            case "down":
                anim.SetTrigger("down");
                break;
            case "left":
                anim.SetTrigger("left");
                break;
            case "right":
                anim.SetTrigger("right");
                break;
            default:
                Debug.LogWarning("Unknown trigger: " + triggerName);
                break;
        }
    }
}
