using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Stuff to Change")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the Item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public TextMeshProUGUI ItemNumberText { get; set; }

    public void ItemSetup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemSprite;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void ClickedOn() // clicking on inventory slot calls this method
    {
        Debug.Log("clicked on");//not happening?
        if (thisItem)
        {
            if (thisItem.isEquipment)
                thisManager.SetupDescriptionAndButton(thisItem.itemDescription, true, thisItem, "Equip");
            else
                thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem, "Use Item");
        }
    }
}