using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Arisu
// Object with this script as component will be left across all scenes (just music is changing)
// Audios is not our original but since we do not pubish this game it should be allright

public class SettingPreferencesScript : MonoBehaviour
{
    
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float volume;  // this was used first time, now we can delete it since we are loading volume from playerPrefs
    

    private int idScene;

    public static SettingPreferencesScript instance;
    
    private AudioSource[] audioSources;  // so I can have more audio sources and change them

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
            if (((gameObject.GetComponent<Slider>() == null) & (idScene == 0 | idScene == 3))) // i know this is not ideal solution but I made it like that this script is also used by slider and I don't want slider to be destroyed
            {
                Destroy(gameObject);
            } 
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
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

        SetSavedVolume();
        
        audioSources = GetComponents<AudioSource>();

    }

    void Update()  // song will change wen music is played
    {

        int oldId = idScene;
        idScene = SceneManager.GetActiveScene().buildIndex;  // I want to change songs between scenes

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