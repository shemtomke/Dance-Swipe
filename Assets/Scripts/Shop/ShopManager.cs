using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    int currentCoins;

    public int GetCurrentCoins() { return currentCoins; }
    public void UpdateCoins(int coins) { currentCoins = coins; }
    public void Purchase(int amount)
    {
        currentCoins -= amount;
    }
}
