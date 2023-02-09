using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class IntValueList : ScriptableObject
{
    public List<int> intList = new List<int>();

    public void ChangeListEntry(int index, int value)
    {
        intList[index] = value;
    }
}