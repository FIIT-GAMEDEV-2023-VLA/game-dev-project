using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Text buttonNGText; // 'cause I want to change color of text in button when selected/hovered (to obtain responsiveness)
    
    //public Color colorClicked;
    public ButtonBehaviorScript buttonBehavior;
    
    
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
        buttonNGText = buttonBehavior.ChangeOfColorClickedButtonText(buttonNGText);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        buttonNGText = buttonBehavior.ChangeOfColorUnclickedButtonText(buttonNGText);
    }




}
