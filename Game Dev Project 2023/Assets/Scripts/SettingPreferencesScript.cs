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

        SetSavedVolume();

    }

    void Update()
    {
        if (idScene == 3) // if we are in settings
        {
            //Load();
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