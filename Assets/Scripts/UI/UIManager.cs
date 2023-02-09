using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public IntValueHealth playerHealth;
    public IntValueHealth playerMagic;
    public Slider playerHealthBar;
    public Slider playerMagicBar;
    public Slider invHealthBar;
    public Slider invMagicBar;
    public Text playerHealthText;
    public Text playerMagicText;
    public Text invHealthText;
    public Text invMagicText;
    //public Text playerName; //eventually have this show whatever you name yourself in game

    public Slider enemyHealthBar;
    public Text enemyHealthText;
    public Text enemyNameText;
    private float healthBarTimer;
    [SerializeField] private float healthBarTimerLength = 2f;

    public PlayerInventory playerInventory;
    //need next 3? can just read/write from player inv right?
    public InventoryItem coin;
    public InventoryItem arrow;
    public InventoryItem key;

    public Text coinText;
    public Text arrowText;
    public Text keyText;
    public Text invCoinText;
    public Text invArrowText;
    public Text invKeyText;
    public GameObject dialogBox;
    public Text dialogText;

    //Hopefully temporary coin UI fix, need for arrows and keys too. or could just do a total update signal?
    public Signal updateUISignal;

    private void Start()
    {
        UpdateHealthBar();
        UpdateMagicBar();
        UpdateCoins();
        UpdateArrows();
        UpdateKeys();
    }

    private void OnEnable()
    {
        Enemy.onGetHit += UpdateEnemyHealthBar;
        Sign.signalEventString += UpdateDialog;
        Door.signalEventString += UpdateDialog;
        TreasureChest.signalEventString += UpdateDialog;
    }

    void Update()
    {
        if (healthBarTimer < healthBarTimerLength)
        {
            healthBarTimer -= Time.deltaTime;

            if (healthBarTimer <= 0)
            {
                healthBarTimer = healthBarTimerLength;
                enemyHealthBar.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateHealthBar()
    {
        playerHealthBar.maxValue = playerHealth.maxValue;
        playerHealthBar.value = playerHealth.currentValue;
        playerHealthText.text = playerHealth.currentValue + " / " + playerHealth.maxValue;

        invHealthBar.maxValue = playerHealth.maxValue;
        invHealthBar.value = playerHealth.currentValue;
        invHealthText.text = playerHealth.currentValue + " / " + playerHealth.maxValue;
    }

    public void UpdateMagicBar()
    {
        playerMagicBar.maxValue = playerMagic.maxValue;
        playerMagicBar.value = playerMagic.currentValue;
        playerMagicText.text = playerMagic.currentValue + " / " + playerMagic.maxValue;

        invMagicBar.maxValue = playerMagic.maxValue;
        invMagicBar.value = playerMagic.currentValue;
        invMagicText.text = playerMagic.currentValue + " / " + playerMagic.maxValue;
    }

    public void UpdateEnemyHealthBar(string enemyName, int currentHealth, int maxHealth)
    {
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = currentHealth;
        enemyHealthText.text = currentHealth
            + " / " + maxHealth;
        enemyNameText.text = enemyName;

        enemyHealthBar.gameObject.SetActive(true);
        healthBarTimer = healthBarTimerLength - 0.1f;
    }

    public void UpdateCoins()
    {
        coin.numberHeld = playerInventory.coin.numberHeld;
        coinText.text = "" + coin.numberHeld;
        invCoinText.text = "" + coin.numberHeld;
    }

    public void UpdateArrows()
    {
        arrow.numberHeld = playerInventory.arrow.numberHeld;
        arrowText.text = "" + arrow.numberHeld;
        invArrowText.text = "" + arrow.numberHeld;
    }

    public void UpdateKeys()
    {
        key.numberHeld = playerInventory.key.numberHeld;
        keyText.text = "" + key.numberHeld;
        invKeyText.text = "" + key.numberHeld;
    }

    public void UpdateDialog(string dialog)
    {
        dialogText.text = dialog;
        dialogBox.SetActive(!dialogBox.activeInHierarchy);
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }

        else if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    public void RestoreMagic(int restoreAmount)
    {
        if (playerMagic.currentValue < playerMagic.maxValue - restoreAmount)
        {
            playerMagic.currentValue += restoreAmount;
        }

        else
        {
            playerMagic.currentValue = playerMagic.maxValue;
        }

        updateUISignal.Raise();
    }

    public void RestoreHealth(int restoreAmount)
    {
        if (playerHealth.currentValue < playerHealth.maxValue - restoreAmount)
        {
            playerHealth.currentValue += restoreAmount;
        }

        else
        {
            playerHealth.currentValue = playerHealth.maxValue;
        }

        updateUISignal.Raise();
    }

    public void GetCoin(int coinsGained)
    {
        playerInventory.coin.numberHeld += coinsGained;
        coin.numberHeld = playerInventory.coin.numberHeld;
        updateUISignal.Raise();
    }

    public void GetArrow(int arrowsGained)
    {
        playerInventory.arrow.numberHeld += arrowsGained;
        updateUISignal.Raise();
    }

    public void GetKey(int keysGained)
    {
        playerInventory.key.numberHeld += keysGained;
        updateUISignal.Raise();
    }

    public void IncreaseMaxHealth(int healthIncreaseAmount)
    {
        playerHealth.maxValue += healthIncreaseAmount;
        playerHealth.currentValue = playerHealth.maxValue;//unless i do an "above max health" thing later (probably wont)
        updateUISignal.Raise();
    }

    private void OnDisable()
    {
        Enemy.onGetHit -= UpdateEnemyHealthBar;
        Sign.signalEventString -= UpdateDialog;
        Door.signalEventString -= UpdateDialog; 
        TreasureChest.signalEventString -= UpdateDialog;
    }
}
