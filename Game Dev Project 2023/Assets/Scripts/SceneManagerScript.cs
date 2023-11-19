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
    
    //public MainMenuScript mainMenu;
    
    

    void Start()
    {
        //mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuScript>(); 
        
    }

    void Update()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;

        if (idScene != 0)  // if we are not in menu
        {
            if (Input.GetKeyDown(KeyCode.Escape))  // escape for returning to menu (all will be saved meanwhile)
            {
                SaveScene();
            }
        }
    }
    
    public void LoadLastSavedScene()  // not my code!: (loading of scene borrowed from net)
    {
        savedScene = PlayerPrefs.GetInt("Saved");  // to get index of saved scene

        if (savedScene != 0)
        {
            SceneManager.LoadSceneAsync(savedScene);  // to load scene
        }
    }

    public void SaveScene()  // not my code!: (saving of scene borrowed from net)
    {
        idScene = SceneManager.GetActiveScene().buildIndex;
        
        PlayerPrefs.SetInt("Saved", idScene);  // to save scene as "Saved"
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(0);  // to load main menu
    }
    
    
}
