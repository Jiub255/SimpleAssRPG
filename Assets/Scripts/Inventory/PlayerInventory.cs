using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
[System.Serializable]
public class PlayerInventory : ScriptableObject
{
    public InventoryItem currentItem;
    public List<InventoryItem> inventoryList = new List<InventoryItem>();
    public InventoryItem key;
    public InventoryItem coin;
    public InventoryItem arrow;

    public bool CheckForItem(InventoryItem item)
    {
        if (inventoryList.Contains(item))
        {
            if (item.numberHeld > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
