using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [NonReorderable]
    public List<Music> musicList;
    public GameObject musicPrefabUI;
    public GameObject musicContentUI;
    public int currentSelectedMusic = 0;

    CoinsManager coinsManager;
    private void Start()
    {
        coinsManager = FindObjectOfType<CoinsManager>();

        AudioManager.Instance.selectedMusic.clip = musicList[currentSelectedMusic].audioClip;

        ListAllMusic();
    }
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
        if (coinsManager.GetCurrentCoins() >= music.unlockableCoins)
        {
            coinsManager.DeductCoins(music.unlockableCoins);
            music.isLocked = false;
        }
    }
    public void SelectMusic(Music music)
    {
        currentSelectedMusic = musicList.IndexOf(music);
    }
    public void MusicStatus(Music music)
    {
        if (currentSelectedMusic == musicList.IndexOf(music))
        {
            music.isSelected = true;
        }
        else
        {
            music.isSelected = false;
        }
    }
}
