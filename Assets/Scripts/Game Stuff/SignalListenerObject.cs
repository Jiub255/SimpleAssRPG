using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListenerObject : MonoBehaviour
{
    public SignalObject signalObject;
    public UnityEvent<object> signalEventObject;

    public void OnSignalRaisedObject(object whatever)
    {
        signalEventObject.Invoke(whatever);
    }

    private void OnEnable()
    {
        signalObject.RegisterListenerObject(this);
    }

    private void OnDisable()
    {
        signalObject.DeregisterListenerObject(this);
    }
}
