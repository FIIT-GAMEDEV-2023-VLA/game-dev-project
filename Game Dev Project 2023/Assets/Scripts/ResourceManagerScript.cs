//Author: Alica Urbanová
//Revised, refactored and finished by: Leonard Puškáč

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameObject = UnityEngine.GameObject;

public class ResourceManagerScript : MonoBehaviour
{
    [SerializeField] private int playerHealth = 4;
    [SerializeField] private int playerMaxHealth = 4;
    [SerializeField] private int playerStartingTorchCount = 0;
    [SerializeField] private int playerTorchCounter = 0;
    
    [SerializeField] private Text torchCountText;
    [SerializeField] private Image[] hearts;  // UI images v unity
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    
    private SpawnManagerScript spawnManagerScript;

    private GameObject gameOverScreen; 
    //public GameObject gameOverScreen;
    
    void Start()
    {   
        playerTorchCounter = playerStartingTorchCount;
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManagerScript>();
        
        GameObject hiddenObjects = GameObject.FindGameObjectWithTag("Hidden");
        gameOverScreen = hiddenObjects.transform.Find("GameOverScreen")?.gameObject;
        if (gameOverScreen)
        {
            Debug.Log("Found Game Over Screen!");
        }
    }

    public void LoadSavedResources(Data savedData)
    {
        playerHealth = savedData.playerHealth;
        playerTorchCounter = savedData.playerTorchCounter;
        Debug.Log("Resource Manger Loaded Saved Game Resources!");
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

    public void AddTorch(int torchesToAdd)
    {
        playerTorchCounter += torchesToAdd;
        //torchCountText.text = "Torches: " + playerTorchCounter.ToString();
    }
    public void RemoveTorch(int torchesToRemove)
    {
        playerTorchCounter -= torchesToRemove;
        //torchCountText.text = "Torches: " + playerTorchCounter.ToString();
    }

    public void AddLife(int lifeToAdd)
    {
        if (playerHealth + lifeToAdd <= playerMaxHealth)
        {
            playerHealth += lifeToAdd;
        }
    }

    public void LoseLife(int lifeToLose)
    {
        if (playerHealth > 0)
        {
            playerHealth -= lifeToLose;
            if (playerHealth < 0)
            {
                playerHealth = 0;
            }
        }
        
        // CHECK IF THE GAME HAS ENDED - 0 HEALTH = RESET GAME 
        if (playerHealth > 0)
        {
            spawnManagerScript.SpawnPlayer();
        }
        else
        {
            // RESET RESOURCES AND RESPAWN AT THE START
            gameOverScreen.SetActive(true);
            //ResetPlayerResources();
            //spawnManagerScript.ResetGame();
        }
    }
    
    private void ResetPlayerResources()
    {
        playerHealth = playerMaxHealth;
        playerTorchCounter = playerStartingTorchCount; 
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
