using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public IntValueHealth playerMagic;

    //do stopattacks at ends of attack animation clips? then animator bool isattacking can act as canattack, get rid of timer section
    public float attackTimerLength = 0.4f;//keep longer than max length of all attack animations 
    private float attackTimer;
    private bool canAttack = true;
    public bool dontAttack;

    private Animator animator;

    public PlayerInventory playerInventory;

    public Signal usedMagic;
    public Signal shotArrow;

    public InventoryItem bow;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                canAttack = true;
            }
        }

        if (canAttack && !dontAttack)//1st for attackTimer, 2nd for interactable item zones
        {
            //MELEE 
            if (Input.GetButtonDown("attack"))
            {
                animator.SetBool("IsAttacking", true);
                canAttack = false;
                attackTimer = attackTimerLength;
            }

            //MAGIC ATTACK
            if (Input.GetButtonDown("secondattack") && playerMagic.currentValue > 0) //or (spell cost - 1) eventually
            {
                //could do different effects (area damage, etc) after a certain time held down. could be cool
                //eventually have an animation here
                MakeMagic();
                playerMagic.currentValue--;// could make this amount vary with different spells eventually
                if (playerMagic.currentValue < 0)
                {
                    playerMagic.currentValue = 0;
                }
                usedMagic.Raise();
                canAttack = false;
                attackTimer = attackTimerLength;
            }

            //BOW AND ARROW
            if (Input.GetButtonDown("thirdattack") && playerInventory.arrow.numberHeld > 0
                && playerInventory.CheckForItem(bow))
            {
                //could do a hold button and release thing for the bow, or attacks in general. have it add strength per time?
                //could do different effects (area damage, etc) after a certain time held down. could be cool
                animator.SetBool("IsShooting", true);
                MakeArrow();
                playerInventory.arrow.numberHeld--;
                if (playerInventory.arrow.numberHeld < 0)
                {
                    playerInventory.arrow.numberHeld = 0;
                }
                shotArrow.Raise();
                canAttack = false;
                attackTimer = attackTimerLength;
            }
        }
    }

    public void StopAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    public void StopShooting()
    {
        animator.SetBool("IsShooting", false);
    }

    public void ToggleAttack()//redo player signal listeners for this
    {
        dontAttack = !dontAttack;
    }

    private void MakeArrow()
    {
        if (animator.GetFloat("Spd") == 0)
        //this part only shoots in four directions
        //could have a short timer that allows for letting go of horizontal and vertical direction button at the same time
        //and keeping a last direction "diagonal" sort of thing
        {
            Vector2 temp = new Vector2(animator.GetFloat("LastHoriz"), animator.GetFloat("LastVert"));
            GameObject arrow = ObjectPool.SharedInstance.GetPooledObject("Arrow");
            if (arrow != null)
            {
                arrow.transform.position = transform.position;
                arrow.transform.rotation = transform.rotation;
                arrow.SetActive(true);
            }
            arrow.GetComponent<Arrow>().Setup(temp, ArrowDirection());
        }
        else //if (animator.GetFloat("Spd") != 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("Horiz"), animator.GetFloat("Vert"));
            GameObject arrow = ObjectPool.SharedInstance.GetPooledObject("Arrow");
            if (arrow != null)
            {
                arrow.transform.position = transform.position;
                arrow.transform.rotation = transform.rotation;
                arrow.SetActive(true);
            }
            arrow.GetComponent<Arrow>().Setup(temp, ArrowDirection());
        }
    }

    Vector3 ArrowDirection()
    {
        if (animator.GetFloat("Spd") == 0)
        {
            float temp = Mathf.Atan2(animator.GetFloat("LastVert"), animator.GetFloat("LastHoriz")) * Mathf.Rad2Deg;
            return new Vector3(0, 0, temp - 90);
        }
        else
        {
            float temp = Mathf.Atan2(animator.GetFloat("Vert"), animator.GetFloat("Horiz")) * Mathf.Rad2Deg;
            return new Vector3(0, 0, temp - 90);
        }
    }

    private void MakeMagic()
    {
        if (animator.GetFloat("Spd") == 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("LastHoriz"), animator.GetFloat("LastVert"));
            GameObject fireball = ObjectPool.SharedInstance.GetPooledObject("Fireball");
            if (fireball != null)
            {
                fireball.transform.position = transform.position;
                fireball.transform.rotation = transform.rotation;
                fireball.SetActive(true);
            }
            fireball.GetComponent<Fireball>().Setup(temp, Vector3.zero);
        }
        else
        {
            Vector2 temp = new Vector2(animator.GetFloat("Horiz"), animator.GetFloat("Vert"));
            GameObject fireball = ObjectPool.SharedInstance.GetPooledObject("Fireball");
            if (fireball != null)
            {
                fireball.transform.position = transform.position;
                fireball.transform.rotation = transform.rotation;
                fireball.SetActive(true);
            }
            fireball.GetComponent<Fireball>().Setup(temp, Vector3.zero);
        }
    }
}