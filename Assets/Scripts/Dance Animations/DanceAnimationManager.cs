using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceAnimationManager : MonoBehaviour
{
    public int currentDanceStyleIndex;
    public SkinnedMeshRenderer danceMeshModel;
    public Text danceBuyAmount;
    public Button danceBuyButton;
    public Button nextDanceButton, previousDanceButton;

    AnimationManager animationManager;
    CharacterManager characterManager;
    private void Start()
    {
        animationManager = FindObjectOfType<AnimationManager>();
        characterManager = FindObjectOfType<CharacterManager>();

        nextDanceButton.onClick.AddListener(() =>
        {
            NextDanceStyle();
        });

        previousDanceButton.onClick.AddListener(() =>
        {
            PreviousDanceStyle();
        });

        danceBuyButton.onClick.AddListener(() =>
        {
            UnlockAnimation(currentDanceStyleIndex);
        });
    }
    private void Update()
    {
        DisplayDanceStyle(currentDanceStyleIndex);

        nextDanceButton.interactable = currentDanceStyleIndex < animationManager.allDanceAnimations.Count - 1;
        previousDanceButton.interactable = currentDanceStyleIndex > 0;
    }
    public void NextDanceStyle()
    {
        if (currentDanceStyleIndex < characterManager.characterList.Count - 1)
        {
            currentDanceStyleIndex++;
        }
    }
    public void PreviousDanceStyle()
    {
        if (currentDanceStyleIndex > 0)
        {
            currentDanceStyleIndex--;
        }
    }
    private void DisplayDanceStyle(int index)
    {
        var allDanceAnimations = animationManager.allDanceAnimations;
        var currentDanceState = animationManager.currentDanceStyleState;
        var newDanceState = allDanceAnimations[index].clip.name;
        var danceStyleAnimator = animationManager.danceStyleAnimator;

        animationManager.ChangeAnimationState(danceStyleAnimator, newDanceState, currentDanceState);

        Debug.Log("Dance Model Mesh : " + danceMeshModel.sharedMesh.name);

        danceBuyAmount.text = allDanceAnimations[index].unlockableCoins.ToString();
        danceBuyButton.gameObject.SetActive(allDanceAnimations[index].isLocked);
    }
    public void UnlockAnimation(int danceAnimationIndex)
    {
        if (ShopManager.Instance.GetCurrentCoins() >= animationManager.allDanceAnimations[danceAnimationIndex].unlockableCoins)
        {
            ShopManager.Instance.Purchase(animationManager.allDanceAnimations[danceAnimationIndex].unlockableCoins);
            animationManager.allDanceAnimations[danceAnimationIndex].isLocked = false;
        }
    }
}