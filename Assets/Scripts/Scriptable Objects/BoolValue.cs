using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    public bool initialValue;
    public bool RuntimeValue;
    //could i make a list of bools? arbitrary length so it can keep track of all chests open state?
}