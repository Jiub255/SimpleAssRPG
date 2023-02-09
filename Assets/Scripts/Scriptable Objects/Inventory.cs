using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>(); //inventory list
    public IntValue numberOfKeys;
    public IntValue numberOfCoins;
    public IntValue numberOfArrows;

    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isKey)
        {
            numberOfKeys.RuntimeValue++;
        }
        else if (itemToAdd.isArrow)
        {
            numberOfArrows.RuntimeValue++;
        }
        else
        {
            if (!items.Contains(itemToAdd))// what if i want multiple of the same item?
            {
                items.Add(itemToAdd);
            }
        }
    }

    public bool CheckForItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }
        return false;
    }
}
