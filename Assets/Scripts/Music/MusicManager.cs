using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [NonReorderable]
    public List<Music> musicList;
    public GameObject musicPrefabUI;
    public GameObject musicContentUI;
    public Text musicDurationText;
    public int currentSelectedMusic = 0;

    private Coroutine musicTimerCoroutine;

    int unlockedMusic;

    MusicType currentMusicType;

    CoinsManager coinsManager;
    private void Start()
    {
        coinsManager = FindObjectOfType<CoinsManager>();

        // Select First Unlocked Music
        AudioManager.Instance.selectedMusic.clip = musicList[currentSelectedMusic].audioClip;
        SetMusicType(musicList[currentSelectedMusic].musicType);

        // Generate list of all music
        ListAllMusic();

        unlockedMusic = SaveLoad.Instance.LoadInt(SaveLoad.Instance.GetUnlockedMusicKey());
    }
    public MusicType GetMusicType() { return currentMusicType; }
    public void SetMusicType(MusicType type) { currentMusicType = type; }
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

            unlockedMusic++;
            SaveLoad.Instance.SaveInt(SaveLoad.Instance.GetUnlockedMusicKey(), unlockedMusic);
        }
    }
    public void SelectMusic(Music music)
    {
        currentSelectedMusic = musicList.IndexOf(music);
        SetMusicType(music.musicType);
        StartMusicTimer();
    }
    public void StartMusicTimer()
    {
        if (musicTimerCoroutine != null)
        {
            StopCoroutine(musicTimerCoroutine);
        }

        musicTimerCoroutine = StartCoroutine(UpdateMusicTimer());
    }
    IEnumerator UpdateMusicTimer()
    {
        float duration = AudioManager.Instance.selectedMusic.clip.length;
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            musicDurationText.text = FormatTime(remainingTime);
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        musicDurationText.text = "00:00";
    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
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
