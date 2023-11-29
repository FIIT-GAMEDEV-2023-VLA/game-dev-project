using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Alica

public class LogicScript : MonoBehaviour
{
    public int playerHealth;  // ešte inicializovať treba
    public int playerTorchCounter;
    public Text torchCountText;
    public int numOfHeartContainers;  // how many lifes there will be (3 but we can change our mind) (now is max 3, but we can change it to less)
    //public GameObject gameOverScreen;

    public LogicScript(int initializeHealth)
    {
            playerHealth = initializeHealth;
    }

    public Image[] hearts;  // UI images v unity
    public Sprite Heart;
    public Sprite EmptyHeart;

    void Start()
    {
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
    
    void Update()  // UI (hearts) and torch count
    {
        if (playerHealth>numOfHeartContainers) { playerHealth=numOfHeartContainers; } // lebo health nemoze byt viac ako mame srdiecok na obrazovke
        for (int i = 0; i < hearts.Length; i++)  // how many hearts will be visible (total health)
        {
            if ((i<playerHealth)) { hearts[i].sprite = Heart; } else { hearts[i].sprite = EmptyHeart; }
            if (i<numOfHeartContainers) { hearts[i].enabled = true; } else { hearts[i].enabled = false; }
        }

        torchCountText.text = "Torches: " + playerTorchCounter.ToString();

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
        playerHealth += lifeToAdd;
    }

    public void loseLife(int lifeToLose)  // if :cc ouch then call this function
    {
        playerHealth -= lifeToLose;
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


}
