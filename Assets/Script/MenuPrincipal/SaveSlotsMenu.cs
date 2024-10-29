using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SaveSlotsMenu : MonoBehaviour
{
    [Header("Menu Navigation")]

    [SerializeField] private MainMenu mainMenu;

    [Header("Menu Buttons")]

    [SerializeField] private Button backButton;

    private SaveSlot[] saveSlots;

    private void Awake()
    {
        
        this.gameObject.SetActive(false);
        
        saveSlots = GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Prototype3");
    }

    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        Dictionary<string, GameData> profilesGameData= DataPersistenceManager.instance.GetAllProfilesGameData();

        foreach(SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
        }
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach(SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }

    internal void ActivateMenu(bool v)
    {
        throw new NotImplementedException();
    }
}
