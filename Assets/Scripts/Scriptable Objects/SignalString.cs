using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalString : ScriptableObject
{

    public List<SignalListenerString> listeners = new List<SignalListenerString>();

    public void RaiseSignalString(string dialog)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaisedString(dialog);
        }
    }

    public void RegisterListenerString(SignalListenerString listener)
    {
        listeners.Add(listener);
    }

    public void DeregisterListenerString(SignalListenerString listener)
    {
        listeners.Remove(listener);
    }

}
