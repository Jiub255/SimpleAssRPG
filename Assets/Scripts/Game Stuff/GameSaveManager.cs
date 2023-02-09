using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : GenericSingletonClass<GameSaveManager>
{
    //could do multiple lists for different types of stuff? inventory, gameworld stuff, stats, etc?
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    // make a blank list that you can run a save method on, like for so's with lists of so's
    //public List<ScriptableObject> varList = new List<ScriptableObject>();

    public int saveGameIndex;
    public StringValue save1Text;
    public StringValue save2Text;
    public StringValue save3Text;
    public StringValue playerName;

    private void OnEnable()
    {
        MenuButton.newGamePushed += NewGame;
        MenuButton.savePushed += SaveScriptables;
        MenuButton.loadPushed += LoadScriptables;
        MenuButton.resetPushed += ResetScriptables;
    }

    public void NewGame(int gameIndex)
    {
        saveGameIndex = gameIndex;
        //ENTER NAME

        LoadScriptables(4);//4 holds the blank save for new games
    }

    public void SaveScriptables(int gameIndex/*, List<ScriptableObject> listOfSOs*/)
    {
        if (gameIndex == 0)
        {
            gameIndex = saveGameIndex;
        }
        Debug.Log(gameIndex);
        for (int i = 0; i < objects.Count; i++)
        {
            //save to 4 once with a blank save, then load 4 for a new game
            FileStream file = File.Create(Application.persistentDataPath + $"/SaveFile{gameIndex}Object{i}.jame");
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
            Debug.Log("Saved Game " + gameIndex + " Object " + i);

            if (gameIndex == 1)
            {
                save1Text.RuntimeValue = "Game 1";
            }

            if (gameIndex == 2)
            {
                save2Text.RuntimeValue = "Game 2";
            }

            if (gameIndex == 3)
            {
                save3Text.RuntimeValue = "Game 3";
            }
        }
        Debug.Log("Saved Game " + gameIndex + " to " + Application.persistentDataPath);
    }

    public void LoadScriptables(int gameIndex)
    {
        if (gameIndex != 4)//to load the blank/newgame save 4, but not change savegameindex to 4
        {
            saveGameIndex = gameIndex;
        }
            
        for (int i = 0; i < objects.Count; i++)
        {

            if (File.Exists(Application.persistentDataPath + $"/SaveFile{gameIndex}Object{i}.jame"))
            {
                FileStream file = File.Open(Application.persistentDataPath + $"/SaveFile{gameIndex}Object{i}.jame", FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
                Debug.Log("File " + gameIndex + i +" loaded");
            }
            else
            {
                Debug.Log("File " + gameIndex + i + " does not exist");
            }
            //else say no save game exists? like a pop up dialog?
        }

        Debug.Log("playing/saving on file " + saveGameIndex);
        Debug.Log("File number " + gameIndex + "loaded");
        SceneManager.LoadScene("House"); //could change this to a saved location eventually
    }

    
    //might get rid of this, new game should do the same or better.
    public void ResetScriptables(int gameIndex)
    {
        Debug.Log("reset disabled for now");
        /*
        for (int i = 0; i < objects.Count; i++)
        {
            if(File.Exists(Application.persistentDataPath + $"/SaveFile{gameIndex}Object{i}.jame"))
            {
                File.Delete(Application.persistentDataPath + $"/SaveFile{gameIndex}Object{i}.jame");
                Debug.Log("File " + gameIndex + i + " deleted");
            }
        }*/
    }
}