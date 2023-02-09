using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameSaveManager saveManager;
    public Text saveGame1; 
    public Text saveGame2;
    public Text saveGame3;
    public StringValue save1Text;
    public StringValue save2Text;
    public StringValue save3Text;

    // public Button newGame1, newGame2, newGame3, continueGame1, continueGame2, continueGame3;

    private void Start()
    {
     //   newGame1.onClick.AddListener(() => ButtonClicked())
    }

    private void OnEnable()
    {
        saveGame1.text = save1Text.RuntimeValue;
        saveGame2.text = save2Text.RuntimeValue;
        saveGame3.text = save3Text.RuntimeValue;
    }

    public void NewGame()
    {
        //go to new game menu
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }
    /*
    public void StartNewGame(int newGameIndex)
    {
        saveManager.saveGameIndex = newGameIndex;
        saveManager.LoadScriptables(4);//4 holds the blank save for new games
        SceneManager.LoadScene("House");
    }*/

    public void Continue()
    {
        //go to continue menu
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
    /*
    public void LoadGame(int loadGameIndex)
    {
        saveManager.saveGameIndex = loadGameIndex;
        saveManager.LoadScriptables(loadGameIndex);
        SceneManager.LoadScene("House"); //could change this to a saved location eventually
    }*/

    public void CancelContinue()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void CancelNewGame()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
    }
}
