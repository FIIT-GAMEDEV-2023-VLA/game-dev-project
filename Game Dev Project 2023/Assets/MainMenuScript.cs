using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text buttonNewGameText; // I want to change color of text in button when selected/hovered (to obtain responsiveness)
    public Text buttonContinueText; 
    public Text buttonSettingsText; 
    public Text buttonQuitText; 
    public Button buttonNewGame;
    public Button buttonContinue;
    public Button buttonSettings;
    public Button buttonQuit;
    
    
    public ButtonBehaviorScript buttonBehavior;

    public float delayBetweenChangedScene;

    
    
    private bool hovered = false;
    //private bool updateFlag = false;

    public Button[] buttonsInMenu;
    public Text[] textsInMenu;

    //private int hoveredOnButton = 0;
    public Color colorBeforeHover;
    public Text hoveredText;
    
    

    void Start()
    {
        buttonBehavior = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonBehaviorScript>();

        
        Debug.Log(buttonNewGameText+"ng tlacitko     45555");
        
        
        buttonsInMenu[0] = buttonNewGame; buttonsInMenu[1] = buttonContinue; buttonsInMenu[2] = buttonSettings; buttonsInMenu[3] = buttonQuit;
        textsInMenu[0] = buttonNewGameText; textsInMenu[1] = buttonContinueText; textsInMenu[2] = buttonSettingsText; textsInMenu[3] = buttonQuitText; 
            
        Debug.Log(buttonsInMenu+"hah");

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
        Debug.Log("Entered object");
        GameObject enteredObject = eventData.pointerEnter;
        Debug.Log("Kurzor vstúpil na objekt: " + enteredObject.name);
        
        Button enteredButton = enteredObject.GetComponent<Button>();

        if (enteredButton != null)
        {
            Debug.Log("Entered button");
            //hovered = true;
            //updateFlag = true;

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
                Debug.Log("Exit works?1");
                hoveredText.color = colorBeforeHover;
                Debug.Log("Exit works?2");
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
        