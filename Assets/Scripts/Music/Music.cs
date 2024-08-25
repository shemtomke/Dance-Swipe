using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Music
{
    public string musicName;
    public MusicType musicType;
    public int unlockableCoins;
    public bool isLocked = true;
    public bool isSelected = false;
    public AudioClip audioClip;
}
