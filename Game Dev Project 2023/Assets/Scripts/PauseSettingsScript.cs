using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Arisu

// I know this script is redundant (if there will be time later I will change it)

public class PauseSettingsScript : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float volume;  // this was used first time, now we can delete it since we are loading volume from playerPrefs

    
    
    // Start is called before the first frame update
    void Start()
    {
        
        //idScene = SceneManager.GetActiveScene().buildIndex;
        
        
        if (PlayerPrefs.HasKey("sound"))
        {
            Load();
        }
        else
        {
            PlayerPrefs.SetFloat("sound", volume);
            Load();
        }
        

        SetSavedVolumeInGame();

    }
    

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value*0.6f;
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
    
    private void SetSavedVolumeInGame()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("sound")*0.6f; // I need to have little bit more quiet music in game
        
    }
    
}
