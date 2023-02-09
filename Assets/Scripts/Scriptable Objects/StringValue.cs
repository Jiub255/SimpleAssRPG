using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class StringValue : ScriptableObject
{
    [TextArea(3,20)]
    public string initialValue;
    [TextArea(3,20)]
    public string RuntimeValue;
}