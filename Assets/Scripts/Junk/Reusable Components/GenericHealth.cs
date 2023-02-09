using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public IntValueHealth health;

    private void Start()
    {
        health.currentValue = health.maxValue;
    }

    public virtual void Heal(int healAmount)
    {
        health.currentValue += healAmount;
        if (health.currentValue > health.maxValue)
        {
            health.currentValue = health.maxValue;
        }
    }

    public virtual void Damage(int damageAmount)
    {
        health.currentValue -= damageAmount;
        if (health.currentValue < 0)
        {
            health.currentValue = 0;
        }
    }
}
