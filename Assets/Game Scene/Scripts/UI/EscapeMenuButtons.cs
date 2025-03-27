using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuButtons : MonoBehaviour
{
    public bool EscapeMenuActive = false;
    public GameObject escapeMenu;
    
    void Start()
    {
        escapeMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscapeMenuActive == false)
            {
                escapeMenu.SetActive(true);
                EscapeMenuActive = true;
            }
            if (EscapeMenuActive == true)
            {
                escapeMenu.SetActive(false);
                EscapeMenuActive = false;
            }
        }
    }
    public void EscapeMenu()
    {
        escapeMenu.SetActive(true);
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
        escapeMenu = GameObject.Find("EscapeMenu");
    }
}
