using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public AudioSource backgroundMusic;
    public AudioSource selectedMusic;

    private void Start()
    {
        AddSelectedMusic();
        PlaySelectedMusic();
    }
    public void AddSelectedMusic()
    {
        selectedMusic.clip = ShopManager.Instance.musicList[ShopManager.Instance.currentSelectedMusic].audioClip;
    }
    public void PlaySelectedMusic()
    {
        selectedMusic.Play();
    }
    public void PauseBgMusic()
    {
        backgroundMusic.Pause();
    }
}
