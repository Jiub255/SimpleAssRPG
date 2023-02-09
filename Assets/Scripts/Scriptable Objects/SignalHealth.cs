using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalHealth : ScriptableObject
{

    public List<SignalListenerHealth> listeners = new List<SignalListenerHealth>();

    public void RaiseSignalHealthInit(int curHp, int maxHp)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaisedHealthInit(curHp, maxHp);
        }
    }

    public void RaiseSignalHealth(int curHp, int maxHp)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaisedHealth(curHp,maxHp);
        }
    }

    public void RegisterListenerHealth(SignalListenerHealth listener)
    {
        listeners.Add(listener);
    }

    public void DeregisterListenerHealth(SignalListenerHealth listener)
    {
        listeners.Remove(listener);
    }

}
