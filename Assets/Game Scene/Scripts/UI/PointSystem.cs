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
            // Instantiate PointPlayer1 and PointPlayer2 at the Player1PointSpawnPoint's and Player2PointSpawnPoint's position
            GameObject g = Instantiate(PointPlayer1, Player1PointSpawnPoint.position, Quaternion.identity);
            GameObject h = Instantiate(PointPlayer2, Player2PointSpawnPoint.position, Quaternion.identity);

            // Increment Player1Point.x and Player2Point.x for spacing purposes
            Player1Point.x += (65);
            Player2Point.x += (65);

            // Update the spawn point position for the next object
            Player1PointSpawnPoint.position = Player1Point;
            Player2PointSpawnPoint.position = Player2Point;

            // Set the parent of the instantiated object to PointPlayer1Parent and PointPlayer2Parent
            g.transform.SetParent(PointPlayer1Parent.transform);
            h.transform.SetParent(PointPlayer2Parent.transform);
        }
    }
}