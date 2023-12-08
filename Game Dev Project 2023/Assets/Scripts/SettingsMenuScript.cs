using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenuScript : MonoBehaviour
{

    [SerializeField] private Slider volumeSlider;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetFloat("sound", 0.26f);
            Load();
        }
        else
        {
            Load();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void ChangeVolume()
    {

        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("sound");

    }


    private void Save()
    {
        
        PlayerPrefs.SetFloat("sound", volumeSlider.value);
    }
    
    
}
