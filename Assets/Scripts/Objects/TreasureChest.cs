using UnityEngine;
using UnityEngine.Events;

public class TreasureChest : Interactable
{
    public static UnityAction<string> signalEventString;

    [Header("Change These")]
    public int chestIndex;
    public InventoryItem contents;
    public int numberOfItems;

    [Header("Keep These")]
    public BoolValueList chestOpenList;//need to make sure this SO has plenty of list elements in the inspector
    public PlayerInventory playerInventory;
    private SpriteRenderer spriteRenderer;
    public Sprite openChestSprite;
    public Signal raiseItem;
    public Signal dontMove;
    public Signal gotKey;
    public Signal gotArrow;
    public Signal gotCoin;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (chestOpenList.boolList[chestIndex] == true)
        {
            spriteRenderer.sprite = openChestSprite;
            objectActive = false;
            if (this.gameObject.transform.childCount != 0)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange && objectActive)
        {
            dontMove.Raise();

            if (chestOpenList.boolList[chestIndex] == false)
            {
                OpenChest();
            }
            else
            {
                ExitChestDialog();
            }
        }
    }

    public void OpenChest()
    {
        signalEventString?.Invoke(contents.itemDescription); //toggles dialog box active, and sends string through

        AddItemToInventory(contents, numberOfItems);
        playerInventory.currentItem = contents;
        raiseItem.Raise();

        chestOpenList.ChangeListEntry(chestIndex, true);
        spriteRenderer.sprite = openChestSprite;
        questionMark.Raise();
    }

    public void ExitChestDialog()
    {
        signalEventString?.Invoke(contents.itemDescription);
        if (this.gameObject.transform.childCount != 0)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        raiseItem.Raise();
        dontAttack.Raise();
        playerInventory.currentItem = null; //necessary?
        objectActive = false;
    }

    void AddItemToInventory(InventoryItem thisItem, int amount)
    {
        if (playerInventory && thisItem)
        {
            if (thisItem.isKey)
            {
                thisItem.numberHeld += amount;
                gotKey.Raise(); 
            }
            else if (thisItem.isArrow)
            {
                thisItem.numberHeld += amount;
                gotArrow.Raise();
            }
            else if (thisItem.isCoin)
            {
                thisItem.numberHeld += amount;
                gotCoin.Raise();
            }
            else
            {
                if (playerInventory.inventoryList.Contains(thisItem))
                {
                    thisItem.numberHeld += amount;
                }
                else
                {
                    playerInventory.inventoryList.Add(thisItem);
                    thisItem.numberHeld = amount;
                }
            }
        }
    }
}
