using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DanceAnimation
{
    public string danceName;
    public MusicType musicType;
    public AnimationClip clip;
    public int unlockableCoins;
    public bool isLocked = true;
}
