using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Alica

public class ButtonBehaviorScript : MonoBehaviour
{
    // 'cause I want to change color of text in button when selected/hovered (to obtain responsiveness)
    // this script is just for setting colors
    public Color colorClicked;
    public Color colorUnclicked;
    public Color colorHovered;
    public Color colorSelected;
    public Color colorHoveredSelected;
    
    public Text buttonText;
    
    public Text ChangeOfColorClickedButtonText(Text actualButtonText)  // color of text changes when button is clicked
    {
        buttonText = actualButtonText;
        buttonText.color = colorClicked;
        return buttonText;
    }
    
    public Text ChangeOfColorUnclickedButtonText(Text actualButtonText)
    {
        buttonText = actualButtonText;
        buttonText.color = colorUnclicked;
        return buttonText;
    }
    
    public Text ChangeOfColorHoveredButtonText(Text actualButtonText)
    {
        buttonText = actualButtonText;
        buttonText.color = colorHovered;
        return buttonText;
    }
    
    public Text ChangeOfColorSelectedButtonText(Text actualButtonText)  // selected buttons will have different color
    {
        buttonText = actualButtonText;
        buttonText.color = colorSelected;
        return buttonText;
    }
    
    public Text ChangeOfColorHoveredSelectedButtonText(Text actualButtonText)  // also hovering on selected button will look different
    {
        buttonText = actualButtonText;
        buttonText.color = colorHoveredSelected;
        return buttonText;
    }
    

}
