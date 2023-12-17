using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Arisu (this script is for better UX, since it just looks better when game screen does not shows up from nowhere)
// could do it with enumerator and stuff so screen is blocked in time when animation is played but since animation is so short I will let it how it is

// I know it would be better to call this in scene manager script (maybe I will redo it later)

public class LevelScreenLoader : MonoBehaviour
{
    private int idScene;

    //public Animator crossSceneTransition;
    public Animator beginningStoryTransition;

    private bool playAnim = true;
    
    // Start is called before the first frame update
    void Start()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;
        GameObject saveMessageGameObject = GameObject.Find("SaveMessagePassBohuzial");
        if (saveMessageGameObject)
        {
            SaveMessagePassScript saveMessagePassScript = saveMessageGameObject.GetComponent<SaveMessagePassScript>();
            if (saveMessagePassScript.IsContinued())
            {
                playAnim = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()  
    {
        int oldId = idScene;
        idScene = SceneManager.GetActiveScene().buildIndex;  // I want to change songs between scenes

        if (idScene != oldId && playAnim)
        {
            if (idScene == 1 | idScene == 2)
            {
                beginningStoryTransition.SetTrigger("StartStory");
                //crossSceneTransition.SetTrigger("StartCrossSceneAnimation");
            }
            
        }
        
    }
    
    
}
