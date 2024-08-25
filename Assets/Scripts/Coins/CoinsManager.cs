using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public Text coinsText;

    int currentCoins;
    private void Start()
    {
        LoadData();
    }
    private void Update()
    {
        coinsText.text = currentCoins.ToString("N0");
    }
    void LoadData()
    {
        var saveManager = SaveLoad.Instance;
        saveManager.LoadInt(saveManager.GetCoinsKey());
    }
    void SaveData()
    {
        var saveManager = SaveLoad.Instance;
        saveManager.SaveInt(saveManager.GetCoinsKey(), currentCoins);
    }
    public int GetCurrentCoins() { return currentCoins; }
    public void UpdateCoins(int coins) { currentCoins = coins; SaveData(); }
    public bool IsAvailableCoins(int coins)
    {
        if (currentCoins >= coins)
            return true;
        return false;
    }
    public void AddCoins(int coins) { currentCoins += coins; SaveData(); }
    public void DeductCoins(int coins) { currentCoins -= coins; SaveData(); }
    
}
