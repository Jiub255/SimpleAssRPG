using UnityEngine;

public class PhysicalPowerupItem : MonoBehaviour
{
    [Header("Change Index On Each Instance")]
    [SerializeField] private int powerupIndex;
    [SerializeField] private PowerupItem thisPowerup;
    private GameObject player;
    private PlayerHealthManager phm;
    public BoolValueList pickedUpList;

    private void OnEnable()//do onenable instead? for loading other scenes?
    {
        player = GameObject.FindGameObjectWithTag("Player");
        phm = player.GetComponent<PlayerHealthManager>();
        if (pickedUpList.boolList[powerupIndex])
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            if (this.tag == "Heart Powerup")
            {
                if (phm.playerHealth.currentValue < phm.playerHealth.maxValue)
                {
                    PowerupEffect();
                    gameObject.SetActive(false);
                }
            }
            else if (this.tag == "Magic Powerup")
            {
                if (phm.playerMagic.currentValue < phm.playerMagic.maxValue)
                {
                    PowerupEffect();
                    gameObject.SetActive(false);
                }
            }
            else
            {
                PowerupEffect();
                gameObject.SetActive(false);
            }
        }
    }

    void PowerupEffect()
    {
        pickedUpList.boolList[powerupIndex] = true;
        thisPowerup.powerupEvent.Invoke();
    }
}
