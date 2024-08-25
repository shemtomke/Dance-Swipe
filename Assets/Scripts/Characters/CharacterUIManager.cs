using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIManager : MonoBehaviour
{
    [NonReorderable]
    public List<Character> characterList;

    public GameObject characterSelectionUI;
    public SkinnedMeshRenderer characterMeshModel;
    public Text characterNameText;
    public Text characterSelectionBuyAmount;
    public Button characterSelectButton, characterSelectedButton, characterBuyButton;
    public Button nextCharacterButton, previousCharacterButton;
}
