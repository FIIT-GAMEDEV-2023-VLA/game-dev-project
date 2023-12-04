using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Alica

public class ButtonBehaviorScript : MonoBehaviour
{
    // 'cause I want to change color of button when selected/hovered (to obtain responsiveness)
    // this script is just for setting different looks of buttons while interacting with mouse and keys
    
    
    public Image[] buttons;  // I will name it like this, cause basicly images are replacement for buttons
    public Sprite[] imageClicked;
    public Sprite[] imageUnclicked;
    public Sprite[] imageHovered;
    public Sprite[] imageSelected;
    public Sprite[] imageHoveredSelected;

    [SerializeField] private AudioSource buttonClickedSound;
    
    public Image ChangeOfColorClickedButton(Image actualButton, int idButton)  // color changes when button is clicked
    {
        buttonClickedSound.Play();
        buttons[idButton] = actualButton;
        buttons[idButton].sprite = imageClicked[idButton];
        return buttons[idButton];
    }
    
    public Image ChangeOfColorUnclickedButton(Image actualButton, int idButton)
    {
        buttons[idButton] = actualButton;
        buttons[idButton].sprite = imageUnclicked[idButton];
        return buttons[idButton];
    }
    
    public Image ChangeOfColorHoveredButton(Image actualButton, int idButton)
    {
        buttons[idButton] = actualButton;
        buttons[idButton].sprite = imageHovered[idButton];
        return buttons[idButton];
    }
    
    public Image ChangeOfColorSelectedButton(Image actualButton, int idButton)  // selected buttons will have different color
    {
        buttonClickedSound.Play();
        buttons[idButton] = actualButton;
        buttons[idButton].sprite = imageSelected[idButton];
        return buttons[idButton];
    }
    
    public Image ChangeOfColorHoveredSelectedButton(Image actualButton, int idButton)  // also hovering on selected button will look different
    {
        buttons[idButton] = actualButton;
        buttons[idButton].sprite = imageHoveredSelected[idButton];
        return buttons[idButton];
    }
    

}
