using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DebugMenuButtons : MonoBehaviour
{
    // References
    public GameObject debugMenu;
    public GameObject firstSelected;

    private Player1 player1;
    private Player2 player2;

    public GameBehaviour gameBehaviour;
    private CardSystemSpawner cardSystemSpawner;
    private PointSystem pointSystem;

    // Bools
    public bool IsMenuActive;

    void Start()
    {
        debugMenu.SetActive(false);
        var parentGameObject = this.transform.root.gameObject;
        Debug.Log(parentGameObject);
    }
    private void Awake()
    {
        GetReferences();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ToggleDebugMenu()
    {
        if (gameBehaviour.IsADebugMenuActive == false && gameBehaviour.IsAEscapeMenuActive == false && gameBehaviour.IsCardsMenuActive == false)
        {
            if (IsMenuActive == false)
            {
                gameBehaviour.IsADebugMenuActive = true;
                IsMenuActive = true;
                debugMenu.SetActive(true);
            }
            if (player1.OpenedByPlayer1 == true)
            {
                EventSystem.current = GameObject.FindWithTag("Player 1 Event System").GetComponent<EventSystem>();
                EventSystem.current.SetSelectedGameObject(firstSelected);
                player1.OpenedByPlayer1 = false;
                Debug.Log("Debug Menu opened by Player 1");
            }
            if (player2.OpenedByPlayer2 == true)
            {
                EventSystem.current = GameObject.FindWithTag("Player 2 Event System").GetComponent<EventSystem>();
                EventSystem.current.SetSelectedGameObject(firstSelected);
                player2.OpenedByPlayer2 = false;
                Debug.Log("Debug Menu opened by Player 2");
            }

        }
        else if (gameBehaviour.IsADebugMenuActive == true && IsMenuActive == true && gameBehaviour.IsCardsMenuActive == false)
        {
            gameBehaviour.IsADebugMenuActive = false;
            IsMenuActive = false;
            debugMenu.SetActive(false);
        }
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
        player1.TakeDamage(player1.MissingHealth = -player1.MissingHealth);
        player2.TakeDamage(player2.MissingHealth = -player2.MissingHealth);
        player1.MissingHealth = 0;
        player2.MissingHealth = 0;
    }
    private void GetReferences()
    {
        gameBehaviour = GameObject.FindWithTag("Game Behaviour").GetComponent<GameBehaviour>();
        var parentGameObject = this.transform.root.gameObject;
        if (parentGameObject.tag == ("Player"))
        {
            debugMenu = GameObject.FindWithTag("Player 1 Debug Menu");
        }
        else if (parentGameObject.tag == ("PlayerAlt"))
        {
            debugMenu = GameObject.FindWithTag("Player 2 Debug Menu");
        }
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        cardSystemSpawner = GameObject.FindWithTag("Cards System").GetComponent<CardSystemSpawner>();
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
    }
}
