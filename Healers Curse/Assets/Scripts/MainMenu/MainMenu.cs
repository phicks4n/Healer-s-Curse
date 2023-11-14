using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start() 
    {
        if (!DataPersistenceManager.instance.HasGameData()) 
        {
            continueGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked() 
    {
        DisableMenuButtons();
        // create a new game - which will initialize our game data
        DataPersistenceManager.instance.NewGame();
        // load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync(0);
    }

    public void OnContinueGameClicked() 
    {
        DisableMenuButtons();
        // load the next scene - which will in turn load the game because of 
        // OnSceneLoaded() in the DataPersistenceManager
        // Load the game data using the existing LoadGame function in DataPersistenceManager
        DataPersistenceManager.instance.LoadGame();

        // Retrieve gameData
        GameData savedData = DataPersistenceManager.instance.GetGameData();

        // Check if sceneIndex is valid before loading
        if (savedData != null && savedData.sceneIndex >= 0 && savedData.sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the saved scene
            savedData.initialSceneIndex = savedData.sceneIndex;

            SceneManager.LoadSceneAsync(savedData.sceneIndex);
        }
        else
        {
            // If the saved data is invalid, load the default scene
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void DisableMenuButtons() 
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
