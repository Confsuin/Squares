using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    // Intergars
    public int CardSpawnAmount;

    // List of Cards
    public List<GameObject> Cards = new();
    public List<GameObject> CardSpawnPoint = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnCardsPlayer1()
    {
        for (int i = 0; i < CardSpawnAmount; i++)
        {
            /*index = Random.Range(0, CardSpawnPoint.Count);
            objActual = CardSpawnPoint[index];
            CardSpawnPoint.Remove(objActual);
            CreateCard();*/
        }
    }
    private void SpawnCardsPlayer2()
    {

    }
    public void CreateCard()
    {
        //Instantiate(Cards, objActual.transform.position, objActual.transform.rotation);
    }
}
