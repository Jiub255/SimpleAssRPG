using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{
    public int healAmount = 1;
    public IntValueHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger && playerHealth.currentValue < playerHealth.maxValue)
        {
            playerHealth.currentValue += healAmount;
            if (playerHealth.currentValue > playerHealth.maxValue)
            {
                playerHealth.currentValue = playerHealth.maxValue;
            }
            SignalAndDeactivate();
        }
    }
}
