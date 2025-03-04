using UnityEngine;

public class CardSystemSpawner : MonoBehaviour
{
    public GameObject CardSpawner;
    public GameObject CardsCanvas;
    public bool CardsSpawned = false;
    public GameObject Cards;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
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
