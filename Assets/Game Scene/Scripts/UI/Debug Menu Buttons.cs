using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenuButtons : MonoBehaviour
{
    // References
    private GameObject DebugMenu;
    private GameObject DebugMenuButton;

    private Player1 player1;
    private Player2 player2;

    private CardSystemSpawner cardSystemSpawner;
    private PointSystem pointSystem;

    void Start()
    {
        GetReferences();
        DebugMenu.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
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
    public void SpawnCardsPlayer1()
    {
        pointSystem.Player2Won = true;
        cardSystemSpawner.DoSpawnCards();
    }
    public void SpawnCardsPlayer2()
    {
        pointSystem.Player1Won = true;
        cardSystemSpawner.DoSpawnCards();
    }
    public void KillPlayer1()
    {
        player1.TakeDamage(player1.currentHealth);
    }
    public void KillPlayer2()
    {
        player2.TakeDamage(player2.currentHealth);
    }
    public void RevivePlayers()
    {
        //player1.currentHealth = player1.currentHealth + player1.MissingHealth
        player1.TakeDamage(player1.MissingHealth = -player1.MissingHealth);
        player2.TakeDamage(player2.MissingHealth = -player2.MissingHealth);
        player1.MissingHealth = 0;
        player2.MissingHealth = 0;
    }
    private void GetReferences()
    {
        DebugMenu = GameObject.FindWithTag("Debug Menu");
        DebugMenuButton = GameObject.FindWithTag("Debug Menu Button");
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        cardSystemSpawner = GameObject.FindWithTag("Cards System").GetComponent<CardSystemSpawner>();
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
    }
}
