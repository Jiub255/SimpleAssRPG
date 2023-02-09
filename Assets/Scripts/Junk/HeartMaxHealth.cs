using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMaxHealth : Powerup
{
    public int healAmount = 1;
    public IntValueHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            playerHealth.maxValue += healAmount;
            playerHealth.currentValue = playerHealth.maxValue;
            SignalAndDeactivate();
        }
    }
}
