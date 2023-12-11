using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    // this script I created so we can switch audio between scene of game and menu
    /*
    [SerializeField] public GameObject BGmusic;
    
    public static AudioScript instance;
    
    private void Awake()  // for keeping same song in scene of menu and settings scene
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 | SceneManager.GetActiveScene().buildIndex == 2)
        {

            BGmusic.GetComponent<AudioSource>().Stop();
        }

        
        
    }
    */
}
