using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public bool dontMove;
    private Vector2 movement;
    public float speed = 5f;
    public float atkSpdMultiplier = 0f;

    public PlayerInventory playerInventory;
    public SpriteRenderer recievedItemSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!dontMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Spd", movement.sqrMagnitude);

            movement.Normalize();

            animator.SetFloat("Horiz", movement.x);
            animator.SetFloat("Vert", movement.y);

            if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
            { 
                animator.SetFloat("LastHoriz", movement.x);
                animator.SetFloat("LastVert", movement.y);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!dontMove)
        {
            if (animator.GetBool("IsAttacking") == true)//or IsShooting?
            {
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime * atkSpdMultiplier);
            }
            else
            {
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            }
        }
    }
    
    public void ToggleDontMove()
    {
        dontMove = !dontMove;
    }

    public void RaiseItem()
    {
        if (animator.GetBool("GotItem") == false)
        {
            animator.SetBool("GotItem", true);
            recievedItemSprite.sprite = playerInventory.currentItem.itemSprite;//???
        }
        else
        {
            animator.SetBool("GotItem", false);
            recievedItemSprite.sprite = null;
        }
    }
}
