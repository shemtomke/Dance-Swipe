using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public GameObject characterSelectionUI;
    public SkinnedMeshRenderer characterMeshModel;
    public Text characterNameText;
    public Text characterSelectionBuyAmount;
    public Button characterSelectButton, characterSelectedButton, characterBuyButton;
    public Button nextCharacterButton, previousCharacterButton;
    [NonReorderable]
    public List<Character> characterList;

    int currentCharacterIndex;
    int currentSelectedCharacterIndex = 0;
    int unlockedCharacters;
    Character selectedCharacter;

    Player player;
    CoinsManager coinsManager;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        coinsManager = FindObjectOfType<CoinsManager>();

        selectedCharacter = characterList[currentSelectedCharacterIndex];

        nextCharacterButton.onClick.AddListener(() =>
        {
            NextCharacter();
        });

        previousCharacterButton.onClick.AddListener(() =>
        {
            PreviousCharacter();
        });

        characterSelectButton.onClick.AddListener(() =>
        {
            currentSelectedCharacterIndex = currentCharacterIndex;
            SelectCharacter();
            CharacterStatus(currentSelectedCharacterIndex);
        });

        characterBuyButton.onClick.AddListener(() =>
        {
            UnlockCharacter(currentCharacterIndex);
        });

        unlockedCharacters = SaveLoad.Instance.LoadInt(SaveLoad.Instance.GetUnlockedCharactersKey());
    }
    private void Update()
    {
        DisplayCharacter(currentCharacterIndex);

        nextCharacterButton.interactable = currentCharacterIndex < characterList.Count - 1;
        previousCharacterButton.interactable = currentCharacterIndex > 0;
    }
    public int GetCharacterIndex() { return currentCharacterIndex; }
    public int GetSelectedCharacterIndex() { return currentSelectedCharacterIndex; }
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
        characterNameText.text = characterList[index].characterName;
        characterSelectionBuyAmount.text = characterList[index].unlockableCoinsAmount.ToString("N0");

        characterBuyButton.gameObject.SetActive(characterList[index].isLocked);
        characterSelectButton.gameObject.SetActive(!characterList[index].isSelected && !characterList[index].isLocked);
        characterSelectedButton.gameObject.SetActive(characterList[index].isSelected);

        characterBuyButton.interactable = coinsManager.IsAvailableCoins(characterList[index].unlockableCoinsAmount);
    }
    public void UnlockCharacter(int characterIndex)
    {
        if (coinsManager.GetCurrentCoins() >= characterList[characterIndex].unlockableCoinsAmount)
        {
            coinsManager.DeductCoins(characterList[characterIndex].unlockableCoinsAmount);
            characterList[characterIndex].isLocked = false;

            unlockedCharacters++;

            SaveLoad.Instance.SaveInt(SaveLoad.Instance.GetUnlockedCharactersKey(), unlockedCharacters);
        }
    }
    public void SelectCharacter()
    {
        selectedCharacter = characterList[currentSelectedCharacterIndex];
    }
    public void CharacterStatus(int characterIndex)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            if (i == characterIndex)
            {
                characterList[i].isSelected = true;
            }
            else
            {
                characterList[i].isSelected = false;
            }
        }
    }
    public Character GetSelectedCharacter() { return selectedCharacter; }
}
