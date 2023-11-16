using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviorScript : MonoBehaviour
{
    public Text buttonNGText; // 'cause I want to change color of text in button when selected/hovered (to obtain responsiveness)
    public Color colorClicked;
    
    public void ColorClickedButton()
    {
        
        
    }
    
    public void ChangeColor(Color color)
    {
        
        buttonNGText.color = color;

    }

}
