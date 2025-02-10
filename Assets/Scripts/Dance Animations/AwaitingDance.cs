using System;
using UnityEngine;

/// <summary>
/// This class stores the awaiting dances to be excuted by the player.
/// </summary>
[Serializable]
public class AwaitingDance
{
    public string danceState;
    public bool isCompleted = false;

    public AwaitingDance(string danceState, bool isCompleted)
    {
        this.danceState = danceState;
        this.isCompleted = isCompleted;
    }
}
