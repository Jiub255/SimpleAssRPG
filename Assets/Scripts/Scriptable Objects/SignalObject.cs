using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalObject : ScriptableObject
{

    public List<SignalListenerObject> listeners = new List<SignalListenerObject>();

    public void RaiseSignalObject(object whatever)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaisedObject(whatever);
        }
    }

    public void RegisterListenerObject(SignalListenerObject listener)
    {
        listeners.Add(listener);
    }

    public void DeregisterListenerObject(SignalListenerObject listener)
    {
        listeners.Remove(listener);
    }

}
