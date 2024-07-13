using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    public string characterName;
    public int unlockableCoinsAmount;
    public bool isLocked = true;
    public bool isSelected = false;
    public Mesh characterMesh;
}
