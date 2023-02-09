using UnityEngine;
using UnityEngine.Events;

public class HurtEnemy : MonoBehaviour
{
    //separate knockback script?
    //BASE WEAPON CLASS
    //can put one on each weapon

    public static event UnityAction<int, int, float, Vector3> hitEnemy;

    public int weaponDamage = 1;
    public float knockbackForce = 10f;
    [HideInInspector] public Vector3 knockbackDirection;

    private Transform player;

    private void Awake()
    {
        player = GetComponentInParent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
        {
            int enemyID = collision.gameObject.GetInstanceID();
            knockbackDirection = collision.transform.position - player.position;
            hitEnemy?.Invoke(enemyID, weaponDamage, knockbackForce, knockbackDirection);
        }

        if (collision.gameObject.CompareTag("Breakable"))
        {
            collision.GetComponent<Jar>().Smash();
        }
    }
}
