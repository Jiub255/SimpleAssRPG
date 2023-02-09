using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/*public enum EnemyState{idle, walk, attack, stagger}*/

public class Enemy : MonoBehaviour
{
    public static event UnityAction<string, int, int> onGetHit;

    //ENEMY STATS
    public IntValue maxHealth;
    public int currentHealth; //IntValue instead? might be too much to keep track of, one for each enemy
    //or intvaluelist for all enemies current healths? max health list too, or keep them individual?
    public string enemyName = "Enter Enemy Name";
    public float moveSpeed = 1f;

    //INVULNERABILITY FRAMES
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [SerializeField] private Color flashColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private float flashDuration;
    [SerializeField] private int numberOfFlashes;
    public bool invulnerable;
    //private float invTimer;

    //KNOCKBACK
   // public bool knockbackActive = false;
    [SerializeField] private float knockbackResistancePercent = 0.5f; //make it zero for no knockback
    [SerializeField] private float knockInvulnRatio = 0.2f;

    //FLOATING DAMAGE NUMBERS
    private Vector3 textPosition;
    [SerializeField] private float yCorrection = 0.6f; //could be higher for big enemies

    //ANIMATOR FOR DEATH ANIMATION
    //should i move all animator stuff here?
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;

    //LOOT DROP STUFF
    public LootTable thisLoot;

    //STATUS EFFECTS
    public bool poisoned;
    [SerializeField] private Color poisonedColor;

    private void Awake()
    {
        currentHealth = maxHealth.initialValue;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        HurtEnemy.hitEnemy += HrtEnemy;
        PlayerProjectile.hitEnemy += HrtEnemy;
    }

    public void HrtEnemy(int enemyID, int weaponDamage, float knockback, Vector3 knockbackDirection)
        //do this with SO's too. have a blank enemyID SO as well. then send signal instead of this ???
    {
        if (invulnerable == false)
        {
            if(enemyID == gameObject.GetInstanceID())
            {
                currentHealth -= weaponDamage;

                if (currentHealth <= 0)
                {
                    MakeLoot();
                    animator.SetBool("IsDead", true);
                    rb.velocity = Vector2.zero;
                    onGetHit?.Invoke(enemyName, 0, maxHealth.initialValue);
                }

                if (currentHealth > 0)
                {
                    StartCoroutine(FlashCoroutine());//could pass in invuln time, diff for diff enemies
                    onGetHit?.Invoke(enemyName, currentHealth, maxHealth.initialValue);
                }

                FloatingDamageNumbers(weaponDamage);

                knockbackDirection.Normalize();
                StartCoroutine(KnockbackCoroutine(knockbackDirection, knockback));
            }
        }
    }

    private IEnumerator FlashCoroutine()
    {
        invulnerable = true;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration / 2f);
            spriteRenderer.color = regularColor;
            yield return new WaitForSeconds(flashDuration / 2f);
        }

        invulnerable = false;
    }

    private IEnumerator KnockbackCoroutine(Vector3 knockbackDirection, float knockback)
    {
        rb.AddForce(knockbackDirection * knockbackResistancePercent * knockback, ForceMode2D.Impulse);
        yield return new WaitForSeconds(flashDuration * numberOfFlashes * knockInvulnRatio);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(flashDuration * numberOfFlashes * (1 - knockInvulnRatio));
    }

    private void FloatingDamageNumbers(int weaponDamage)
    {
        if (weaponDamage != 0)
        {
            textPosition = new Vector3(transform.position.x, transform.position.y + yCorrection, transform.position.z);
            GameObject clone = ObjectPool.SharedInstance.GetPooledObject("Damage Canvas");
            if (clone != null)
            {
                clone.transform.position = textPosition;
                clone.transform.rotation = transform.rotation;
                clone.SetActive(true);
            }
            clone.GetComponent<FloatingDamageNumbers>().damageNumber = weaponDamage;
        }
    }

    public void StartPoisonCo(int poisonDamage, float poisonDuration)
    {
        StartCoroutine(PoisonDamage(poisonDamage, poisonDuration));
    }

    public IEnumerator PoisonDamage(int poisonDamage, float poisonDuration)
    {
        if (poisoned == false)
        {
            poisoned = true;
            spriteRenderer.color = poisonedColor;
            for (int i = 0; i < poisonDamage; i++)
            {
                currentHealth--;
                FloatingDamageNumbers(1);
                if (currentHealth <= 0)
                {
                    spriteRenderer.color = regularColor;
                    MakeLoot();
                    rb.velocity = Vector2.zero;
                    animator.SetBool("IsDead", true);
                    onGetHit?.Invoke(enemyName, 0, maxHealth.initialValue);
                    yield break;
                }
                onGetHit?.Invoke(enemyName, currentHealth, maxHealth.initialValue);
                yield return new WaitForSeconds(poisonDuration / poisonDamage);
            }
            spriteRenderer.color = regularColor;
            poisoned = false;
        }
    }

    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            GameObject current = thisLoot.LootPowerup();
            if(current != null)
            {
                Instantiate(current, transform.position, Quaternion.identity);
            }
        }
    }

    public void GetDead()
    {
        gameObject.SetActive(false); 
    }

    private void OnDisable()
    {
        HurtEnemy.hitEnemy -= HrtEnemy;
        Arrow.hitEnemy -= HrtEnemy;
    }
}
