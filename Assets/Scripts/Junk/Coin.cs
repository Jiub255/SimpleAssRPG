using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    public PlayerInventory playerInventory;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            playerInventory.coin.numberHeld++;
            SignalAndDeactivate();
        }
    }

    public void CoinPowerup()
    {
        playerInventory.coin.numberHeld++;
        SignalAndDeactivate();
    }
}