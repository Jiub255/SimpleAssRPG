using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Powerup : MonoBehaviour
{
    /*
    public string powerupName;
    public Sprite powerupSprite;
    public UnityEvent powerupEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            powerupEvent.Invoke();
            Debug.Log(powerupName + "powerup used");
            gameObject.SetActive(false);
        }
    }
    */

    
    public Signal powerupSignal;

    public void SignalAndDeactivate()
    {
        powerupSignal.Raise();
        gameObject.SetActive(false);
    }
    
}