using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class IntValueHealth : ScriptableObject
{
    public int maxValue;// why all the null reference exceptions?
    public int currentValue;
}
