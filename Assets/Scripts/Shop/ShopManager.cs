using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Characters
    // Music
    // Environment
    // Outfits - Watch, Cap, Shirt, Trouser, Short, Specs
    // Coins
    [Header("Characters")]
    [NonReorderable]
    public List<Character> characterList;
    [Header("Music")]
    [NonReorderable]
    public List<Music> musicList;
    [Header("Environment")]
    [NonReorderable]
    public List<Environment> environmentList;
    [Header("Outfit")]
    [NonReorderable]
    public List<Outfit> outfitList;


    public int currentSelectedMusic;
    public int currentSelectedEnvironment;
    public int currentSelectedCharacter;


}
