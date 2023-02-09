using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{

    //WANT TO COMBINE THIS WITH "ENEMY" SCRIPT


    //public int enemyMaxHealth;
    //public int enemyCurrentHealth;

    private SpriteRenderer enemySprite;

    private bool flashActive = false;
    [SerializeField] private float flashLength = 0.75f;
    private float flashTimer;

    //need to have nothing that i need to drag into the inspector. scriptable objects, like in UI manager
    //public Slider enemyHealthBar;
   // public Text enemyHealthText;
   // public Text enemyNameText;

   // private float healthBarTimer;
   // [SerializeField] private float healthBarTimerLength = 2f;

  //  [HideInInspector] public Enemy enemy;

    public IntValue enemyMaxHealth;
    public IntValue enemyCurrentHealth;
    //public Signal enemyHealthSignal;

    //knockback section

    private bool knockbackActive = false;
    [SerializeField] private float knockbackTimerLength = 0.5f;
    private float knockbackTimer = -1f;
    [SerializeField] private float knockbackResistancePercent = 0.5f;
    //private float knockbackAmount;

    [HideInInspector] public HurtEnemy hurtEnemy;


    void Start()
    {
        // enemy = GetComponent<Enemy>();

        // enemyMaxHealth = enemy.maxHealth.initialValue;
        //   enemyCurrentHealth = enemy.currentHealth;
        enemyCurrentHealth.initialValue = enemyMaxHealth.initialValue;

        enemySprite = GetComponent<SpriteRenderer>();

        hurtEnemy = FindObjectOfType<HurtEnemy>();
    }

    void Update()
    {
       /* if (healthBarTimer < healthBarTimerLength)
        {
            healthBarTimer -= Time.deltaTime;
            enemyNameText.text = name;
            enemyHealthBar.maxValue = enemyMaxHealth.initialValue;
            enemyHealthBar.value = enemyCurrentHealth.initialValue;
            enemyHealthText.text = enemyCurrentHealth.initialValue + " / " + enemyMaxHealth.initialValue;

            if (healthBarTimer <= 0)
            {
                healthBarTimer = healthBarTimerLength;
                enemyHealthBar.gameObject.SetActive(false);
            }
        } */

        if (flashActive)
        {

            if (flashTimer > flashLength * .99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashTimer > flashLength * .91f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashTimer > flashLength * .83f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashTimer > flashLength * .74f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashTimer > flashLength * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashTimer > flashLength * .58f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashTimer > flashLength * .50f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashTimer > flashLength * .41f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashTimer > flashLength * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashTimer > flashLength * .25f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashTimer > flashLength * .17f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashTimer > flashLength * .08f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashTimer > 0)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }
            flashTimer -= Time.deltaTime;

        }

        if (knockbackActive)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                knockbackActive = false;
            }
        }

    }

    public void HurtEnemy(int weaponDamage, float knockback, Vector3 knockbackDirection)
    {
        flashActive = true;
        flashTimer = flashLength;

        enemyCurrentHealth.RuntimeValue -= weaponDamage;

       // enemyHealthSignal.Raise();

        //enemyHealthBar.gameObject.SetActive(true);
        //healthBarTimer = healthBarTimerLength - 0.1f;

        if (enemyCurrentHealth.RuntimeValue <= 0)
        {
            //Destroy(gameObject);
            this.gameObject.SetActive(false);

        }
        /*
        if (enemyCurrentHealth.RuntimeValue > 0)
        {
            enemyHealthSignal.Raise();
        }*/

        //knockback section

        if (knockbackTimer <= 0)
        {

            //knockbackAmount = hurtEnemy.knockbackDistance;

            knockbackDirection.Normalize();
            transform.position += knockbackDirection * knockbackResistancePercent * knockback;

            knockbackActive = true;
            knockbackTimer = knockbackTimerLength;

        }

    }



    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
        }
    }
    */

}
