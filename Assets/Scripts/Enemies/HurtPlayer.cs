using UnityEngine;
using UnityEngine.Events;

public class HurtPlayer : MonoBehaviour
{
    public static event UnityAction<int, float, Vector3> hitPlayer;

    public int enemyAttack = 1;
    public float knockbackDistance = 2f;
    [HideInInspector] public Vector3 knockbackDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
        {
            knockbackDirection = collision.transform.position - transform.position;
            hitPlayer?.Invoke(enemyAttack, knockbackDistance, knockbackDirection);
        }
    }
}
