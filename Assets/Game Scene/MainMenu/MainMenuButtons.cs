using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    // Game Objects
    public GameObject mainMenu;
    public GameObject mainMenuFirstSelected;

    public GameObject mainMenuLocal;
    public GameObject mainMenuLocalFirstSelected;

    public GameObject mainMenuOptions;
    public GameObject mainMenuOptionsFirstSelected;

    public GameObject mainMenuInfoAndControls;
    public GameObject mainMenuInfoAndControlsFirstSelected;

    void Start()
    {
        GetReferences();
        mainMenuLocal.SetActive(false);
        mainMenuOptions.SetActive(false);
        mainMenuInfoAndControls.SetActive(false);
    }
    // Main Menu
    public void DoMainMenuLocal()
    {
        mainMenu.SetActive(false);
        mainMenuLocal.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuLocalFirstSelected);
    }
    public void DoMainMenuOptions()
    {
        mainMenu.SetActive(false);
        mainMenuOptions.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuOptionsFirstSelected);
    }
    public void DoMainMenuQuit()
    {
        Application.Quit();
        Debug.Log("Application closed");
    }
    // Main Menu Local
    public void doMainMenuVersus()
    {
        SceneManager.LoadScene(1);
    }
    public void DoMainMenuSandbox()
    {
        SceneManager.LoadScene(2);
    }
    public void DoMainMenuLocalBack()
    {
        mainMenuLocal.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
    }

    // Main Menu Options
    public void DoMainMenuOptionsInfoAndControls()
    {
        mainMenuOptions.SetActive(false);
        mainMenuInfoAndControls.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuInfoAndControlsFirstSelected);
    }
    public void DoMainMenuOptionsBack()
    {
        mainMenu.SetActive(true);
        mainMenuOptions.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
    }

    // Main Menu Info And Controls
    public void DoMainMenuInfoAndControlsBack()
    {
        mainMenuInfoAndControls.SetActive(false);
        mainMenuOptions.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuOptionsFirstSelected);

    }



    
    private void GetReferences()
    {
        mainMenu = GameObject.Find("Main Menu");
        mainMenuFirstSelected = GameObject.Find("Local");
        mainMenuLocal = GameObject.Find("Main Menu Local");
        mainMenuLocalFirstSelected = GameObject.Find("Versus");
        mainMenuOptions = GameObject.Find("Main Menu Options");
        mainMenuOptionsFirstSelected = GameObject.Find("Info And Controls");
        mainMenuInfoAndControls = GameObject.Find("Main Menu Info And Controls");
        mainMenuInfoAndControlsFirstSelected = GameObject.Find("Info And Controls Back");
    }
}
