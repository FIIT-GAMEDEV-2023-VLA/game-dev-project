//Author: Alica Urbanová
//Revised, refactored and finished by: Leonard Puškáč
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManagerScript : MonoBehaviour
{
    [SerializeField] private int playerHealth = 4;
    [SerializeField] private int playerMaxHealth = 4;
    [SerializeField] private int playerTorchCounter = 2;
    
    [SerializeField] private Text torchCountText;
    [SerializeField] private Image[] hearts;  // UI images v unity
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    private SpawnManagerScript spawnManagerScript;
    //public GameObject gameOverScreen;
    
    void Start()
    {
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManagerScript>();
        int idScene = SceneManager.GetActiveScene().buildIndex;  // if current scene is saved game
        if (idScene==2)
        {
            SaveManagerScript saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManagerScript>();
            Data data = saveManager.LoadMyStuffPlease();
            playerHealth = data.playerHealth;
            playerTorchCounter = data.playerTorchCounter;
        }
    }

    // neskôr v player script budeme overovať (pri kolizii), či má postava ešte životy, a keď stratí 3, tak sa z daného scriptu zavolá game over screen
    // aj player life status bude až v player scripte (status alive = true na false sa zmení iba ak bude mať nula životov)
    
    void Update()
    {
        // CHECK IF PLAYER HEALTH DOES NOT EXCEED MAX HEALTH
        if (playerHealth>playerMaxHealth) {playerHealth=playerMaxHealth;}
        // UPDATE GUI ELEMENTS - HEALTH AND TORCH COUNT
        for (int i = 0; i < hearts.Length; i++)
        {
            if ((i<playerHealth)) { hearts[i].sprite = heartSprite; } else { hearts[i].sprite = emptyHeartSprite; }
            if (i<playerMaxHealth) { hearts[i].enabled = true; } else { hearts[i].enabled = false; }
        }

        torchCountText.text = "Torches: " + playerTorchCounter;
    }

    public void addTorch(int torchesToAdd)
    {
        playerTorchCounter += torchesToAdd;
        //torchCountText.text = "Torches: " + playerTorchCounter.ToString();
    }
    public void removeTorch(int torchesToRemove)
    {
        playerTorchCounter -= torchesToRemove;
        //torchCountText.text = "Torches: " + playerTorchCounter.ToString();
    }

    public void addLife(int lifeToAdd)  // if healing, then call this
    {
        if (playerHealth + lifeToAdd <= playerMaxHealth)
        {
            playerHealth += lifeToAdd;
        }
    }

    public void loseLife(int lifeToLose)  // if :cc ouch then call this function
    {
        if (playerHealth > 0)
        {
            playerHealth -= lifeToLose;
            if (playerHealth < 0)
            {
                playerHealth = 0;
            }
        }
        
        // TODO: add a condition for playerHealth == 0 and change the outcome to be resetgame
        if (playerHealth >= 0)
        {
            spawnManagerScript.SpawnPlayer();
        }
        // toto až v player scripte, podmienka (v prípade že stratil 3 životy zavolá sa game over screen):
        // gameOverScreen.SetActive(true);
    }

    public void restartGame()  // ešte spravím
    {
        //gameOverScreen.SetActive(false);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // zavolám konkrétnu scénu
    }

    public void gameOver()  // bude volané z player script
    {
        //gameOverScreen.SetActive(true);  // odkomentujem až to doimplementujem
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public int GetTorchCount()
    {
        return playerTorchCounter;
    }


}
