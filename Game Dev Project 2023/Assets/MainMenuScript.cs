using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Alica

public class MainMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    //, IPointerEnterHandler, IPointerExitHandler
{

    //public Text buttonNewGameText; // I want to change color of text in button when selected/hovered (to obtain responsiveness)
    //public Text buttonContinueText; 
    //public Text buttonSettingsText; 
    //public Text buttonQuitText; 
    //public Button buttonNewGame;
    //public Button buttonContinue;
    //public Button buttonSettings;
    //public Button buttonQuit;
    
    
    public ButtonBehaviorScript buttonBehavior;

    public float delayBetweenChangedScene;  // delay before changing scene (for buttons to seem responsive)

    
    
    private bool hovered = false;  // flag which tell if object of button is hovered

    // here will be all buttons and text we use in main menu scene (which we will interact with)
    [SerializeField] Button[] buttonsInMenu;
    [SerializeField] Text[] textsInMenu;

    public Color colorBeforeHover;
    public Text hoveredText;
    
    public Text selectedText;  // just for start because there were problems with setting right color after hovering on selected button at the beginning
    private int selectedIndex = 0;  // for using key up and down (some index of buttons/texts)
    private int previousIndex = 0;
    

    void Start()  // loading some stuff for scene
    {
        buttonBehavior = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonBehaviorScript>();  // load of buttonBehavior script
        
        buttonsInMenu = new Button[4];  // I need to load some texts and buttons (editor in unity will not solve this for me)
        textsInMenu = new Text[4];
        
        GameObject buttonObject = GameObject.Find("ButtonNG");  // I need to find all objects in scene to work with them
        buttonsInMenu[0] = buttonObject.GetComponent<Button>();
        buttonObject = GameObject.Find("ButtonContinue");
        buttonsInMenu[1] = buttonObject.GetComponent<Button>();
        buttonObject = GameObject.Find("ButtonSettings");
        buttonsInMenu[2] = buttonObject.GetComponent<Button>();
        buttonObject = GameObject.Find("ButtonQuit");
        buttonsInMenu[3] = buttonObject.GetComponent<Button>();
        
        GameObject textObject = GameObject.Find("TextNG");
        textsInMenu[0] = textObject.GetComponent<Text>();
        textObject = GameObject.Find("TextContinue");
        textsInMenu[1] = textObject.GetComponent<Text>();
        textObject = GameObject.Find("TextSettings");
        textsInMenu[2] = textObject.GetComponent<Text>();
        textObject = GameObject.Find("TextQuit");
        textsInMenu[3] = textObject.GetComponent<Text>();


        selectedText = textsInMenu[0];  // just giving right colors to text because there were some problems at start with colors after hovering upon button/text which was selected at beginning
        hoveredText = textsInMenu[0];
        hoveredText.color = buttonBehavior.colorHoveredSelected;
        colorBeforeHover = buttonBehavior.colorHoveredSelected;
        selectedText.color = buttonBehavior.colorSelected;
    }

    void Update()  // just some key handling
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))  // up was selected so we move in menu up
        {
            previousIndex = selectedIndex;
            selectedIndex = selectedIndex - 1;
            if (selectedIndex == -1) { selectedIndex = 3; }
            
            textsInMenu[previousIndex] = buttonBehavior.ChangeOfColorUnclickedButtonText(textsInMenu[previousIndex]);  // colors to make buttons responsive
            textsInMenu[selectedIndex] = buttonBehavior.ChangeOfColorSelectedButtonText(textsInMenu[selectedIndex]);
            

        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))  // down was selected so we move in menu down
        {
            previousIndex = selectedIndex;
            selectedIndex = selectedIndex + 1;
            if (selectedIndex == 4) { selectedIndex = 0; }
            
            textsInMenu[previousIndex] = buttonBehavior.ChangeOfColorUnclickedButtonText(textsInMenu[previousIndex]);  // colors to make buttons responsive
            textsInMenu[selectedIndex] = buttonBehavior.ChangeOfColorSelectedButtonText(textsInMenu[selectedIndex]);
            
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))  // after "enter" key pressed selected button will be clicked
        {
            buttonsInMenu[selectedIndex].onClick.Invoke();
        }
        
    }

    public void NewGame()  // after choosing New Game option in menu
    {
        textsInMenu[0] = buttonBehavior.ChangeOfColorClickedButtonText(textsInMenu[0]); // button clicked color
        
        Invoke("LoadNewGameScene",delayBetweenChangedScene);  // we will wait a while before changing scene (so buttons seems responsive)
    }
    
    public void ContinueGame(){}
    
    public void SetSettings(){}
    
    public void QuitGame(){}



    public void LoadNewGameScene()  // just function to change scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        textsInMenu[0] = buttonBehavior.ChangeOfColorSelectedButtonText(textsInMenu[0]); // to just change color of button back
    }
    
    
    
    public void OnPointerEnter(PointerEventData eventData)  // function will run whenever cursor colides with object
    {
        GameObject enteredObject = eventData.pointerEnter;
        
        Button enteredButton = enteredObject.GetComponent<Button>();  // button on which we hovered

        if (enteredButton != null)
        {
            int buttonIndex = System.Array.IndexOf(buttonsInMenu, enteredButton);  // we will identify the button

            if (buttonIndex != -1)
            {
                hovered = true;
                hoveredText = textsInMenu[buttonIndex];
                colorBeforeHover = hoveredText.color;
                
                if (colorBeforeHover == buttonBehavior.colorSelected)  // there are different hovering colors when button is selected or is not selected
                {
                    textsInMenu[buttonIndex] = buttonBehavior.ChangeOfColorHoveredSelectedButtonText(textsInMenu[buttonIndex]);
                }
                else 
                {
                    textsInMenu[buttonIndex] = buttonBehavior.ChangeOfColorHoveredButtonText(textsInMenu[buttonIndex]);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)  // this function will run after exiting an object with cursor (responsivity/hovers)
    {
        GameObject enteredObject = eventData.pointerEnter;
        
        Button enteredButton = enteredObject.GetComponent<Button>();   // button we unhovered

        if (hovered==true) {  // only if we hovered on button
            if (enteredButton != null)
            {
                hoveredText.color = colorBeforeHover;
                hovered = false;
            }
        }
    }
    
}
        