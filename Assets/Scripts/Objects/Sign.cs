using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Sign : Interactable
{
    public static UnityAction<string> signalEventString;

    public Signal dontMove;
    [Header("Change This")]
    public StringValue dialog;

    protected virtual void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            questionMark.Raise();
            dontMove.Raise();//might be unnecessary
            signalEventString?.Invoke(dialog.RuntimeValue);
        }
    }

}