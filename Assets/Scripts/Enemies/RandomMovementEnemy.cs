using UnityEngine;

public class RandomMovementEnemy : Enemy
{
    // BASE CLASS FOR RANDOM MOVEMENT/CHASE

    [HideInInspector] public Transform player;
    public float chaseRadius = 4f;

    private float movementTimer;
    private float randomMovementLength;
    private float randomXComponent;
    private float randomYComponent;
    private bool isChasing;
    public float movementTimerMultiplier = 2f; //gives the range of possible movement timer lengths
    [SerializeField] private Vector2 randomMovementVector;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (isChasing == false)
        {
            if (movementTimer <= 0)
            {
                randomMovementLength = Random.value * movementTimerMultiplier;
                movementTimer = randomMovementLength;

                randomXComponent = (Random.value * 2) - 1; //to get numbers in [-1,1]
                randomYComponent = (Random.value * 2) - 1;
                randomMovementVector = new Vector2(randomXComponent, randomYComponent);
                randomMovementVector.Normalize();
            }
            else
            {
                movementTimer -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (invulnerable == false)
        {
            CheckDistance();
            //do random movement here
            if (isChasing == false)
            {
                rb.MovePosition(rb.position + randomMovementVector * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void CheckDistance()
    {
        if (Vector3.Distance(player.position, transform.position) <= chaseRadius)
        {
            isChasing = true;
            Vector3 temp = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(temp);
        }

        else if (Vector3.Distance(player.position, transform.position) > chaseRadius)
        {
            if (isChasing)
            {
                isChasing = false;
            }
        }
    }
}