using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [Header("Change Index On Each Instance")]
    [SerializeField] private int itemIndex;
    [SerializeField] private InventoryItem thisItem;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private BoolValueList itemsPickedUpList;

    private void OnEnable()
    {
        if (itemsPickedUpList.boolList[itemIndex])
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
        {
            AddItemToInventory();
            gameObject.SetActive(false);
        }
    }

    void AddItemToInventory()
    {
        itemsPickedUpList.boolList[itemIndex] = true;
        if (playerInventory && thisItem)
        {
            if (playerInventory.inventoryList.Contains(thisItem))
            {
                thisItem.numberHeld++;
            }
            else
            {
                playerInventory.inventoryList.Add(thisItem);
            }
        }
    }
}
