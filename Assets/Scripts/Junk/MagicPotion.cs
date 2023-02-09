using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPotion : Powerup
{
    public int magicHealAmount = 1;
    public IntValueHealth playerMagic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerMagic.currentValue < playerMagic.maxValue && collision.isTrigger)
        {
            playerMagic.currentValue += magicHealAmount;
            if (playerMagic.currentValue > playerMagic.maxValue)
            {
                playerMagic.currentValue = playerMagic.maxValue;
            }
            SignalAndDeactivate();
        }
    }
}