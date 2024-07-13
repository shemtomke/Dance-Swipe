using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Music
{
    public string musicName;
    public int unlockableCoins;
    public bool isLocked = true;
    public bool isSelected = false;
    public AudioClip audioClip;
}
