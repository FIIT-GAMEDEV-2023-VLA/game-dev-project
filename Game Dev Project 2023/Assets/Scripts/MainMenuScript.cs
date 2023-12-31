using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Alica(/Arisu)

public class MainMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    //, IPointerEnterHandler, IPointerExitHandler
{

    // I want to change color of text in button when selected/hovered (to obtain responsiveness)
    public ButtonBehaviorScript buttonBehavior;
    
    // for Loading of saved scene
    public SceneManagerScript sceneManager;

    public float delayBetweenChangedScene;  // delay before changing scene (for buttons to seem responsive)

    
    
    private bool hovered = false;  // flag which tell if object of button is hovered

    // here will be all buttons and text we use in main menu scene (which we will interact with)
    [SerializeField] Button[] buttonsInMenu;
    //[SerializeField] Text[] textsInMenu;
    [SerializeField] Image[] imagesInMenu;

    private GameObject saveMessagePass;

    //public Color colorBeforeHover;
    //public Text hoveredText;
    
    public Sprite spriteBeforeHover;               // n
    public Image hoveredImage;               // n
    
    //public Text selectedText;  // just for start because there were problems with setting right color after hovering on selected button at the beginning
    public Image selectedImage;  // just for start because there were problems with setting right color after hovering on selected button at the beginning
    
    private int selectedIndex = 0;  // for using key up and down (some index of buttons/texts)
    private int previousIndex = 0;

    private bool wasClicked = false; // just for better UX cause there were this problem that after cliked button with mouse and exiting it, color was changed back to normal (because of unhover)
    

