using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerProjectile : MonoBehaviour
{
    //BASE CLASS FOR PLAYER PROJECTILES

    public static event UnityAction<int, int, float, Vector3> hitEnemy;

    public int weaponDamage = 1;
    public float knockbackDistance = 2f;
    private Vector3 knockbackDirection;
    public float speed = 8f;
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

    public void Setup(Vector2 direction, Vector3 orientation)
    {
        rb.velocity = direction.normalized * speed;
        transform.rotation = Quaternion.Euler(orientation);
        knockbackDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
        {
            int enemyID = collision.gameObject.GetInstanceID();
            // knockbackDirection = collision.transform.position - transform.position;
            hitEnemy?.Invoke(enemyID, weaponDamage, knockbackDistance, knockbackDirection);
        }

        if (collision.gameObject.CompareTag("Breakable"))
        {
            collision.GetComponent<Jar>().Smash();
        }
        gameObject.SetActive(false);
    }
}
