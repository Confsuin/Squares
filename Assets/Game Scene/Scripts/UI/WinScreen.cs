using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreen : MonoBehaviour
{
    // References
    private PointSystem pointSystem;

    public GameObject winScreen;
    public TMP_Text[] WinScreenText;
    TMP_Text WinnerText;

    void Start()
    {
        GetReferences();
        winScreen.SetActive(false);
    }
    public void DoWinScreen()
    {
        ShowWinScreen();
        SetWinnerText();
    }
    private void SetWinnerText()
    {
        if (pointSystem.Player1Won == true)
        {
            WinnerText.text = "Player 1 Won the game!";
            foreach (TMP_Text text in WinScreenText)
            {
                text.color = new Color(0f, 1f, 0.1198683f);
            }
        }
        else if (pointSystem.Player2Won == true)
        {
            WinnerText.text = "Player 2 Won the game!";
            foreach (TMP_Text text in WinScreenText)
            {
                text.color = new Color(1f, 0f, 0.8144698f);
            }
        }
    }
    private void ShowWinScreen()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void GetReferences()
    {
        WinScreenText = GetComponentsInChildren<TMP_Text>();
        WinnerText = GetComponentInChildren<TMP_Text>();
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
        winScreen = GameObject.FindWithTag("Win Screen");
    }
}
