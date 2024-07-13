using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public int currentCoins;

    [Header("Characters")]
    public GameObject characterSelectionUI;
    public SkinnedMeshRenderer characterMeshModel;
    public Text characterSelectionText;
    public Text characterSelectionBuyAmount;
    public Button characterSelectButton, characterSelectedButton, characterBuyButton;
    public Button nextCharacterButton, previousCharacterButton;
    [NonReorderable]
    public List<Character> characterList;
    public int currentCharacterIndex;

    [Header("Dance Styles - Animations")]
    public int currentDanceStyleIndex;
    public SkinnedMeshRenderer danceMeshModel;
    public Text danceBuyAmount;
    public Button danceBuyButton;
    public Button nextDanceButton, previousDanceButton;

    [Header("Music")]
    [NonReorderable]
    public List<Music> musicList;
    public GameObject musicPrefabUI;
    public GameObject musicContentUI;

    [Header("Environment")]
    [NonReorderable]
    public List<Environment> environmentList;

    [Header("Outfit")]
    [NonReorderable]
    public List<Outfit> outfitList;

    public int currentSelectedMusic;
    public int currentSelectedEnvironment;
    public int currentSelectedCharacter;

    AnimationManager animationManager;
    Player player;
    private void Start()
    {
        animationManager = FindObjectOfType<AnimationManager>();

        nextCharacterButton.onClick.AddListener(() =>
        {
            NextCharacter();
        });

        previousCharacterButton.onClick.AddListener(() =>
        {
            PreviousCharacter();
        });

        nextDanceButton.onClick.AddListener(() =>
        {
            NextDanceStyle();
        });

        previousDanceButton.onClick.AddListener(() =>
        {
            PreviousDanceStyle();
        });

        ListAllMusic();
    }
    private void Update()
    {
        DisplayCharacter(currentCharacterIndex);
        DisplayDanceStyle(currentDanceStyleIndex);
        UpdateButtonStates();
    }
    private void UpdateButtonStates()
    {
        nextCharacterButton.interactable = currentCharacterIndex < characterList.Count - 1;
        previousCharacterButton.interactable = currentCharacterIndex > 0;

        nextDanceButton.interactable = currentDanceStyleIndex < animationManager.allDanceAnimations.Count - 1;
        previousDanceButton.interactable = currentDanceStyleIndex > 0;
    }

    #region Characters
    public void NextCharacter()
    {
        if (currentCharacterIndex < characterList.Count - 1)
        {
            currentCharacterIndex++;
        }
    }
    public void PreviousCharacter()
    {
        if (currentCharacterIndex > 0)
        {
            currentCharacterIndex--;
        }
    }
    private void DisplayCharacter(int index)
    {
        characterMeshModel.sharedMesh = characterList[index].characterMesh;
        characterSelectionText.text = characterList[index].characterName;
        characterSelectionBuyAmount.text = characterList[index].unlockableCoinsAmount.ToString();

        characterBuyButton.gameObject.SetActive(characterList[index].isLocked);
        characterSelectButton.gameObject.SetActive(!characterList[index].isSelected && !characterList[index].isLocked);
        characterSelectedButton.gameObject.SetActive(characterList[index].isSelected);
    }
    #endregion

    #region Animations - Dance Styles
    public void NextDanceStyle()
    {
        if (currentDanceStyleIndex < characterList.Count - 1)
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
        //Debug.Log("Player Mesh : " + player.currentPlayerMesh.sharedMesh.name);
        Debug.Log("Dance Model Mesh : " + danceMeshModel.sharedMesh.name);
        //danceMeshModel.sharedMesh = player.currentPlayerMesh.sharedMesh;
        danceBuyAmount.text = allDanceAnimations[index].unlockableCoins.ToString();

        danceBuyButton.gameObject.SetActive(allDanceAnimations[index].isLocked);
    }
    public void UnlockAnimation(DanceAnimation danceAnimation)
    {
        if (currentCoins >= danceAnimation.unlockableCoins)
        {
            Purchase(danceAnimation.unlockableCoins);
            danceAnimation.isLocked = false;
        }
    }
    #endregion

    #region Music List
    public void ListAllMusic()
    {
        for (int i = 0; i < musicList.Count; i++)
        {
            GameObject music = Instantiate(musicPrefabUI);
            music.transform.SetParent(musicContentUI.transform, false);

            MusicContainer musicContainer = music.GetComponent<MusicContainer>();
            musicContainer.music = musicList[i];
        }
    }
    public void UnlockMusic(Music music)
    {
        if (currentCoins >= music.unlockableCoins)
        {
            Purchase(music.unlockableCoins);
            music.isLocked = false;
        }
    }
    public void SelectMusic(Music music)
    {
        currentSelectedMusic = musicList.IndexOf(music);
    }
    public void MusicStatus(Music music)
    {
        if(currentSelectedMusic == musicList.IndexOf(music))
        {
            music.isSelected = true;
        }
        else
        {
            music.isSelected = false;
        }
    }
    #endregion

    public void Purchase(int amount)
    {
        currentCoins -= amount;
    }
}
