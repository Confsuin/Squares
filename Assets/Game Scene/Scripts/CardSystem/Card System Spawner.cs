using UnityEngine;
using UnityEngine.SceneManagement;
public class CardSystemSpawner : MonoBehaviour
{
    private PointSystem pointSystem;

    public GameObject CardSpawner;
    public GameObject CardsCanvasP1;
    public GameObject CardsCanvasP2;
    public GameObject Cards;

    public bool CardsSpawned = false;

    void Awake()
    {
        GetReferences();
    }

    public void DoSpawnCards()
    {
        if (CardsSpawned == false)
        {
            if (pointSystem.Player1Won == true)
            {
                Cards = Instantiate(CardSpawner, CardsCanvasP2.transform);
                CardsSpawned = true;
                Debug.Log("Cards Spawned");
            }
            else if (pointSystem.Player2Won == true)
            {
                Cards = Instantiate(CardSpawner, CardsCanvasP1.transform);
                CardsSpawned = true;
                Debug.Log("Cards Spawned");
            }
        }
        /*else
        {
            GameObject.Destroy(Cards);
            Cards = Instantiate(CardSpawner, CardsCanvas.transform);
            Debug.Log("Cards Rerolled");
        }*/
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            GameObject.Find("Close Cards Menu Text(SandBox Only)").SetActive(false);
        }
    }
    private void GetReferences()
    {
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
    }
}
