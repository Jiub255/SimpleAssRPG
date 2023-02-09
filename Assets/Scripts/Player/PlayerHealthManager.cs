using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    //PLAYER STATS
    public IntValueHealth playerHealth;
    public IntValueHealth playerMagic;
    public FloatValue blockChance;

    //INVULNERABILITY FRAMES
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public SpriteRenderer spriteRenderer;
    // public Collider2D triggerCollider;
    public bool invulnerable;

    //DEAD STUFF
    public Rigidbody2D rb;
    private bool isDead = false;
    [SerializeField] private float deathTimerLength = 1f;
    private float deathTimer;

    //KNOCKBACK
    private float knockbackResistancePercent = 0.5f;
    [SerializeField] private float knockInvulnRatio = .2f;

    //SIGNAL STUFF
    public Signal healthSignal;
    public Signal toggleDontMoveSignal;

    //STATUS EFFECTS 
    public bool poisoned;
    public Color poisonedColor;
     
    private void OnEnable()
    {
        HurtPlayer.hitPlayer += HrtPlayer;
        Projectile.ProjectileHitPlayer += HrtPlayer; //do these through SOs too? attach player health SO to enemy script, then send signals?
    }

    void Start()
    {
        playerHealth.currentValue = playerHealth.maxValue; //should this be here?
        deathTimer = deathTimerLength;
    }

    void Update()
    {
        if (isDead == true)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                deathTimer = deathTimerLength;//may not need this line
                playerHealth.currentValue = playerHealth.maxValue;
                SceneManager.LoadScene("SampleScene");//how reset does this line make things?
            }
        }
    }

    public void HrtPlayer(int damage, float knockback, Vector3 knockbackDirection)
    {
        if (invulnerable == false)
        {
            //have block chance in here, (and HrtEnemy)
            //then if (block){damage = 0}
            //floatingdamagenumbers will have if(damage == 0){say "blocked"} instead of damage amount
            //just have shield on sprite whenever equipped, not popup animations for when blocked

            playerHealth.currentValue -= damage;
            healthSignal.Raise();
            FloatingDamageNumbers(damage);

            if (playerHealth.currentValue <= 0)
            {
                playerHealth.currentValue = 0;
                isDead = true;
                rb.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                foreach(Transform child in transform)
                {
                    foreach(Transform grandchild in child)
                    {
                        grandchild.gameObject.SetActive(false);
                    }
                    child.gameObject.SetActive(false);
                }
                gameObject.layer = LayerMask.NameToLayer("No Collide");
            }

            if (playerHealth.currentValue > 0)
            {
                StartCoroutine(FlashCoroutine());
            }

            knockbackDirection.Normalize();
            StartCoroutine(KnockbackCoroutine(knockbackDirection, knockback));
        }
    }

    private IEnumerator FlashCoroutine()
    {
        invulnerable = true;
        toggleDontMoveSignal.Raise();

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration / 2f);
            spriteRenderer.color = regularColor;
            yield return new WaitForSeconds(flashDuration / 2f);
        }

        invulnerable = false;
        toggleDontMoveSignal.Raise();
    }

    private IEnumerator KnockbackCoroutine(Vector3 knockbackDirection, float knockback)
    {
        rb.AddForce(knockbackDirection * knockbackResistancePercent * knockback, ForceMode2D.Impulse);
        yield return new WaitForSeconds(flashDuration * numberOfFlashes * knockInvulnRatio);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(flashDuration * numberOfFlashes * (1 - knockInvulnRatio));
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
                playerHealth.currentValue--;
                healthSignal.Raise();
                FloatingDamageNumbers(1);
                if (playerHealth.currentValue <= 0)
                {
                    spriteRenderer.color = regularColor;
                    playerHealth.currentValue = 0;
                    isDead = true;
                    rb.bodyType = RigidbodyType2D.Static;
                    spriteRenderer.enabled = false;
                    foreach (Transform child in transform)
                    {
                        foreach (Transform grandchild in child)
                        {
                            grandchild.gameObject.SetActive(false);
                        }
                        child.gameObject.SetActive(false);
                    }
                    gameObject.layer = LayerMask.NameToLayer("No Collide");
                    yield break;
                }
                yield return new WaitForSeconds(poisonDuration / poisonDamage);
            }
            spriteRenderer.color = regularColor;
            poisoned = false;
        }
    }

    private void FloatingDamageNumbers(int damage)
    {
        if (damage != 0)
        {
            GameObject clone = ObjectPool.SharedInstance.GetPooledObject("Damage Canvas");
            if (clone != null)
            {
                clone.transform.position = transform.position;
                clone.transform.rotation = transform.rotation;
                clone.SetActive(true);
            }
            clone.GetComponent<FloatingDamageNumbers>().damageNumber = damage;
        }
    }

    private void OnDisable()
    {
        HurtPlayer.hitPlayer -= HrtPlayer;
        Projectile.ProjectileHitPlayer -= HrtPlayer;
    }
}
