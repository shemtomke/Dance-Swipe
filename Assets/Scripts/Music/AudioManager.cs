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
    private void Update()
    {
        
    }
    public void AddSelectedMusic(Music music)
    {
        selectedMusic.clip = music.audioClip;
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
