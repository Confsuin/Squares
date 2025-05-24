using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    TMP_Text SquareOffCountdown;

    private GameObject SquareOff;
    private GameObject Player1;
    private GameObject Player2;
    private PlayerHealthBar player1HealthBar;
    private PlayerHealthBar player2HealthBar;

    public Player1 player1;
    public Player2 player2;

    private GameObject Player1Barrier;
    private GameObject Player2Barrier;

    private GameBehaviour gameBehaviour;

    public List<GameObject> Maps; 

    void Start()
    {
        GetReferences();
        Player1Barrier.SetActive(false);
        Player2Barrier.SetActive(false);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            SquareOff.SetActive(false);
        }
    }
    public void StartSquareOff()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            SelectMap();
            StartCoroutine(SquareOffCoolDown());
        }
    }
    public void SelectMap()
    {
        Player1Barrier.SetActive(true);
        Player2Barrier.SetActive(true);
        Player1.transform.position = new Vector2(-20, 0);
        Player2.transform.position = new Vector2(20, 0);

        Destroy(GameObject.FindWithTag("Map"));
        int n = Random.Range(0, Maps.Count);
        GameObject g = Instantiate(Maps[n], transform);
    }
    IEnumerator SquareOffCoolDown()
    {
        SquareOff.SetActive(true);
        SquareOffCountdown.text = "3!";
        yield return new WaitForSeconds(1f);
        SquareOffCountdown.text = "2!";
        yield return new WaitForSeconds(1f);
        SquareOffCountdown.text = "1!";
        yield return new WaitForSeconds(1f);
        SquareOffCountdown.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        SquareOff.SetActive(false);
        Player1Barrier.SetActive(false);
        Player2Barrier.SetActive(false);
        gameBehaviour.DisableEnablePlayerInput(1);
    }
    private void GetReferences()
    {
        gameBehaviour = GameObject.FindWithTag("Game Behaviour").GetComponent<GameBehaviour>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("PlayerAlt");
        player1HealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
        player2HealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
        Player1Barrier = GameObject.FindWithTag("Player 1 Barrier");
        Player2Barrier = GameObject.FindWithTag("Player 2 Barrier");
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            SquareOff = GameObject.FindWithTag("Square Off");
            SquareOffCountdown = GameObject.FindWithTag("Square Off Countdown").GetComponent<TMP_Text>();
        }
    }
}
