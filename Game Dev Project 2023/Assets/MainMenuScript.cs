using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Text buttonNewGameText; // I want to change color of text in button when selected/hovered (to obtain responsiveness)
    
    public ButtonBehaviorScript buttonBehavior;

    public float delayBetweenChangedScene;
    
    void Start()
    {
        buttonBehavior = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonBehaviorScript>();
        
    }
    void Update()
    {
        // tu budem ešte dajaké veci riešiť (hovernutie šípkou zmení farbu text a tak)
    }

    public void NewGame()
    {
        buttonNewGameText = buttonBehavior.ChangeOfColorClickedButtonText(buttonNewGameText);
        
        Invoke("LoadNewGameScene",delayBetweenChangedScene);
    }
    
    public void ContinueGame(){}
    
    public void SetSettings(){}
    
    public void QuitGame(){}



    public void LoadNewGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        buttonNewGameText = buttonBehavior.ChangeOfColorSelectedButtonText(buttonNewGameText);
    }

}
