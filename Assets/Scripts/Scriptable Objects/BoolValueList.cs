using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValueList : ScriptableObject
{
    public List<bool> boolList = new List<bool>();

    public void ChangeListEntry(int index, bool value)// seems unnecessary
    {
        boolList[index] = value;
    }
}