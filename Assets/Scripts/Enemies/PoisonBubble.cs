using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBubble : MonoBehaviour
{
    //inherit from projectile?
    public float speed = 0.7f;
    public float lifetimeLength = 4f;
    private float lifetimeTimer;
    public Rigidbody2D rb;
    [SerializeField] private int poisonDamage;
    [SerializeField] private float poisonDuration;

    private void OnEnable()
    {
        lifetimeTimer = lifetimeLength;
    }

    private void Update()
    {
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Launch(Vector2 target)
    {
        rb.velocity = target * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            //poison player, deactivate object
            //collision.GetComponent<PlayerHealthManager>().poisoned = true;
            collision.GetComponent<PlayerHealthManager>().StartPoisonCo(poisonDamage, poisonDuration);
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Enemy") && collision.isTrigger)
        {
            //poison player, deactivate object
            //collision.GetComponent<Enemy>().poisoned = true;
            collision.GetComponent<Enemy>().StartPoisonCo(poisonDamage, poisonDuration);
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Fireball") || collision.CompareTag("Weapon"))
        {
            gameObject.SetActive(false);
        }
    }
}
