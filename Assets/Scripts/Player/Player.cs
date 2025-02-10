using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkinnedMeshRenderer currentPlayerMesh;
    public Animator anim;
    CharacterManager characterManager;

    AnimationManager animationManager;
    private void Start()
    {
        characterManager = FindFirstObjectByType<CharacterManager>();
        animationManager = FindFirstObjectByType<AnimationManager>();
    }
    private void Update()
    {
        currentPlayerMesh.sharedMesh = characterManager.GetSelectedCharacter().characterMesh;
        //animationManager.ProcessAwaitingDances(anim);
    }
}
