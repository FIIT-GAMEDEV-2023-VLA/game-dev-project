using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

// Alica

public class SaveManagerScript : MonoBehaviour
{

    private GameObject player;
    private PlayerScript playerScript;
    private ResourceManagerScript resourceManagerScript;
    private SpawnManagerScript spawnManagerScript;
    
    private int idScene;
    
    // Start is called before the first frame update
    void Start()
    {
        idScene = SceneManager.GetActiveScene().buildIndex;
        
        if (idScene != 0 & idScene != 3)  // if we are not in menu
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent<PlayerScript>(); 
            resourceManagerScript = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>();
            spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManagerScript>();
            GameObject saveMessageGameObject = GameObject.Find("SaveMessagePassBohuzial");
            if (saveMessageGameObject)
            {
                SaveMessagePassScript saveMessagePassScript = saveMessageGameObject.GetComponent<SaveMessagePassScript>();
                if (saveMessagePassScript.IsContinued())
                {
                    Data savedData = LoadMyStuffPlease();
                    resourceManagerScript.LoadSavedResources(savedData);
                    spawnManagerScript.LoadSavedGameSpawn(savedData);
                    Destroy(saveMessageGameObject);
                }
            }
        }
        
    }

    public void SaveMePlease()
    {
        
        Data myData = new Data();  // just wrapped data for better saving
        myData.positionX = player.transform.position.x;
        myData.positionY = player.transform.position.y;
        myData.positionZ = player.transform.position.z;
        myData.playerHealth = resourceManagerScript.GetPlayerHealth();
        myData.playerTorchCounter = resourceManagerScript.GetTorchCount();
        myData.saveZoneIndex = spawnManagerScript.GetActiveSafeZoneIndex();
        
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
    public int saveZoneIndex;

}
