using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    int currentSelectedCharacterIndex;

    Player player;
    CoinsManager coinsManager;
    private void Start()
    {
        player = FindObjectOfType<Player>();

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
        });

        characterSelectedButton.onClick.AddListener(() =>
        {
            currentSelectedCharacterIndex = currentCharacterIndex;
        });

        characterBuyButton.onClick.AddListener(() =>
        {
            UnlockCharacter(currentCharacterIndex);
        });
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
        characterSelectionBuyAmount.text = characterList[index].unlockableCoinsAmount.ToString();

        characterBuyButton.gameObject.SetActive(characterList[index].isLocked);
        characterSelectButton.gameObject.SetActive(!characterList[index].isSelected && !characterList[index].isLocked);
        characterSelectedButton.gameObject.SetActive(characterList[index].isSelected);
    }
    public void UnlockCharacter(int characterIndex)
    {
        if (coinsManager.GetCurrentCoins() >= characterList[characterIndex].unlockableCoinsAmount)
        {
            coinsManager.DeductCoins(characterList[characterIndex].unlockableCoinsAmount);
            characterList[characterIndex].isLocked = false;
        }
    }
    Mesh SelectedCharacterMesh() { return characterList[currentSelectedCharacterIndex].characterMesh; }
    public void SelectCharacter()
    {
        player.UpdatePlayerCharacterMesh(SelectedCharacterMesh());
    }
}
