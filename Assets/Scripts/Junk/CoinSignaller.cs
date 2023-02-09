using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSignaller : MonoBehaviour
{
    public Signal coinSignal;

    public void SignalAndDeactivate()
    {
        coinSignal.Raise();
    }

}
