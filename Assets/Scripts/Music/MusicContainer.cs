using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MusicContainer : MonoBehaviour
{
    public Music music;

    public Button selectButton;
    public Button buyButton;
    public Button selectedButton;

    public Text musicName;
    public Text unlockableAmountText;

    MusicManager musicManager;
    CoinsManager coinsManager;
    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        coinsManager = FindObjectOfType<CoinsManager>();

        buyButton.onClick.AddListener(() =>
        {
            musicManager.UnlockMusic(music);
        });

        selectButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.AddSelectedMusic(music);
            musicManager.SelectMusic(music);
        });
    }
    private void Update()
    {
        unlockableAmountText.text = music.unlockableCoins.ToString();
        musicName.text = music.musicName;

        musicManager.MusicStatus(music);

        selectButton.gameObject.SetActive(!music.isSelected && !music.isLocked);
        buyButton.gameObject.SetActive(music.isLocked);
        selectedButton.gameObject.SetActive(music.isSelected);

        buyButton.interactable = coinsManager.IsAvailableCoins(music.unlockableCoins);
    }
}
