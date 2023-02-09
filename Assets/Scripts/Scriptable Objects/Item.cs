using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite sprite;
    [TextArea(3,20)]
    public string itemDescription;
    public bool isKey;
    public bool isArrow;
}
