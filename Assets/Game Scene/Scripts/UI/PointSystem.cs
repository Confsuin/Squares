using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    public GameObject PointPlayer1;
    public GameObject PointPlayer2;
    public GameObject PointPlayer1Parent;
    public GameObject PointPlayer2Parent;
    public Transform Player1PointSpawnPoint;
    public Transform Player2PointSpawnPoint;
    public GameObject PointsToWinSlider;

    public PointCounter pointCounter;

    public Vector2 Player1Point;
    public Vector2 Player2Point;

    public float PointsToWin = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
        UpdatePointsToWin();
        Debug.Log(PointsToWinSlider);
    }

    public void UpdatePointsToWin()
    {
        PointsToWin = PointsToWinSlider.GetComponent<Slider>().value;
    }

    public void SetPoints()
    {
        for (int i = 0; i < PointsToWin; i++)
        {
            // Instantiate PointPlayer1 and PointPlayer2 at the Player1PointSpawnPoint's and Player2PointSpawnPoint's position and as child.
            GameObject g = Instantiate(PointPlayer1, Player1PointSpawnPoint.transform);
            GameObject h = Instantiate(PointPlayer2, Player2PointSpawnPoint.transform);

            // Increment PointPlayer1 and PointPlayer2 by 65x for each spawned point.
            g.transform.localPosition = new Vector2(65 * i, 0);
            h.transform.localPosition = new Vector2(65 * i, 0);

            // Gets the PointCounter Script
            pointCounter = g.GetComponent<PointCounter>();
            //h.GetComponent<PointCounter>();
            Debug.Log(pointCounter);
            //
            
        }
    }
}