using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class FlipaCard : MonoBehaviour
{
    // References
    public List<Card> SpawnedCards;

    public Button ElementButton;
    Navigation customNav = new Navigation();

    // Floats
    private int NextCardToBeFlipped;

    private void Awake()
    {
        GetReferences();
    }
    public void FlipCard()
    {
        //SpawnedCards[NextCardToBeFlipped].MoveToSpawnPoint();
        foreach (TMP_Text text in SpawnedCards[NextCardToBeFlipped].CardText)
        {
            text.enabled = true;
        }
        NextCardToBeFlipped += 1;
        if (NextCardToBeFlipped >= SpawnedCards.Count)
        {
            EventSystem.current.SetSelectedGameObject(GameObject.FindWithTag("FirstCardToSelect"));
            customNav.mode = Navigation.Mode.None;
            ElementButton.navigation = customNav;
        }
    }
    public void GetReferences()
    {
        ElementButton = GetComponent<Button>();
    }
}
