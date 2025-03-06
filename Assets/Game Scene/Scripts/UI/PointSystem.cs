using UnityEngine;
using UnityEngine.UI;


public class PointSystem : MonoBehaviour
{
    public GameObject Pointplayer1;
    public GameObject Pointplayer2;
    public GameObject Player1PointSpawnPoint;
    public GameObject Player2PointSpawnPoint;
    public GameObject PointsToWinSlider;


    public float PointsToWin = 1;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
        Debug.Log(PointsToWinSlider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdatePointsToWin()
    {
        PointsToWin = PointsToWinSlider.GetComponent<Slider>().value;
    }
    public void SetPoints()
    {
        for (int i = 0; i < PointsToWin; i++)
        {
            GameObject g = Instantiate(Pointplayer1, Player1PointSpawnPoint.transform);
            GameObject h = Instantiate(Pointplayer2, Player2PointSpawnPoint.transform);
            Player1PointSpawnPoint.transform.localPosition = new Vector2(65, 0);
        }
    }
}
