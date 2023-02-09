using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class StatDictionary : ScriptableObject
{
    //have a StatManager to deal with stats/leveling?
    //it could have all the base stats, then derived stats like health will just be calculated in their own scripts
    public Dictionary<string, int> intDict = new Dictionary<string, int>();

    public void ChangeListEntry(string key, int value)
    {
        intDict[key] = value;
    }

    private void Awake()
    {
        //set up stats here?
        intDict.Add("Strength", 3);
        intDict.Add("Defense", 3);//???
    }
}