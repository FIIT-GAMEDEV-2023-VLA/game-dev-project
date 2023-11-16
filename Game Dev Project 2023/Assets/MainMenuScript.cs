using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Text buttonNGText; // 'cause I want to change color of text in button when selected/hovered (to obtain responsiveness)
    //public string RGBAColor;
    public Color colorClicked;
    
    void Update()
    {
        
        
    }


    public void NewGame()
    {
        ChangeColor(colorClicked);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }




    public void ChangeColor(Color color)
    {
        
        buttonNGText.color = color;

    }

}
