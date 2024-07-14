using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    CoinsManager coinsManager;
    private void Start()
    {
        coinsManager = FindObjectOfType<CoinsManager>();
    }
}
