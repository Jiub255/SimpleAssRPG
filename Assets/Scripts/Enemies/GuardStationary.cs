using UnityEngine;

public class GuardStationary : Enemy
{
    // BASE CLASS FOR STATIONARY GUARD MOVEMENT/CHASE

    [HideInInspector] public Transform player;
    public float chaseRadius = 4f;
    [HideInInspector] public Vector3 homePosition;
    [HideInInspector] public Vector3 home;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        homePosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("IsDead") == false && invulnerable == false)
        {
            CheckDistance();
        }
    }

    void CheckDistance()
    {
        if (Vector3.Distance(player.position, transform.position) <= chaseRadius)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            animator.SetFloat("Speed", moveSpeed);//kinda weird? could do a bool of isMoving?
            ChangeAnim(temp - transform.position);
            rb.MovePosition(temp);
        }

        else if (Vector3.Distance(player.position, transform.position) > chaseRadius &&
            Vector3.Distance(transform.position, homePosition) > .3f)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
            animator.SetFloat("Speed", moveSpeed);
            ChangeAnim(temp - transform.position);
            rb.MovePosition(temp);
        }

        else
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Horiz", 0);
            animator.SetFloat("Vert", -1);
        }
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
