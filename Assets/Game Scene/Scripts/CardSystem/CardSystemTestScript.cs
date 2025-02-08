using UnityEngine;

public class CardSystemTestScript : MonoBehaviour
{
    public GameObject CardSpawner;
    public GameObject CardsCanvas;

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
        Instantiate(CardSpawner, CardsCanvas.transform);
    }
}
