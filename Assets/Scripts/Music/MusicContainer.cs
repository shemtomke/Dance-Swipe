using System;
using System.Collections;
using System.Collections.Generic;
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
    private void Start()
    {
        buyButton.onClick.AddListener(() =>
        {
            ShopManager.Instance.UnlockMusic(music);
        });

        selectButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.AddSelectedMusic(music);
            ShopManager.Instance.SelectMusic(music);
        });
    }
    private void Update()
    {
        unlockableAmountText.text = music.unlockableCoins.ToString();
        musicName.text = music.musicName;

        ShopManager.Instance.MusicStatus(music);

        selectButton.gameObject.SetActive(!music.isSelected && !music.isLocked);
        buyButton.gameObject.SetActive(music.isLocked);
        selectedButton.gameObject.SetActive(music.isSelected);
    }
}
