using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveManagerScript : MonoBehaviour
{
    
    public PlayerScript playerStats;
    public LogicScript logicStats;
    
    private int idScene;
    
    // Start is called before the first frame update
    void Start()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;
        
        if (idScene != 0)  // if we are not in menu
        {
            playerStats = GameObject.Find("Player").GetComponent<PlayerScript>(); 
            logicStats = GameObject.Find("Logic Manager").GetComponent<LogicScript>(); 
        }
        
    }

    public void SaveMePlease()
    {
        
        Data myData = new Data();  // just wrapped data for better saving
        myData.positionX = playerStats.groundCheck.position.x;
        myData.positionY = playerStats.groundCheck.position.y;
        myData.positionZ = playerStats.groundCheck.position.z;
        myData.playerHealth = logicStats.playerHealth;
        myData.playerTorchCounter = logicStats.playerTorchCounter;

        // this saving I have from tutorial!
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;
        file = File.Create(Application.persistentDataPath + "/SavedGameStats.save");
        binaryFormatter.Serialize(file, myData);

        file.Close();
    }
    
    public Data LoadMyStuffPlease()
    {
        if (File.Exists(Application.persistentDataPath + "/SavedGameStats.save"))
        {
            // this loading I have from tutorial!
            BinaryFormatter binaryFormatter = new BinaryFormatter();  
            FileStream file = File.Open(Application.persistentDataPath + "/SavedGameStats.save", FileMode.Open);
            Data myData = (Data)binaryFormatter.Deserialize(file);
            file.Close();
            return myData;
        }

        return null;
    }
    
}


[System.Serializable] public class Data
{
    
    public float positionX;
    public float positionY;
    public float positionZ;
    public int playerHealth;
    public int playerTorchCounter;
    
}
