using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [Header("UI Stuff to Change")]
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private Image equipmentImage;

    [Header("Variables from the Item")]
    public EquipmentItem thisItem;
    public InventoryManager thisManager;

    public TextMeshProUGUI StatText { get; set; }

    public void ItemSetup(EquipmentItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            equipmentImage.sprite = thisItem.itemSprite;
            statText.text = "" + thisItem.StatName + " " + thisItem.Stat;
        }
    }

    public void ClickedOn()
    {
        Debug.Log("clicked on");//not happening?
        if (thisItem)
        { // make button say unequip here?
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, false, thisItem, "");
        }
    }
}