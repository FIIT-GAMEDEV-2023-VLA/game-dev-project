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
    

    void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManagerScript>();
    }

    void Update()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;

        if (idScene != 0 & idScene !=3)  // if we are not in menu
        {
            if (Input.GetKeyDown(KeyCode.Escape))  // escape for returning to menu (all will be saved meanwhile)
            {
                SaveScene();
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
    
    
}
