using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Player Equipment")]
[System.Serializable]
public class PlayerEquipment : ScriptableObject
{
    public Dictionary<EquipmentType, EquipmentItem> equipmentDictionary = new Dictionary<EquipmentType, EquipmentItem>();

    public List<EquipmentItem> equipmentList = new List<EquipmentItem>();

    public PlayerInventory playerInventory;

    public static event UnityAction onChangedEquipment;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();

        ConvertDictToList();
    }

    void ConvertDictToList()
    {
        equipmentList.Clear();

        foreach (KeyValuePair<EquipmentType, EquipmentItem> kvp in equipmentDictionary)
        {
            Debug.Log(kvp);
            equipmentList.Add(kvp.Value);
        }
    }

    public void Equip(EquipmentItem equipmentItem) // use inventoryItem instead
    {
        equipmentItem.IsEquipped = true;

        if (equipmentDictionary.ContainsKey(equipmentItem.EquipmentType))
        {
            // unequip old
            equipmentDictionary[equipmentItem.EquipmentType].IsEquipped = false;
            // add old back into inv
            playerInventory.inventoryList.Add(equipmentDictionary[equipmentItem.EquipmentType]);
            // equip new
            equipmentDictionary[equipmentItem.EquipmentType] = equipmentItem;
            // remove new from inv
            playerInventory.inventoryList.Remove(equipmentItem);
        }
        else
        {
            // equip new 
            // why is it adding a weapon as a helmet? (helmet is first on enum list)
            equipmentDictionary.Add(equipmentItem.EquipmentType, equipmentItem);
            // remove new from inv
            playerInventory.inventoryList.Remove(equipmentItem);
        }

        // send signal to UI to update
        onChangedEquipment?.Invoke();

        // update list with new equipment changes
        ConvertDictToList();
    }
}