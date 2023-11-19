using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Alica

public class sceneManagerScript : MonoBehaviour
{
    private int savedScene;
    private int idScene;
    
    public void LoadLastSavedScene()  // not my code!: (loading of scene borrowed from net)
    {
        savedScene = PlayerPrefs.GetInt("Saved");  // to get index of saved scene

        if (savedScene != 0)
        {
            SceneManager.LoadSceneAsync(savedScene);  // to load scene
        }
    }

    public void SaveScene()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;
        
        PlayerPrefs.SetInt("Saved", idScene);  // to save scene as "Saved"
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(0);  // to load main menu
    }
    
    
}
