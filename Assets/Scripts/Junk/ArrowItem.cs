using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowItem : Powerup
{
    public PlayerInventory playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            //playerInventory.numberOfArrows.RuntimeValue++;
            playerInventory.arrow.numberHeld++;
            powerupSignal.Raise();
            gameObject.SetActive(false);
        }
    }
}
