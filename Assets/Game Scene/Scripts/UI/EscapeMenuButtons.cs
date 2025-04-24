using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuButtons : MonoBehaviour
{
    public bool EscapeMenuActive = false;
    public GameObject escapeMenu;
    
    void Start()
    {
        GetReferences();
        escapeMenu.SetActive(false);
    }
    public void EscapeMenu()
    {
        if (EscapeMenuActive == false)
        {
            EscapeMenuActive = true;
            escapeMenu.SetActive(true);
        }
        else
        {
            EscapeMenuActive = false;
            escapeMenu.SetActive(false);
        }
    }
    public void Resume()
    {
        escapeMenu.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void GetReferences()
    {
        escapeMenu = GameObject.FindWithTag("Escape Menu");
    }
}
