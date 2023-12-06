using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPreferencesScript : MonoBehaviour
{
    [SerializeField] private float pivotVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        
        // tu si nacitam settingy z filu
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadMySettings()
    {
        
        
    }
    
    
    public void SaveNewSettings()
    {
        
        
    }

    
    
    
    
}



[System.Serializable] public class SettingsData  // in this format I will be saving my settings preference data
{
    
    public float volume;
    
}