    void Start()  // loading some stuff for scene
    {
        buttonBehavior = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonBehaviorScript>();  // load of buttonBehavior script
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();  // load of scene manager script
        
        buttonsInMenu = new Button[4];  // I need to load some texts and buttons (editor in unity will not solve this for me)
        imagesInMenu = new Image[4];
        
        GameObject buttonObject = GameObject.Find("ButtonNG");  // I need to find all objects in scene to work with them
        buttonsInMenu[0] = buttonObject.GetComponent<Button>();
        buttonObject = GameObject.Find("ButtonContinue");
        buttonsInMenu[1] = buttonObject.GetComponent<Button>();
        buttonObject = GameObject.Find("ButtonSettings");
        buttonsInMenu[2] = buttonObject.GetComponent<Button>();
        buttonObject = GameObject.Find("ButtonQuit");
        buttonsInMenu[3] = buttonObject.GetComponent<Button>();
        
        GameObject imageObject = GameObject.Find("ButtonNGImage");
        imagesInMenu[0] = imageObject.GetComponent<Image>();
        imageObject = GameObject.Find("ButtonContinueImage");
        imagesInMenu[1] = imageObject.GetComponent<Image>();
        imageObject = GameObject.Find("ButtonSettingsImage");
        imagesInMenu[2] = imageObject.GetComponent<Image>();
        imageObject = GameObject.Find("ButtonQuitImage");
        imagesInMenu[3] = imageObject.GetComponent<Image>();


        selectedImage = imagesInMenu[0];  // just giving right image because there were some problems at start after hovering upon button which was selected at beginning
        hoveredImage = imagesInMenu[0];
        hoveredImage.sprite = buttonBehavior.imageHoveredSelected[0];
        spriteBeforeHover = buttonBehavior.imageHoveredSelected[0];
        selectedImage.sprite = buttonBehavior.imageSelected[0];
        
            
        if (PlayerPrefs.HasKey("sound"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("sound");
        }
        else
        {
            AudioListener.volume = 0.26f;
        }
    }

    void Update()  // just some key handling
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))  // up was selected so we move in menu up
        {
            previousIndex = selectedIndex;
            selectedIndex = selectedIndex - 1;
            if (selectedIndex == -1) { selectedIndex = 3; }
            
            imagesInMenu[previousIndex] = buttonBehavior.ChangeOfColorUnclickedButton(imagesInMenu[previousIndex], previousIndex);  // colors to make buttons responsive
            imagesInMenu[selectedIndex] = buttonBehavior.ChangeOfColorSelectedButton(imagesInMenu[selectedIndex], selectedIndex);
            

        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))  // down was selected so we move in menu down
        {
            previousIndex = selectedIndex;
            selectedIndex = selectedIndex + 1;
            if (selectedIndex == 4) { selectedIndex = 0; }
            
            imagesInMenu[previousIndex] = buttonBehavior.ChangeOfColorUnclickedButton(imagesInMenu[previousIndex], previousIndex);  // colors to make buttons responsive
            imagesInMenu[selectedIndex] = buttonBehavior.ChangeOfColorSelectedButton(imagesInMenu[selectedIndex], selectedIndex);
            
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))  // after "enter" key pressed selected button will be clicked
        {
            buttonsInMenu[selectedIndex].onClick.Invoke();
        }
        
    }

    public void NewGame()  // after choosing New Game option in menu
    {
        wasClicked = true; // for better UX I explained it up ^
        imagesInMenu[0] = buttonBehavior.ChangeOfColorClickedButton(imagesInMenu[0], 0); // button clicked color
        
        Invoke("LoadNewGameScene",delayBetweenChangedScene);  // we will wait a while before changing scene (so buttons seems responsive)
    }

    public void ContinueGame()
    {
        wasClicked = true; // for better UX I explained it up ^
        imagesInMenu[1] = buttonBehavior.ChangeOfColorClickedButton(imagesInMenu[1], 1); // button clicked color
        
        Invoke("LoadSavedGameScene",delayBetweenChangedScene);  // we will wait a while before changing scene (so buttons seems responsive)
    }

    public void SetSettings()
    {
        wasClicked = true; // for better UX I explained it up ^
        imagesInMenu[2] = buttonBehavior.ChangeOfColorClickedButton(imagesInMenu[2], 2); // button clicked color
        
        Invoke("LoadSettingseScene",delayBetweenChangedScene);  // we will wait a while before changing scene (so buttons seems responsive)
    }

    public void QuitGame()
    {
        wasClicked = true; // for better UX I explained it up ^
        imagesInMenu[3] = buttonBehavior.ChangeOfColorClickedButton(imagesInMenu[3], 3); // button clicked color
        
        Invoke("MyQuit",delayBetweenChangedScene);  // we will wait a while before changing scene (so buttons seems responsive)
    }



    public void LoadNewGameScene()  // just function to change scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void LoadSavedGameScene()  // just function to change scene
    {
        saveMessagePass = GameObject.Find("SaveMessagePassBohuzial");
        if (saveMessagePass)
        {
            SaveMessagePassScript saveMessagePassScript = saveMessagePass.GetComponent<SaveMessagePassScript>();
            saveMessagePassScript.SetContinuedFlag(true);
        }
        SceneManager.LoadScene(1);
        //sceneManager.LoadLastSavedScene();
    }
    
    public void LoadSettingseScene()  // just function to change scene
    {
        SceneManager.LoadScene(3);
    }

    public void MyQuit()
    {
        //if (UNITY_EDITOR) {UnityEditor.EditorApplication.isPlaying = false;} else { Application.Quit(); }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
                hoveredImage = imagesInMenu[buttonIndex];
                spriteBeforeHover = hoveredImage.sprite;
                
                if (spriteBeforeHover == buttonBehavior.imageSelected[buttonIndex])  // there are different hovering colors when button is selected or is not selected
                {
                    imagesInMenu[buttonIndex] = buttonBehavior.ChangeOfColorHoveredSelectedButton(imagesInMenu[buttonIndex], buttonIndex);
                }
                else 
                {
                    imagesInMenu[buttonIndex] = buttonBehavior.ChangeOfColorHoveredButton(imagesInMenu[buttonIndex], buttonIndex);
                }
            }
        }
    }

    public void
        OnPointerExit(
            PointerEventData eventData) // this function will run after exiting an object with cursor (responsivity/hovers)
    {
        if (wasClicked == false)
        {

            GameObject enteredObject = eventData.pointerEnter;

            Button enteredButton = enteredObject.GetComponent<Button>(); // button we unhovered

            if (hovered == true)
            {
                // only if we hovered on button
                if (enteredButton != null)
                {
                    hoveredImage.sprite = spriteBeforeHover;
                    hovered = false;
                }
            }
        }

        
    }
    
}
