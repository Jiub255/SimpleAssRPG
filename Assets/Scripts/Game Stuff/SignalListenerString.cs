using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListenerString : MonoBehaviour
{
    public SignalString signalString;
    public UnityEvent<string> signalEventString;

    public void OnSignalRaisedString(string dialog)
    {
        signalEventString.Invoke(dialog);
    }

    private void OnEnable()
    {
        signalString.RegisterListenerString(this);
    }

    private void OnDisable()
    {
        signalString.DeregisterListenerString(this);
    }
}
