using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private string targetTag;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag) && collision.isTrigger)
        {
            GenericHealth temp = collision.GetComponent<GenericHealth>();
            if (temp)
            {
                temp.Damage(damage);
            }
        }
    }
}

