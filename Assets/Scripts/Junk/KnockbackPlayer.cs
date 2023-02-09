using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackPlayer : MonoBehaviour
{

    private bool knockbackActive = false;
    [SerializeField] private float knockbackTimerLength = 0.5f;
    private float knockbackTimer = -1f;
    [SerializeField] private float knockbackDistance = 1f;


    private void Update()
    {
        if (knockbackActive)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                knockbackActive = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && knockbackTimer <= 0)
        {
            Vector3 difference = transform.position - collision.transform.position;
            difference.Normalize();
            transform.position += difference * knockbackDistance; //could add player knockback resistance eventually

            knockbackActive = true;
            knockbackTimer = knockbackTimerLength;
        }
    }
}
