using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    // Intergars
    public int CardSpawnAmount;

    // List of GameObjects
    public List<GameObject> Cards = new();
    public List<GameObject> CardSpawnPoint = new();
    public GameObject pos;

    void Start()
    {
        SpawnCards();
    }

    void Update()
    {

    }
    // Picks random card and then spawns selected card at card spawn position doing so for every spawn position.
    public void SpawnCards()
    {
        for (int i = 0; i < CardSpawnAmount; i++)
        {
            pos = CardSpawnPoint[i];
            int n = Random.Range(0, Cards.Count);
            GameObject g = Instantiate(Cards[n], pos.transform);
            Cards.Remove(Cards[n]);
        }
    }
}
