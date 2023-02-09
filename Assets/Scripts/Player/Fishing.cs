using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fishing : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventoryItem fishingPole;
    public bool fishing;
    public Animator animator;
    private float randomFishingOddsValue;
    public float fishingSuccessChance;
    public List<InventoryItem> catchableItems = new List<InventoryItem>();
    private int randomItemIndex;
    public Signal raiseItem;
    private InventoryItem caughtItem;
    public GameObject angerBubble;
    public Signal dontMove;
    public Signal dontAttack;
    public bool disableControls;
    public float raycastLength = 1f;
    public Rigidbody2D rb;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerInventory.CheckForItem(fishingPole) && !animator.GetBool("IsFishing") && !disableControls)// && bait?
        {
            //check for water
            if (CheckForWater())
            {
                //go fishing
                rb.velocity = Vector2.zero;
                animator.SetBool("IsFishing", true);
                disableControls = true;
                dontAttack.Raise();
                dontMove.Raise();
            }
        }
    }

    public bool RaycastHitWater(Vector2 direction)
    {
        // Cast a ray
        LayerMask mask = LayerMask.GetMask("Water");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastLength, mask);

        // If it hits something...
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GoFish()
    {
        //random chance to catch fish (just for now, better minigame eventually)
        randomFishingOddsValue = Random.value;
        if (randomFishingOddsValue < fishingSuccessChance)
        {
            //choose a fish or whatever item randomly
            randomItemIndex = Random.Range(0, catchableItems.Count);
            caughtItem = catchableItems[randomItemIndex];

            AddItemToInventory(caughtItem);
            playerInventory.currentItem = caughtItem;
            animator.SetBool("IsFishing", false);
            raiseItem.Raise();
            StartCoroutine(StopRaiseItem());
        }
        else
        {
            animator.SetBool("IsFishing", false);
            StartCoroutine(AngerBubble());
        }
    }

    private IEnumerator StopRaiseItem()
    {
        yield return new WaitForSeconds(1f);
        raiseItem.Raise();
        dontAttack.Raise();
        dontMove.Raise();
        disableControls = false;
    }

    public IEnumerator AngerBubble()
    {
        angerBubble.SetActive(true);
        yield return new WaitForSeconds(1f);
        angerBubble.SetActive(false);
        dontAttack.Raise();
        dontMove.Raise();
        disableControls = false;
    }

    void AddItemToInventory(InventoryItem thisItem)
    {
        if (playerInventory && thisItem)
        {
            if (playerInventory.inventoryList.Contains(thisItem))
            {
                thisItem.numberHeld ++;
            }
            else
            {
                playerInventory.inventoryList.Add(thisItem);
                thisItem.numberHeld = 1;
            }
        }
    }
    
    public bool CheckForWater()
    {
        //check the tile directly in front of me
        //return true if its water

        //get animation state for facing direction
        if (animator.GetFloat("Spd") >= 0.1f)
        {
            if (animator.GetFloat("Vert") == 0)//maybe have a "facing" enum (int) in the animator to make things like this easier?
            {
                if (animator.GetFloat("Horiz") == 1) // moving right
                {
                    return RaycastHitWater(Vector2.right);
                }
                if (animator.GetFloat("Horiz") == -1) // moving left
                {
                    return RaycastHitWater(Vector2.left);
                }
            }
            if (animator.GetFloat("Horiz") == 0)
            {
                if (animator.GetFloat("Vert") == 1) // moving up
                {
                    return RaycastHitWater(Vector2.up);
                }
                if (animator.GetFloat("Vert") == -1) // moving down
                {
                    return RaycastHitWater(Vector2.down);
                }
            }
            else //moving diagonally (not working)
            {
                if (animator.GetFloat("Horiz") > 0.2f) //moving diagonally right
                {
                    return RaycastHitWater(Vector2.right);
                }
                if (animator.GetFloat("Horiz") < -0.2f) //moving diagonally left
                {
                    return RaycastHitWater(Vector2.left);
                }
            }
        }
        else if (animator.GetFloat("Spd") < 0.1f)
        {
            if (animator.GetFloat("LastVert") == 0)
            {
                if(animator.GetFloat("LastHoriz") == 1) // facing right
                {
                    return RaycastHitWater(Vector2.right);
                }
                if (animator.GetFloat("LastHoriz") == -1) // facing left
                {
                    return RaycastHitWater(Vector2.left);
                }
            }
            if (animator.GetFloat("LastHoriz") == 0)
            {
                if (animator.GetFloat("LastVert") == 1) // facing up
                {
                    return RaycastHitWater(Vector2.up);
                }
                if (animator.GetFloat("LastVert") == -1) // facing down
                {
                    return RaycastHitWater(Vector2.down);
                }
            }
            return false;
        }
        return false;
    }
}
