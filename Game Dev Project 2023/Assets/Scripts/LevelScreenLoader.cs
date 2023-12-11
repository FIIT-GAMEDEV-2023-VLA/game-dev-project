using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScreenLoader : MonoBehaviour
{
    private int idScene;

    public Animator crossSceneTransition;
    
    // Start is called before the first frame update
    void Start()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;
        
    }

    // Update is called once per frame
    void Update()
    {
        int oldId = idScene;
        idScene = SceneManager.GetActiveScene().buildIndex;  // I want to change songs between scenes

        if (idScene != oldId)
        {
            if (idScene == 1 | idScene == 2)
            {
                crossSceneTransition.SetTrigger("StartCrossSceneAnimation");
            }
            
        }
        
    }
    
    
}
