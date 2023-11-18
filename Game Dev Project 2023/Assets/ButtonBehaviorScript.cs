using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBehaviorScript : MonoBehaviour
{
    //public Text buttonText; // 'cause I want to change color of text in button when selected/hovered (to obtain responsiveness)
    public Color colorClicked;
    public Color colorUnclicked;
    public Color colorHovered;
    public Color colorSelected;
    
    public Text buttonText;
    
    public Text ChangeOfColorClickedButtonText(Text actualButtonText)
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
    
    public Text ChangeOfColorSelectedButtonText(Text actualButtonText)
    {
        buttonText = actualButtonText;
        buttonText.color = colorSelected;
        return buttonText;
    }
    

}
