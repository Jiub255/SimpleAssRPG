using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Powerup
{
    public PlayerInventory playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            playerInventory.key.numberHeld++;
            SignalAndDeactivate();
        }
    }
}