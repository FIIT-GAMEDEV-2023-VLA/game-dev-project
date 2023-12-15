using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Alica


public class SceneManagerScript : MonoBehaviour
{
    private int savedScene;
    private int idScene;

    private Data data;
    public SaveManagerScript saveManager;
    
    public GameObject HiddenPauseMenu;
    public GameObject HiddenSettingsMenu;
    public GameObject hiddenObjects;
    

    void Start()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;
        
        if (idScene != 3) // if we are not in settings menu
        {
            saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManagerScript>();
        }
        
        if (idScene != 0 & idScene !=3)  // if we are not in menu and settings menu
        {
            hiddenObjects = GameObject.FindGameObjectWithTag("Hidden");
            HiddenPauseMenu = hiddenObjects.transform.Find("PauseMenuObject")?.gameObject; // I tried to find object which was not active, but this is working
            HiddenSettingsMenu = hiddenObjects.transform.Find("GameSettingsPauseMenu")?.gameObject;
        }
    }

    void Update()
    {

        if (idScene != 0 & idScene !=3)  // if we are not in menu and settings menu
        {
            if (Input.GetKeyDown(KeyCode.Escape) & (HiddenPauseMenu.activeSelf==false))  // escape for returning to menu (all will be saved meanwhile)
            {
                HiddenPauseMenu.SetActive(true);
                
                
                
                // pause all in game here!
                
                // TODO: ADD pausing game (so all traps and everything is paused)  also hide UI
                
                //...
                
                
                
                
                //SaveScene();  // this will be in pause menu script not here
            }
            
            if (Input.GetKeyDown(KeyCode.Escape) & (HiddenSettingsMenu.activeSelf==true))  // escape for returning to menu (all will be saved meanwhile)
            {
                HiddenSettingsMenu.SetActive(false);
            }
            
        }

        if (idScene == 3)
        {
            if (Input.GetKeyDown(KeyCode.Escape))  // escape for returning to menu (all will be saved meanwhile)
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }
    
    public void LoadLastSavedScene()
    {
        
        Data data = saveManager.LoadMyStuffPlease();

        if (data != null)
        {
            SceneManager.LoadScene(2);  // this will load copy of game scene only difference is in indexes so I can handle later loading of previous game states (by checking index of scene, id=2 means I need to load stuff from file)

        }
        
    }

    public void SaveScene()  // not my code!: (saving of scene borrowed from net)
    {
        
        saveManager.SaveMePlease();
        
        SceneManager.LoadSceneAsync(0);

    }



    public void ShowHiddenSettingsMenu()  // only in pause menu (in game)
    {
        HiddenSettingsMenu.SetActive(true);
    }
    
    
    
}
