using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    //might redo whole knockback system to use addforce, more like the tutorial maybe?
    private bool knockbackActive = false;
    [SerializeField] private float knockbackTimerLength = 0.5f;
    private float knockbackTimer = -1f;
    [SerializeField] private float knockbackResistancePercent = 0.5f;
    private float knockbackAmount;

    //[HideInInspector] public Enemy enemy;
    [HideInInspector] public HurtEnemy hurtEnemy;

    private void Start()
    {
        //enemy = GetComponent<Enemy>();
        //hurtEnemy = GetComponent<HurtEnemy>();
        hurtEnemy = FindObjectOfType<HurtEnemy>();
        //enemy.currentState = EnemyState.idle;
    }

    private void Update()
    {
        if (knockbackActive)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                knockbackActive = false;
                //enemy.currentState = EnemyState.idle;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//why trigger here? should be a thing that can be called from hurtenemy, with the damage
    {
        if (collision.tag == "Weapon" && knockbackTimer <= 0)
        {
            //enemy.currentState = EnemyState.stagger; //want them to be unable to attack in stagger state
            knockbackAmount = hurtEnemy.knockbackForce;

            Vector3 difference = transform.position - collision.transform.position;
            difference.Normalize();
            transform.position += difference * knockbackResistancePercent * knockbackAmount;

            knockbackActive = true;
            knockbackTimer = knockbackTimerLength;
        }
    }
}
