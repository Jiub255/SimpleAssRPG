using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    //BASE CLASS FOR ENEMY PROJECTILES

    public static event UnityAction<int, float, Vector3> ProjectileHitPlayer;

    public int damage = 1;
    public float knockback = 1f;
    private Vector3 knockbackDirection;
    public float speed = 1f;
    public float lifetimeLength = 1f;
    private float lifetimeTimer;
    public Rigidbody2D rb;

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

    public void Launch(Vector2 target)//could add orientation like player projectile has
    {
        rb.velocity = target * speed;
        knockbackDirection = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
        {
            ProjectileHitPlayer?.Invoke(damage, knockback, knockbackDirection);
        }
        gameObject.SetActive(false);
    }
}
