using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListenerHealth : MonoBehaviour
{

    public SignalHealth signalHealthInit;
    public SignalHealth signalHealth;
    public UnityEvent<int, int> signalEventHealth;
    public UnityEvent<int, int> signalEventHealthInit;
    //public UnityAction<string> enemyNamer;

    public void OnSignalRaisedHealthInit(int curHp, int maxHp)
    {
        signalEventHealthInit.Invoke(curHp, maxHp);
    }

    public void OnSignalRaisedHealth(int curHp, int maxHp)
    {
        signalEventHealth.Invoke(curHp, maxHp);
    }

    private void OnEnable()
    {
        signalHealth.RegisterListenerHealth(this);
        signalHealthInit.RegisterListenerHealth(this);
    }

    private void OnDisable()
    {
        signalHealth.DeregisterListenerHealth(this);
        signalHealthInit.DeregisterListenerHealth(this);
    }

}
