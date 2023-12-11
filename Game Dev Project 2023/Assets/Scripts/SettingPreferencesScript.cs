using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingPreferencesScript : MonoBehaviour
{
    
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float volume;  // this was used first time, now we can delete it since we are loading volume from playerPrefs
    

    private int idScene;

    public static SettingPreferencesScript instance;
    private bool isMenuMusic = false;  // I didn't come up with better solution how to know when to play or stop songs (songs are different in menu and game)
    //private bool deleteMenuMusic = false;
    //private bool isGameMusic = false;
    
    private AudioSource[] audioSources;
    
    [SerializeField] public GameObject BGmusic;

    private void Awake()  // for keeping same song in scene of menu and settings scene
    {
        if (instance == null)
        {
            instance = this;
            idScene = SceneManager.GetActiveScene().buildIndex;
            if (idScene == 0 | idScene == 3)
            {
                if (gameObject.GetComponent<Slider>() == null) { DontDestroyOnLoad(gameObject); }
            }
        }
        else
        {
            if (((gameObject.GetComponent<Slider>() == null) & (idScene == 0 | idScene == 3)) | ((idScene == 1 | idScene == 2) & isMenuMusic)) // i know this is not ideal solution but I made it like that this script is also used by slider and I don't want slider to be destroyed
            {
                //if (gameObject.GetComponent<AudioSource>)
                Destroy(gameObject);
            } 
        }
        
        
    }
    
    
    // Start is called before the first frame update
    void Start()
    {

        //PlayerPrefs.SetFloat("sound", volumeSlider.value);
        
        idScene = SceneManager.GetActiveScene().buildIndex;
        
        if (idScene == 3) // if we are in settings
        {
            if (PlayerPrefs.HasKey("sound"))
            {
                Load();
            }
            else
            {
                PlayerPrefs.SetFloat("sound", volume);
                Load();
            }
        }

        if ((idScene == 0 | idScene == 3) & isMenuMusic == false)
        {
            isMenuMusic = true; 
        }

        SetSavedVolume();
        
        audioSources = GetComponents<AudioSource>();

    }

    void Update()  // song will change wen music is played
    {
        // I somehow need to destroy bg music from menu when entering game, but it can be destroyed only in update (I tried destroy in start function, but it does not work there)
        if (SceneManager.GetActiveScene().buildIndex == 1 | SceneManager.GetActiveScene().buildIndex == 2)
        {

            if (isMenuMusic == true)
            {
                //BGmusic.GetComponent<AudioSource>().Stop();
                //Destroy(BGmusic);
            }
        }

        int oldId = idScene;
        idScene = SceneManager.GetActiveScene().buildIndex;

        if (idScene != oldId)
        {
            if (idScene == 1 | idScene == 2)
            {
                audioSources[0].Stop();
                audioSources[1].enabled = true;
                audioSources[1].Play();
                SetSavedVolumeInGame();  // I need to have little bit more quiet music in game
            }
            if (oldId == 1 | oldId == 2)
            {
                audioSources[1].Stop();
                audioSources[0].Play();
                SetSavedVolume();
            }
            
        }
        

    }
    
    


    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()  // we are loading settings from playerprefs
    {
        volumeSlider.value = PlayerPrefs.GetFloat("sound");

    }


    private void Save()
    {
        PlayerPrefs.SetFloat("sound", volumeSlider.value);
        PlayerPrefs.Save();
    }

    private void SetSavedVolume()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("sound");
        
    }
    
    private void SetSavedVolumeInGame()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("sound")*0.6f; // I need to have little bit more quiet music in game
        
    }
    
    
    
    // maybe I will add something later
    
    public void LoadMySettings()
    {
        
    }
    
    public void SaveNewSettings()
    {
        
    }
    
    
}



// this is not used anywhere: (maybe I will delete it later)

[System.Serializable] public class SettingsData  // in this format I will be saving my settings preference data
{
    
    public float volume;
    
}