using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EscapeMenuButtons : MonoBehaviour
{
    public bool EscapeMenuActive = false;
    public bool ShouldUnpause = true;

    public GameObject escapeMenu;
    public GameObject firstSelected;

    public GameBehaviour gameBehaviour;

    private Player1 player1;
    private Player2 player2;

    void Start()
    {
        GetReferences();
        escapeMenu.SetActive(false);
    }
    public void ToggleEscapeMenu()
    {
        if (gameBehaviour.IsAEscapeMenuActive == false && gameBehaviour.IsCardsMenuActive == false)
        {
            if (EscapeMenuActive == false)
            {
                if (Time.timeScale <= 0)
                {
                    ShouldUnpause = false;
                }
                Time.timeScale = 0;
                gameBehaviour.IsAEscapeMenuActive = true;
                EscapeMenuActive = true;
                escapeMenu.SetActive(true);
            }
            if (player1.OpenedByPlayer1 == true)
            {
                EventSystem.current = GameObject.FindWithTag("Player 1 Event System").GetComponent<EventSystem>();
                EventSystem.current.SetSelectedGameObject(firstSelected);
            }
            if (player2.OpenedByPlayer2 == true)
            {
                EventSystem.current = GameObject.FindWithTag("Player 2 Event System").GetComponent<EventSystem>();
                EventSystem.current.SetSelectedGameObject(firstSelected);
            }

        }
        else if (gameBehaviour.IsAEscapeMenuActive == true && EscapeMenuActive == true && gameBehaviour.IsCardsMenuActive == false)
        {
            if (ShouldUnpause == true)
            {
                Time.timeScale = 1;
            }
            gameBehaviour.IsAEscapeMenuActive = false;
            EscapeMenuActive = false;
            escapeMenu.SetActive(false);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void GetReferences()
    {
        gameBehaviour = GameObject.FindWithTag("Game Behaviour").GetComponent<GameBehaviour>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        escapeMenu = GameObject.FindWithTag("Escape Menu");
    }
}
