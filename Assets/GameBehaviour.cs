using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameBehaviour : MonoBehaviour
{
    private Player1 player1;
    private Player2 player2;

    private PlayerHealthBar player1HealthBar;
    private PlayerHealthBar player2HealthBar;

    public bool IsAEscapeMenuActive = false;
    public bool IsADebugMenuActive = false;
    void Start()
    {
        GetReferences();
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            DisableEnablePlayerInput(0);
        }
        UpdatePlayerHealthBars();
        StartCoroutine(UpdatePlayerHealthBarsTimer());
        Time.timeScale = 1;
    }
    public void DisableEnablePlayerInput(int EnableDisable)
    {
        if (EnableDisable == 0)
        {
            player1.canDoActions = false;
            player2.canDoActions = false;
        }
        if (EnableDisable == 1)
        {
            player1.canDoActions = true;
            player2.canDoActions = true;
        }

    }
    public void UpdatePlayerHealthBars()
    {
        player1HealthBar.UpdateHealthBar();
        player2HealthBar.UpdateHealthBar();
    }
    IEnumerator UpdatePlayerHealthBarsTimer()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        UpdatePlayerHealthBars();
        StartCoroutine(UpdatePlayerHealthBarsTimer());
    }
    private void GetReferences()
    {
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1HealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
        player2HealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
    }
}
