using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Powerup")]
[System.Serializable]
public class PowerupItem : ScriptableObject
{
    //public string powerupName;
   // [TextArea(3, 20)]
   // public string powerupDescription; //have a popup when mouse is hovering over?
//public Sprite powerupSprite;
    //dont need name or sprite, never see name and sprite will be attached to gameobject
    
    public UnityEvent powerupEvent;//could get these methods from wherever makes sense, instead of just from powerup child scripts

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            powerupEvent.Invoke();
            Debug.Log(powerupName + "powerup used");
            //somehow set object inactive here?
        }
    }
    */
}
