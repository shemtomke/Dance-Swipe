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
        currentCoins = 0;
    }
    private void Update()
    {
        coinsText.text = currentCoins.ToString();
    }
    public int GetCurrentCoins() { return currentCoins; }
    public void UpdateCoins(int coins) { currentCoins = coins; }
    public void AddCoins(int coins) { currentCoins += coins; }
    public void DeductCoins(int coins) { currentCoins -= coins; }
    
}
