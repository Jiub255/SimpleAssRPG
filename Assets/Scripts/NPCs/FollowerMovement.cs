using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform target;
    [SerializeField] private float followRadius = 7f;
    [SerializeField] private float nearRadius = 2f;
    [SerializeField] private float targetSpeed;
    [SerializeField] private bool isNear;
    [SerializeField] private float nearSpeedMultiplier = 0.25f;
    [SerializeField] private float speedMultiplier = 2f;

    private float movementTimer;
    private float randomMovementLength;
    private float randomXComponent;
    private float randomYComponent;
    public float movementTimerMultiplier = 2f; //gives the range of possible movement timer lengths
    private Vector2 randomMovementVector;

    private void Awake()
    {
        targetSpeed = target.GetComponent<PlayerMovement>().speed;
    }

    private void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= nearRadius)
        {
            isNear = true;
        }

        if (Vector3.Distance(target.position, transform.position) > followRadius )
        {
            isNear = false;
        }

        if (isNear == false)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, targetSpeed * speedMultiplier * Time.deltaTime);
            animator.SetFloat("Speed", targetSpeed * speedMultiplier);
            ChangeAnim(temp - transform.position);
            rb.MovePosition(temp);
        }

        if (isNear) // Move randomly until farther apart than the followRadius
        {
            if (movementTimer <= 0)
            {
                randomMovementLength = Random.value * movementTimerMultiplier;
                movementTimer = randomMovementLength;

                randomXComponent = (Random.value * 2) - 1; //to get numbers in [-1,1]
                randomYComponent = (Random.value * 2) - 1;
                randomMovementVector = new Vector2(randomXComponent, randomYComponent);
                randomMovementVector.Normalize();

                //sometimes, dont move
                var rand = Random.value;
                if (rand < .15)
                {
                    randomMovementVector = Vector2.zero;
                    animator.SetFloat("Speed", 0f);
                }
                else
                {
                    animator.SetFloat("Speed", targetSpeed * nearSpeedMultiplier);
                    ChangeAnim(randomMovementVector);
                }
            }
            else
            {
                movementTimer -= Time.deltaTime;
            }

            rb.MovePosition(rb.position + randomMovementVector * targetSpeed * nearSpeedMultiplier * Time.fixedDeltaTime);
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
