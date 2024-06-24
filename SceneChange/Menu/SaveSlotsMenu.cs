using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    public SaveSlots[] saveSlots;
    [SerializeField] private Button backButton;

    private bool isLoadingGame = false;

    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmationPopupMenu confirmationPopup;

    private void Awake()
    {
        saveSlots = GameObject.Find("Canvas/SaveSlotMenu/Panel").GetComponentsInChildren<SaveSlots>();
    }

    public void OnSaveSlotClicked(SaveSlots saveSlot)
    {
        //disable all buttons
        DisableMenuButtons();

        //case - loading game
        if(isLoadingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileId());
            SaveGameAndLoadScene();
        }
        //case - new game, but the save slot has data
        else if(saveSlot.hasData)
        {
            confirmationPopup.ActivateMenu(
                "Starting a New Game with this slot will override the currently saved data. Are you sure?",
                //function to execute if we select yes
                () =>
                {
                    DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileId());
                    DataPersistenceManager.instance.NewGame();
                    SaveGameAndLoadScene();
                },
                //function to execute if we select cancel
                () =>
                {
                    this.ActivateMenu(isLoadingGame);
                });
        }
        //case - new game, and the save slot has no data
        else
        {
            DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileId());
            DataPersistenceManager.instance.NewGame();
            SaveGameAndLoadScene();
        }
    }

    private void SaveGameAndLoadScene()
    {
        // save the game anytime before loading a new scene
        DataPersistenceManager.instance.SaveGame();

        //load the scene
        if(DataPersistenceManager.instance.gameData.currentScene == null)
        {
            SceneManager.LoadScene("House");
        }
        else
        {
            SceneManager.LoadScene(DataPersistenceManager.instance.gameData.currentScene);
        }
    }

    public void OnClearClicked(SaveSlots saveSlot)
    {
        DisableMenuButtons();

        confirmationPopup.ActivateMenu(
                "Do you want to delete this save file?",
                //function to execute if we select yes
                () =>
                {
                    DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
                    ActivateMenu(isLoadingGame);
                },
                //function to execute if we select cancel
                () =>
                {
                    ActivateMenu(isLoadingGame);
                });
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        //set this menu to be active
        this.gameObject.SetActive(true);

        //set mode
        this.isLoadingGame = isLoadingGame;

        //load all of the profiles that exists
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfileGameData();

        //ensure the back button is enabled when we activate the menu
        backButton.interactable = true;

        //loop through each save slot in the UI and set the content appropriately
        GameObject firstSelected = backButton.gameObject;
        foreach (SaveSlots saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if(profileData == null && isLoadingGame) 
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if(firstSelected.Equals(backButton.gameObject)) 
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }

        //set the first selected button
        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);

    }
    private void DisableMenuButtons()
    {
        foreach(SaveSlots saveSlot in saveSlots) 
        { 
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
