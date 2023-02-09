using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericKnockback : MonoBehaviour
{
    [SerializeField] private float knockback;
    [SerializeField] private float knockbackTime;
    [SerializeField] string targetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag) && collision.isTrigger)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 difference = rb.transform.position = transform.position;
                difference = difference.normalized * knockback;
                rb.AddForce(difference, ForceMode2D.Impulse);
            }
        }
    }


}
