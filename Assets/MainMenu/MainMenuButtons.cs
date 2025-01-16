using Unity.VisualScripting;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    // Game Objects
    public GameObject mainMenu;
    public GameObject mainMenuLocal;
    public GameObject mainMenuOptions;
    void Start()
    {
        mainMenu = GameObject.Find("Main Menu");
        mainMenuLocal = GameObject.Find("Main Menu Local");
        mainMenuOptions = GameObject.Find("Main Menu Options");
        mainMenuLocal.SetActive(false);
        mainMenuOptions.SetActive(false);
    }

    void Update()
    {

    }
    // Main Menu
    public void DoMainMenuLocal()
    {
        mainMenu.SetActive(false);
        mainMenuLocal.SetActive(true);
    }
    public void DoMainMenuOptions()
    {
        mainMenu.SetActive(false);
        mainMenuOptions.SetActive(true);
    }
    public void DoMainMenuQuit()
    {
        Application.Quit();
        Debug.Log("Application closed");
    }
    // Main Menu Local
    public void doMainMenu()
    {

    }
    public void DoMainMenuOptionsSandbox()
    {

    }
    public void DoMainMenuLocalBack()
    {
        mainMenuLocal.SetActive(false);
        mainMenu.SetActive(true);
    }
 
    // Main Menu Options
    public void DoMainMenuOptionsBack()
    {
        mainMenu.SetActive(true);
        mainMenuOptions.SetActive(false);
    }
}
