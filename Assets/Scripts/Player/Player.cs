using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkinnedMeshRenderer currentPlayerMesh;
    public Animator anim;

    public void UpdatePlayerCharacterMesh(Mesh mesh)
    {
        currentPlayerMesh.sharedMesh = mesh;
    }
}
