using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Alica







// tento kod je cely zle to cele chcem prerobit este!!





public class SceneManagerScript : MonoBehaviour
{
    private int savedScene;
    private int idScene;
    
    //public MainMenuScript mainMenu;


    //private bool wassaved = false;

    private Data data;
    
    
    
    public SaveManagerScript saveManager;
    

    void Start()
    {
        //mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuScript>(); 
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManagerScript>();
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
        
        
        //SceneManager.LoadScene("Saved2");
        
        // nope inak: budem tu načítavať veci z json súborov a potom iba klasicky loadnem scénu a nastavím stuff na pôvodný stav
        
        Data data = saveManager.LoadMyStuffPlease();

        if (data != null)
        {

            //SceneManager.LoadSceneAsync(1);
            
            //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

            SceneManager.LoadScene(2);  // this will load copy of game scene only difference is in indexes so I can handle later loading of previous game states (by checking index of scene, id=2 means I need to load stuff from file)

            
            /*
            PlayerScript playerStats = GameObject.Find("Player").GetComponent<PlayerScript>();
            LogicScript logicStats = GameObject.Find("Logic Manager").GetComponent<LogicScript>();

            var x = data.positionX;
            var y = data.positionY;
            var z = data.positionZ;

            playerStats.transform.position = new Vector3(x, y, z);
            logicStats.playerHealth = data.playerHealth;
            logicStats.playerTorchCounter = data.playerTorchCounter;
            */

        }
        
    }

    public void SaveScene()  // not my code!: (saving of scene borrowed from net)
    {
        //idScene = SceneManager.GetActiveScene().buildIndex;
        
        //PlayerPrefs.SetInt("Saved", idScene);  // to save scene as "Saved"
        //PlayerPrefs.Save();
        //SceneManager.LoadSceneAsync(0);  // to load main menu
        
        
        
        /*
        
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

        */
        
        
        saveManager.SaveMePlease();

        //wassaved = true;
        
        SceneManager.LoadSceneAsync(0);

    }
    
    
    
    GameObject GetObjectFromScene(string sceneName, string objectName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (scene.IsValid())
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();

            foreach (GameObject obj in rootObjects)
            {
                if (obj.name == objectName)
                {
                    return obj;
                }
            }
        }

        return null;
    }
    
    
   
    
    
}
