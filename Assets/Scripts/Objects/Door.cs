using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    public static UnityAction<string> signalEventString;

    [Header("Door Variables")] //can create "tool tips" too?
    public DoorType thisDoorType;
    public PlayerInventory playerInventory;
    public SpriteRenderer spriteRenderer;
    public Sprite openDoor;
    public BoxCollider2D doorCollider;
    public StringValue dialog;
    public Signal dontMove;

    private void Update()
    {
        if (Input.GetButtonDown("attack") && objectActive)
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                questionMark.Raise();
                if (playerInventory.key.numberHeld > 0)
                {
                    playerInventory.key.numberHeld--;
                    Open();
                }
                else //if you have no keys
                {
                    dontMove.Raise();
                    signalEventString?.Invoke(dialog.RuntimeValue);
                }
            }
        }
    }

    public void Open()//screen goes black twice fast here, not sure why
    {
        spriteRenderer.sprite = openDoor;
        objectActive = false; 
        gameObject.transform.GetChild(0).gameObject.SetActive(true); //could just loadscene house here instead of having a scenetransition object
    }
}
