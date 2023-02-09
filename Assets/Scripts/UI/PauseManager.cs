using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private static bool isPaused;
    public GameObject mainUI;
    public GameObject inventoryUI;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    private bool pauseMenuOpen;
    private bool inventoryOpen;
    //public UIManager uim;

    void Start()
    {
       // uim = GetComponentInChildren<UIManager>();
        isPaused = false; //keep this for scene changes etc
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void TogglePause()
    {
        if (!inventoryOpen)
        {
            isPaused = !isPaused;
            pauseMenuOpen = !pauseMenuOpen;

            if (isPaused)
            {
                pausePanel.SetActive(true);
                mainUI.SetActive(false);
                Time.timeScale = 0f; //change this later, timescale change bad?
            }
            else
            {
                pausePanel.SetActive(false);
                mainUI.SetActive(true);
                Time.timeScale = 1f;
            }
        }
        else //use here to escape out of any menu
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!pauseMenuOpen)
        {
            isPaused = !isPaused;
            inventoryOpen = !inventoryOpen;

            if (isPaused)
            {
                inventoryPanel.SetActive(true);
                inventoryUI.SetActive(true);
                mainUI.SetActive(false);
                Time.timeScale = 0f;
            }
            else
            {
                inventoryPanel.SetActive(false);
                inventoryUI.SetActive(false);
                mainUI.SetActive(true);
                Time.timeScale = 1f;
              //  uim.UpdateMagicBar();//fixed UI not updating from inv potions
              //  uim.UpdateHealthBar();
            }
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }
}
