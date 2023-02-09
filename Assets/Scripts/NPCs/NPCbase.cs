using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCbase : Sign
{
    private Animator animator;
    private float movementTimer;
    private float randomMovementLength;
    private float randomXComponent;
    private float randomYComponent;
    public float movementTimerMultiplier = 2f; //gives the range of possible movement timer lengths
    private Vector2 randomMovementVector;
    public float moveSpeed = 1f;
    private Rigidbody2D rb;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetFloat("Speed", moveSpeed);
    }

    protected override void Update() 
    {
        base.Update();
        
        if (movementTimer <= 0)
        {
            randomMovementLength = Random.value * movementTimerMultiplier;
            movementTimer = randomMovementLength;

            randomXComponent = (Random.value * 2) - 1; //to get numbers in [-1,1]
            randomYComponent = (Random.value * 2) - 1;
            randomMovementVector = new Vector2(randomXComponent, randomYComponent);
            randomMovementVector.Normalize();

            ChangeAnim(randomMovementVector);
        }
        else
        {
            movementTimer -= Time.deltaTime;
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + randomMovementVector * moveSpeed * Time.fixedDeltaTime);
    }

    void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                animator.SetFloat("Horiz", 0);
                animator.SetFloat("Vert", 1);
            }
            else if (direction.y < 0)
            {
                animator.SetFloat("Horiz", 0);
                animator.SetFloat("Vert", -1);
            }
        }

        else if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                animator.SetFloat("Horiz", 1);
                animator.SetFloat("Vert", 0);
            }
            else if (direction.x < 0)
            {
                animator.SetFloat("Horiz", -1);
                animator.SetFloat("Vert", 0);
            }
        }
    }
}
