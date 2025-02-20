using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceStyleManager : MonoBehaviour
{
    public int currentDanceStyleIndex;
    public SkinnedMeshRenderer danceMeshModel;
    public Text danceStyleName;
    public Text danceBuyAmount;
    public Button danceBuyButton;
    public Button nextDanceButton, previousDanceButton;

    int unlockedDanceStyles;

    AnimationManager animationManager;
    CharacterManager characterManager;
    CoinsManager coinsManager;
    private void Start()
    {
        coinsManager = FindObjectOfType<CoinsManager>();
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

        unlockedDanceStyles = SaveLoad.Instance.LoadInt(SaveLoad.Instance.GetUnlockedDanceStylesKey());
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

        animationManager.UnSyncAnimationState(danceStyleAnimator, newDanceState, currentDanceState);

        danceStyleName.text = allDanceAnimations[index].danceName;

        danceBuyAmount.text = allDanceAnimations[index].unlockableCoins.ToString("N0");
        danceBuyButton.gameObject.SetActive(allDanceAnimations[index].isLocked);

        danceBuyButton.interactable = coinsManager.IsAvailableCoins(allDanceAnimations[index].unlockableCoins);
    }
    public void UnlockAnimation(int danceAnimationIndex)
    {
        if (coinsManager.GetCurrentCoins() >= animationManager.allDanceAnimations[danceAnimationIndex].unlockableCoins)
        {
            coinsManager.DeductCoins(animationManager.allDanceAnimations[danceAnimationIndex].unlockableCoins);
            animationManager.allDanceAnimations[danceAnimationIndex].isLocked = false;

            unlockedDanceStyles++;

            SaveLoad.Instance.SaveInt(SaveLoad.Instance.GetUnlockedDanceStylesKey(), unlockedDanceStyles);
        }
    }
}
