using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using System.Collections;
public class GameBehaviour : MonoBehaviour
{
    private PlayerHealthBar player1HealthBar;
    private PlayerHealthBar player2HealthBar;

    public bool IsAEscapeMenuActive = false;
    public bool IsADebugMenuActive = false;
    void Start()
    {
        GetReferences();
        UpdatePlayerHealthBars();
        StartCoroutine(UpdatePlayerHealthBarsTimer());
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
        player1HealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
        player2HealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
    }
}
