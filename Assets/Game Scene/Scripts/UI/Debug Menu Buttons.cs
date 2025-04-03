using UnityEngine;

public class DebugMenuButtons : MonoBehaviour
{
    private GameObject DebugMenu;
    private GameObject DebugMenuButton;
    private Player1 player1;
    private Player2 player2;
    void Start()
    {
        GetReferences();
        DebugMenu.SetActive(false);
    }
    public void OpenDebugMenu()
    {
        DebugMenu.SetActive(true);
        DebugMenuButton.SetActive(false);
    }
    public void CloseDebugMenu()
    {
        DebugMenu.SetActive(false);
        DebugMenuButton.SetActive(true);
    }
    public void KillPlayer1()
    {
        player1.TakeDamage(player1.maxHealth);
    }
    public void KillPlayer2()
    {
        player2.TakeDamage(player2.maxHealth);
    }
    private void GetReferences()
    {
        DebugMenu = GameObject.FindWithTag("Debug Menu");
        DebugMenuButton = GameObject.FindWithTag("Debug Menu Button");
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
    }
}
