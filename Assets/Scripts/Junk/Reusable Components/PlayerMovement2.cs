using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 5f;
    public float atkSpdMultiplier = 0f;
    private Rigidbody2D rb;
   // private Animator animator;
    private Vector2 movement;
    public bool dontMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
     //   animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dontMove)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
       // animator.SetFloat("Spd", movement.sqrMagnitude);

        movement.Normalize();

      //  animator.SetFloat("Horiz", movement.x);
      //  animator.SetFloat("Vert", movement.y);

      //  if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
      //  {
      //      animator.SetFloat("LastHoriz", movement.x);
      //      animator.SetFloat("LastVert", movement.y);
       // }
    }
    private void FixedUpdate()
    {
       // if (animator.GetBool("IsAttacking") == true)
       // {
       //     rb.MovePosition(rb.position + movement * atkSpdMultiplier * speed * Time.fixedDeltaTime);
       // }
       // else
       // {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
       // }
    }
    public void ToggleDontMove()
    {
        dontMove = !dontMove;
    }
}
