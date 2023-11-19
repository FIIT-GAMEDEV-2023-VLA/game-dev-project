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


    private bool wassaved = false;
    
    

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
        //savedScene = PlayerPrefs.GetInt("Saved");  // to get index of saved scene

        //if (savedScene != 0)
        //{
        //    SceneManager.LoadSceneAsync(savedScene);  // to load scene
        //}
        
        
        if (wassaved == true)
        {
            SceneManager.LoadScene("Saved2");
        }
    }

    public void SaveScene()  // not my code!: (saving of scene borrowed from net)
    {
        //idScene = SceneManager.GetActiveScene().buildIndex;
        
        //PlayerPrefs.SetInt("Saved", idScene);  // to save scene as "Saved"
        //PlayerPrefs.Save();
        //SceneManager.LoadSceneAsync(0);  // to load main menu
        
        
        
        
        
        Scene currentScene = SceneManager.GetActiveScene();
        Scene newScene = SceneManager.CreateScene("Saved2");
        //GameObject sceneCopy = new GameObject("SceneCopy");

        // Prechádzať všetky GameObjecty vo vybranej scéne
        foreach (GameObject obj in currentScene.GetRootGameObjects())
        {
            // Vytvoriť kópiu GameObjectu
            GameObject newObj = Instantiate(obj);

            // Presunúť kópiu do novej scény
            SceneManager.MoveGameObjectToScene(newObj, newScene);
        }

        wassaved = true;
        
        SceneManager.LoadSceneAsync(0);

    }
    
    
}
