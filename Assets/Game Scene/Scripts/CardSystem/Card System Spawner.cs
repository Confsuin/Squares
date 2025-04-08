using UnityEngine;

public class CardSystemSpawner : MonoBehaviour
{
    public GameObject CardSpawner;
    public GameObject CardsCanvas;
    public GameObject Cards;

    public bool CardsSpawned = false;
    
    public void DoSpawnCards()
    {
        if (CardsSpawned == false)
        {
            Cards = Instantiate(CardSpawner, CardsCanvas.transform);
            CardsSpawned = true;
            Debug.Log("Cards Spawned");
        }
        else
        {
            GameObject.Destroy(Cards);
            Cards = Instantiate(CardSpawner, CardsCanvas.transform);
            Debug.Log("Cards Rerolled");
        }
    }
}
