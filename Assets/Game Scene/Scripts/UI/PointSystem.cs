using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class PointSystem : MonoBehaviour
{
    public GameObject PointPlayer1;
    public GameObject PointPlayer2;
    public GameObject PointPlayer1Parent;
    public GameObject PointPlayer2Parent;
    public GameObject PointsToWinSlider;


    public Transform Player1PointSpawnPoint;
    public Transform Player2PointSpawnPoint;
    

    public List<Slider> PointCounterSlidersPlayer1;
    public List<Slider> PointCounterSlidersPlayer2;
    public Slider CurrentPointCounterSliderPlayer1;
    public Slider CurrentPointCounterSliderPlayer2;
    

    public List<PointCounter> PointCounters1;
    public List<PointCounter> PointCounters2;
    

    public PointCounter CurrentPointCounterPlayer1;
    public PointCounter CurrentPointCounterPlayer2;
    public PointCounter pointCounter;


    public Player1 player1;
    public Player2 player2;

    public Vector2 Player1Point;
    public Vector2 Player2Point;

    public float PointsToWin = 1;
    public int TotalPlayer1Points;
    public int TotalPlayer2Points;

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

            // Gets the PointCounter Script, Set PointNumber as I + 1 and then add the PointCounter Script to list of all PointCounter Scripts
            pointCounter = g.GetComponentInChildren<PointCounter>();
            pointCounter.PointNumber = i + 1;
            PointCounters1.Add(pointCounter);
            pointCounter = h.GetComponentInChildren<PointCounter>();
            pointCounter.PointNumber = i + 1;
            PointCounters2.Add(pointCounter);

            // Adds Slider of PointPlayer1 and PointPlayer2 to PointCountersSliders List
            PointCounterSlidersPlayer1.Add(g.GetComponent<Slider>());
            PointCounterSlidersPlayer2.Add(h.GetComponent<Slider>());
            
        }
        CurrentPointCounterPlayer1 = PointCounters1[0];
        CurrentPointCounterPlayer2 = PointCounters2[0];
        CurrentPointCounterSliderPlayer1 = PointCounterSlidersPlayer1[0];
        CurrentPointCounterSliderPlayer2 = PointCounterSlidersPlayer2[0];
    }
    private int PointCounterListIndex1 = 0;
    private int PointCounterListIndex2 = 0;
    private void UpdatePoints()
    {
        if (CurrentPointCounterPlayer1.CurrentPoints == CurrentPointCounterPlayer1.MaxPoints)
        {
            CurrentPointCounterSliderPlayer1.value = CurrentPointCounterPlayer1.CurrentPoints;
            PointCounterListIndex1 += 1;
            CurrentPointCounterPlayer1 = PointCounters1[PointCounterListIndex1];
            CurrentPointCounterSliderPlayer1 = PointCounterSlidersPlayer1[PointCounterListIndex1];
        }
        else
        {
            CurrentPointCounterSliderPlayer1.value = CurrentPointCounterPlayer1.CurrentPoints;
        }

        if (CurrentPointCounterPlayer2.CurrentPoints == CurrentPointCounterPlayer2.MaxPoints)
        {
            CurrentPointCounterSliderPlayer2.value = CurrentPointCounterPlayer2.CurrentPoints;
            PointCounterListIndex2 += 1;
            CurrentPointCounterPlayer2 = PointCounters1[PointCounterListIndex2];
            CurrentPointCounterSliderPlayer2 = PointCounterSlidersPlayer2[PointCounterListIndex2];
        }
        else
        {
            CurrentPointCounterSliderPlayer2.value = CurrentPointCounterPlayer2.CurrentPoints;
        }
    }
    public void IncreasePoints()
    {
        if (player1.Player1Dead == true)
        {
            TotalPlayer1Points += 1;
            CurrentPointCounterPlayer1.CurrentPoints += 1;
            UpdatePoints();
        }
        if (player2.Player2Dead == true)
        {
            TotalPlayer2Points += 1;
            CurrentPointCounterPlayer2.CurrentPoints += 1;
            UpdatePoints();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdatePoints();
        }
    }
    private void FixedUpdate()
    {
        if (player1.Player1Dead == true)
        {
            IncreasePoints();
            player1.Player1Dead = false;
            player1.currentHealth = player1.maxHealth;
        }
        if (player2.Player2Dead == true)
        {
            IncreasePoints();
            player2.Player2Dead = false;
            player2.currentHealth = player2.maxHealth;
        }
    }

}