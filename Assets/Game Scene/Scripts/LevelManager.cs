using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    TMP_Text SquareOffCountdown;

    private GameObject SquareOff;
    private GameObject Player1;
    private GameObject Player2;
    private PlayerHealthBar player1HealthBar;
    private PlayerHealthBar player2HealthBar;

    public List<GameObject> Maps; 

    void Start()
    {
        GetReferences();
        SquareOff.SetActive(false);
    }
    public void StartSquareOff()
    {
        Time.timeScale = 0;
        
        SelectMap();
        StartCoroutine(SquareOffCoolDown());
    }
    public void SelectMap()
    {
        Player1.transform.position = new Vector2(-20, 0);
        Player2.transform.position = new Vector2(20, 0);

        Destroy(GameObject.FindWithTag("Map"));
        int n = Random.Range(0, Maps.Count);
        GameObject g = Instantiate(Maps[n], transform);
    }
    IEnumerator SquareOffCoolDown()
    {
        player1HealthBar.UpdateHealthBar();
        player2HealthBar.UpdateHealthBar();
        SquareOff.SetActive(true);
        SquareOffCountdown.text = "3!";
        yield return new WaitForSecondsRealtime(1f);
        SquareOffCountdown.text = "2!";
        yield return new WaitForSecondsRealtime(1f);
        SquareOffCountdown.text = "1!";
        yield return new WaitForSecondsRealtime(1f);
        SquareOffCountdown.text = "Go!";
        yield return new WaitForSecondsRealtime(0.5f);
        SquareOff.SetActive(false);
        Time.timeScale = 1;
    }
    private void GetReferences()
    {
        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("PlayerAlt");
        player1HealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
        player2HealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
        SquareOff = GameObject.FindWithTag("Square Off");
        SquareOffCountdown = GameObject.FindWithTag("Square Off Countdown").GetComponent<TMP_Text>();
    }
}
