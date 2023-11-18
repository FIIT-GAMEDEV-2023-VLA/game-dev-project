using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    //, IPointerEnterHandler, IPointerExitHandler
{

    public Text buttonNewGameText; // I want to change color of text in button when selected/hovered (to obtain responsiveness)
    //public Text buttonContinueText; 
    //public Text buttonSettingsText; 
    //public Text buttonQuitText; 
    //public Button buttonNewGame;
    //public Button buttonContinue;
    //public Button buttonSettings;
    //public Button buttonQuit;
    
    
    public ButtonBehaviorScript buttonBehavior;

    public float delayBetweenChangedScene;

    
    
    private bool hovered = false;

    [SerializeField] public Button[] buttonsInMenu;
    [SerializeField] public Text[] textsInMenu;

    //private int hoveredOnButton = 0;
    public Color colorBeforeHover;
    public Text hoveredText;
    
    

    void Start()
    {
        buttonBehavior = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonBehaviorScript>();  // load of buttonBehavior script
        
        buttonsInMenu = new Button[4];  // I need to load some texts and buttons (editor in unity will not solve this for me)
        textsInMenu = new Text[4];
        
        GameObject buttonObject = GameObject.Find("ButtonNG");
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

    }

    void Update() // here I will solve some hovering on buttont (because I have invisible buttons and I want to have responsive text)
    {

        /*
        if (updateFlag == true)
        {
            if (hovered == true)
            {
                textsInMenu[hoveredOnButton] = buttonBehavior.ChangeOfColorHoveredButtonText(textsInMenu[hoveredOnButton]);
            }
            else
            {
                textsInMenu[hoveredOnButton].color = colorBeforeHover;
                
            }

            updateFlag = false;
        }
        */
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
    
    
    
    public void OnPointerEnter(PointerEventData eventData)  // function will run whenever cursor colides with object
    {
        GameObject enteredObject = eventData.pointerEnter;
        
        Button enteredButton = enteredObject.GetComponent<Button>();

        if (enteredButton != null)
        {
            int buttonIndex = System.Array.IndexOf(buttonsInMenu, enteredButton);
            
            Debug.Log(buttonsInMenu[3]+"    "+enteredButton);

            if (buttonIndex != -1)
            {
                hovered = true;
                hoveredText = textsInMenu[buttonIndex];
                colorBeforeHover = hoveredText.color;
                textsInMenu[buttonIndex] = buttonBehavior.ChangeOfColorHoveredButtonText(textsInMenu[buttonIndex]);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject enteredObject = eventData.pointerEnter;
        
        Button enteredButton = enteredObject.GetComponent<Button>();

        if (hovered==true) {
            if (enteredButton != null)
            {
                hoveredText.color = colorBeforeHover;
                hovered = false;
            }
        }
    }

    
    /*
    public void ButtonOnHover()
    {
        hovered = true;
        updateFlag = true;

        Vector2 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hoveredObject = hit.collider.gameObject;

            // Kontrola, či je objekt tlačidlo (Button)
            Button hoveredButton = hoveredObject.GetComponent<Button>();

            if (hoveredButton != null && buttonsInMenu.Contains(hoveredButton))
            {
                // Aktuálne hovernuté tlačidlo
                Debug.Log("Hovernuté tlačidlo: " + hoveredButton.name);
            }
    }


        //hoveredOnButton =
        //colorBeforeHover =
        
    }
    
    public void ButtonOffHover()
    {
        hovered = false;
        updateFlag = true;
    }
    */
    
}
        