using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Armor,
    Weapon,
    Shield,
    Boots
}

public enum StatName
{
    Attack,
    Defense,
    Speed
}

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory/Equipment Items")]
[System.Serializable]
public class EquipmentItem : InventoryItem
{
    [SerializeField] EquipmentType equipmentType;
    [SerializeField] StatName statName;
    [SerializeField] int stat;
    bool isEquipped;

    public EquipmentType EquipmentType { get; set; }
    public StatName StatName { get; set; }
    public int Stat { get; set; }
    public bool IsEquipped { get; set; }
}