using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    // public PlayerInventory equipInventory;// probably not this
    public PlayerEquipment playerEquipment;
    [SerializeField] GameObject equipmentPanel;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItem currentItem;

    private GameObject player;
    private PlayerHealthManager phm;

    //Equipment
    [SerializeField] private GameObject blankEquipmentSlot;
    // how to make these have the same positions for different/changing resolutions?
    // maybe make blank image ui elements to hold the positions, and instantiate equipment slots as their children
    [SerializeField] List<EquipIconPanel> equipIconPanels = new List<EquipIconPanel>();

    //Have keys, arrows, and coins show up in a special counter on the side of UI
    //also have health and magic show on inv screen
    //want these as children of inv panel, not main ui

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        phm = player.GetComponent<PlayerHealthManager>();
        UpdateInventorySlots();
        SetTextAndButton("", false);
        UpdateEquipment();

        PlayerEquipment.onChangedEquipment += UpdateEquipment;
        PlayerEquipment.onChangedEquipment += UpdateInventorySlots;
    }

    private void OnDisable()
    {
        PlayerEquipment.onChangedEquipment -= UpdateEquipment;
        PlayerEquipment.onChangedEquipment -= UpdateInventorySlots;
    }

    public void UpdateInventorySlots()
    {
        ClearInventorySlots();
        MakeInventorySlots();
    }

    void ClearInventorySlots()
    {
        for(int i = 0; i < inventoryContent.transform.childCount; i++)
        {
            Destroy(inventoryContent.transform.GetChild(i).gameObject);//can avoid destroy? have ondisable on inventoryslot script maybe?
        }
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for(int i = (playerInventory.inventoryList.Count - 1); i >= 0; i--)
            {
                if(playerInventory.inventoryList[i].numberHeld > 0)
                {
                    //use object pool instead?
                    GameObject temp = Instantiate(blankInventorySlot,
                        inventoryContent.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryContent.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.ItemSetup(playerInventory.inventoryList[i], this);
                    }
                }
            }
        }
    }

    // so far, only used to reset text to "" and disable button
    public void SetTextAndButton(string description, bool buttonActive) 
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            // set button text to use for items, equip for equipment?
            useButton.SetActive(false);
        }
    }

    public void SetupDescriptionAndButton(string newDescriptionString, bool isButtonUsable, InventoryItem newItem, string buttonText)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
        useButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
    }

    public void UseButtonPressed() // use button in inventory calls this method
    {
        // do equip here?
        if (currentItem)
        {
            if (currentItem.name == "Health Potion")
            {
                if (phm.playerHealth.currentValue < phm.playerHealth.maxValue)
                {
                    ActuallyUse();
                }
                else
                {
                    //else popup "health is full"?
                    Debug.Log("Health full");
                }
            }
            else if (currentItem.name == "Magic Potion")
            {
                if(phm.playerMagic.currentValue < phm.playerMagic.maxValue)
                {
                    ActuallyUse();
                }
            }
            else if (currentItem.name == "Fish Health")
            {
                if (phm.playerHealth.currentValue < phm.playerHealth.maxValue)
                {
                    ActuallyUse();
                }
                else
                {
                    Debug.Log("Health full");
                }
            }
            else if (currentItem.name == "Fish Magic")
            {
                if (phm.playerMagic.currentValue < phm.playerMagic.maxValue)
                {
                    ActuallyUse();
                }
            }
            else
            {
                ActuallyUse();

                // if isEquipment, call Equip method on PlayerEquipment from use (equip) button
                // use InventoryItem instead of EquipmentItem maybe?
                if (currentItem.isEquipment)
                {
                    //playerEquipment.Equip(currentItem);
                }
            }
        }
    }

    public void ActuallyUse()
    {
        // use the item
        currentItem.Use();
        // refresh inv slots
        UpdateInventorySlots();
        // reset description/use button
        if (currentItem.numberHeld == 0)
        {
            SetTextAndButton("", false);
        }
    }

    public void UpdateEquipment()
    {
        ClearEquipmentSlots();
        MakeEquipmentSlots();
    }

    void ClearEquipmentSlots()
    {
        foreach(Transform child in equipmentPanel.transform) // the children are just placeholders
        {
            if (child.childCount > 0)
                Destroy(child.GetChild(0).gameObject);
        }
    }

    void MakeEquipmentSlots()
    {
        foreach(Transform child in equipmentPanel.transform)
        {
            GameObject temp = Instantiate(blankEquipmentSlot, child.transform.position, Quaternion.identity, child);
        }

        foreach(EquipIconPanel equipIconPanel in equipIconPanels)
        {
            print(equipIconPanel.equipmentType);
            // not getting past this if statement, why?
            if (playerEquipment.equipmentDictionary.ContainsKey(equipIconPanel.equipmentType))
            {
                print("equipment type: " + equipIconPanel.equipmentType);
                print("equipment item: " + playerEquipment.equipmentDictionary[equipIconPanel.equipmentType]);

                equipIconPanel.panel.transform.GetChild(0).GetComponent<EquipmentSlot>().ItemSetup(
                    playerEquipment.equipmentDictionary[equipIconPanel.equipmentType], this
                    );
            }
        }
        if (playerEquipment.equipmentDictionary.Count == 0)
            print("equipmentDictionary empty"); 
        foreach (KeyValuePair<EquipmentType, EquipmentItem> kvp in playerEquipment.equipmentDictionary)
        {
            print("equipmentDictionary contains " + kvp);
        }
    }
}

[System.Serializable]
public class EquipIconPanel
{
    public GameObject panel;
    public EquipmentType equipmentType;
} 