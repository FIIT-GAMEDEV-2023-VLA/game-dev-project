using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Alica

public class LogicScript : MonoBehaviour
{
    public int playerHealth;  // ešte inicializovať treba
    public int playerTorchCounter;
    public Text torchCountText;
    //public GameObject gameOverScreen;

    public LogicScript(int initializeHealth)
    {
            playerHealth = initializeHealth;
    }

    // neskôr v player script budeme overovať, či má postava ešte životy, a keď stratí 3, tak sa z daného scriptu zavolá game over screen


    public void addLife(int lifeToAdd)  // if healing, then call this
    {

        playerHealth += lifeToAdd;

        // doimplementujem UI pak ešte tu:

    }

    public void loseLife(int lifeToLose)  // if :cc ouch then call this function
    {

        playerHealth -= lifeToLose;

        // doimplementujem UI pak ešte tu:

        // doimplementovať if statement, či je už dead tu:

        // gameOverScreen.SetActive(true);

    }

    public void restartGame()  // ešte spravím
    {
        //gameOverScreen.SetActive(false);


        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()  // bude volané z player script
    {
        //gameOverScreen.SetActive(true);
    }



}
