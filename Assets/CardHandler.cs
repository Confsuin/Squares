using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    // Intergars
    public int CardSpawnAmount;

    

    // List of Cards
    public List<GameObject> Cards = new();
    public List<Transform> CardSpawnPoint = new();
    public Transform pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCardsPlayer1();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnCardsPlayer1()
    {
        for (int i = 0; i < CardSpawnAmount; i++)
        {
            pos = CardSpawnPoint[i];
            int n = Random.Range(0, Cards.Count);
            GameObject g = Instantiate(Cards[n]);
            Debug.Log(g.transform.parent);
            g.transform.SetParent(pos, false);
            Debug.Log(g.transform.parent);
        }

    }
    private void SpawnCardsPlayer2()
    {

    }
    public void CreateCard()
    {
        
    }
}
