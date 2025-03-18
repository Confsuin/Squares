using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuButtons : MonoBehaviour
{
    public bool EscapeMenuActive = false;
    public GameObject escapeMenu;
    
    void Start()
    {
        escapeMenu = GameObject.Find("EscapeMenu");
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
    public void doEscapeMenu()
    {
        escapeMenu.SetActive(true);
    }
    public void doResume()
    {
        escapeMenu.SetActive(false);
    }
    public void doMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
