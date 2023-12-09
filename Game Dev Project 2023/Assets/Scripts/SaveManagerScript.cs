using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

// Alica

public class SaveManagerScript : MonoBehaviour
{
    
    public PlayerScript playerScript;
    public ResourceManagerScript resourceManagerScript;
    
    private int idScene;
    
    // Start is called before the first frame update
    void Start()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;
        
        if (idScene != 0)  // if we are not in menu
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>(); 
            resourceManagerScript = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>(); 
        }
        
    }

    public void SaveMePlease()
    {
        
        Data myData = new Data();  // just wrapped data for better saving
        myData.positionX = playerScript.transform.position.x;
        myData.positionY = playerScript.transform.position.y;
        myData.positionZ = playerScript.transform.position.z;
        myData.playerHealth = resourceManagerScript.GetPlayerHealth();
        myData.playerTorchCounter = resourceManagerScript.GetTorchCount();

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


[System.Serializable] public class Data  // in this format I will be saving my game data
{
    
    public float positionX;
    public float positionY;
    public float positionZ;
    public int playerHealth;
    public int playerTorchCounter;
    
}
