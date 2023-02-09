using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
[System.Serializable]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    [TextArea(3, 20)]
    public string itemDescription;
    public Sprite itemSprite;
    public int numberHeld;
    public bool usable;
    public bool isUnique;
    public bool isKey;
    public bool isArrow;
    public bool isCoin;
    public bool isEquipment;
    public UnityEvent thisEvent;
    // make an equip method on the playerEquipment SO that can be called from the use/equip button

    public void Use()
    {
        if (numberHeld > 0)
        {
            thisEvent.Invoke();
            Debug.Log("Used " + itemName);
        }
    }

    public void DecreaseAmount(int decreaseAmount) // gets called by thisEvent for whichever item needs to be decreased
    {
        numberHeld -= decreaseAmount;
        if (numberHeld < 0)
        {
            numberHeld = 0;
        }
    }
}