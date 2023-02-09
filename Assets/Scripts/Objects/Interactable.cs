using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Signal dontAttack;
    public Signal questionMark;
    public bool playerInRange;
    public bool objectActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && objectActive && !collision.isTrigger)
        {
            questionMark.Raise();
            dontAttack.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && objectActive && !collision.isTrigger)
        {
            questionMark.Raise();
            dontAttack.Raise();
            playerInRange = false;
        }
    }
}
