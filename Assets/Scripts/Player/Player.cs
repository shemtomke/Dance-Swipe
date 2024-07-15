using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkinnedMeshRenderer currentPlayerMesh;
    public Animator anim;
    CharacterManager characterManager;

    private void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
    }
    private void Update()
    {
        currentPlayerMesh.sharedMesh = characterManager.GetSelectedCharacter().characterMesh;
    }
}
