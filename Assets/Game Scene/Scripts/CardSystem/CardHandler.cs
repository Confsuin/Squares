using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class CardHandler : MonoBehaviour
{
    // Intergars
    public int CardSpawnAmount;

    // List of References
    public List<GameObject> Cards = new();
    public List<GameObject> CardSpawnPoint = new();
    public GameObject pos;

    private PointSystem pointSystem;

    public TMP_Text XIspickingtext;

    private void Awake()
    {
        GetReferences();
        StartCoroutine(WaitForReference());
        SpawnCards();
        UpdateIsPickingText();
    }
    // Picks random card and then spawns selected card at card spawn position doing so for every spawn position.
    public void SpawnCards()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for (int i = 0; i < CardSpawnAmount; i++)
        {
            pos = CardSpawnPoint[i];
            int n = Random.Range(0, Cards.Count);
            GameObject g = Instantiate(Cards[n], pos.transform);
            g.GetComponent<Card>().CardNumber = i + 1;
            if (i == 0)
            {
                g.gameObject.tag = "FirstCardToSelect";
                if (pointSystem.Player1Won == true)
                {
                    EventSystem.current = GameObject.FindWithTag("Player 2 Event System").GetComponent<EventSystem>();
                    EventSystem.current.SetSelectedGameObject(g);
                }
                if (pointSystem.Player2Won == true)
                {
                    EventSystem.current = GameObject.FindWithTag("Player 1 Event System").GetComponent<EventSystem>();
                    EventSystem.current.SetSelectedGameObject(g);
                }
            }
            Cards.Remove(Cards[n]);
        }
    }
    private void UpdateIsPickingText()
    {
        if (pointSystem.Player1Won == true)
        {
            XIspickingtext.text = "Player 2 is selecting a card.";
        }
        if (pointSystem.Player2Won == true)
        {
            XIspickingtext.text = "Player 1 is selecting a card.";
        }
    }
    private void GetReferences()
    {
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
        XIspickingtext = GameObject.FindWithTag("X Is picking text").GetComponent<TMP_Text>();
    }
    IEnumerator WaitForReference()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        GetReferences();
        UpdateIsPickingText();
    }
}
