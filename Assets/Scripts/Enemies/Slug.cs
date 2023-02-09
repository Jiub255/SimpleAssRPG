using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : Enemy
{
    private float randomXComponent;
    private float randomYComponent;
    [SerializeField] private Vector2 randomLaunchVector;
    private float randomSizeFactor;
    private int numberOfBubbles = 7;

    private float randomWalkTime;
    private bool randomBool;
    [SerializeField] private float walkTimeOffset = 3f;

    private void Start()
    {
        randomBool = Random.value > 0.5f; // random bool
        randomWalkTime = Random.value + walkTimeOffset; // random value in [walkTimeOffset, walkTimeOffset + 1]
    }

    private void Update()
    {
        randomWalkTime -= Time.deltaTime;
        if (randomWalkTime <= 0f)
        {
            randomBool = Random.value > 0.5f; // random bool
            randomWalkTime = Random.value + walkTimeOffset; // random value in [walkTimeOffset, walkTimeOffset + 1]
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("IsDead") == false && invulnerable == false)
        {
            if (randomBool)
            {
                //walk left
                if (transform.localScale.x == -1)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = 1f;
                    transform.localScale = scale;
                }
                if (randomWalkTime > 0f)
                {
                    rb.MovePosition(rb.position + Vector2.left * moveSpeed * Time.fixedDeltaTime);
                }
            }

            else
            {
                //walk right
                if (transform.localScale.x == 1)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = -1f;
                    transform.localScale = scale;
                }
                if (randomWalkTime > 0f)
                {
                    rb.MovePosition(rb.position + Vector2.right * moveSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("Fireball") || collision.CompareTag("Arrow") || collision.CompareTag("Weapon")) && collision.isTrigger)
        {
            //release poison bubbles. have them in object pool

            for (int i = 0; i < numberOfBubbles; i++)
            {
                randomXComponent = (Random.value * 2) - 1; //to get numbers in [-1,1]
                randomYComponent = (Random.value * 2) - 1;
                randomLaunchVector = new Vector2(randomXComponent, randomYComponent);
                randomSizeFactor = (Random.value * 0.5f) + 0.1f; //to get numbers in [0.1,0.6]
                Vector3 sizer = new Vector3(randomSizeFactor, randomSizeFactor, 0f);

                GameObject Bubble = ObjectPool.SharedInstance.GetPooledObject("Poison Bubble");
                if (Bubble != null)
                {
                    Bubble.transform.position = transform.position;
                    Bubble.transform.rotation = transform.rotation;
                    Bubble.transform.localScale = sizer;
                    Bubble.SetActive(true);
                }

                Bubble.GetComponent<PoisonBubble>().Launch(randomLaunchVector);//not working, why?
            }
        }
    }
}
