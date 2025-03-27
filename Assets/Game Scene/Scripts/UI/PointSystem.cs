using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

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
    

    public List<PointCounter> PointCounters1;
    public List<PointCounter> PointCounters2;
    

    public PointCounter pointCounter;


    public Player1 player1;
    public Player2 player2;


    public LevelManager levelManager;
    public CardSystemSpawner cardSystemSpawner;


    public Vector2 Player1Point;
    public Vector2 Player2Point;

    public float PointsToWin = 1;
    public int TotalPlayer1Points;
    public int TotalPlayer2Points;


    public bool Player1Won = false;
    public bool Player2Won = false;


    void Start()
    {
        Time.timeScale = 0;
        GetReferences();
        GameObject.FindWithTag("Player").SetActive(false);
        GameObject.FindWithTag("PlayerAlt").SetActive(false);
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
            pointCounter.PointNumber = i * 2;
            PointCounters1.Add(pointCounter);
            pointCounter = h.GetComponentInChildren<PointCounter>();
            pointCounter.PointNumber = i * 2;
            PointCounters2.Add(pointCounter);

            // Adds Slider of PointPlayer1 and PointPlayer2 to PointCountersSliders List
            PointCounterSlidersPlayer1.Add(g.GetComponent<Slider>());
            PointCounterSlidersPlayer2.Add(h.GetComponent<Slider>());
            
        }
    }
    private void UpdatePoints()
    {
        foreach (PointCounter pointCounter in PointCounters1)
        {
            pointCounter.CurrentPoints = Mathf.Clamp(TotalPlayer1Points - pointCounter.PointNumber, 0, 2);
            pointCounter.GetComponentInParent<Slider>().value = pointCounter.CurrentPoints;
            ResetPlayersHealth();
        }
        foreach (PointCounter pointCounter in PointCounters2)
        {
            pointCounter.CurrentPoints = Mathf.Clamp(TotalPlayer2Points - pointCounter.PointNumber, 0, 2);
            pointCounter.GetComponentInParent<Slider>().value = pointCounter.CurrentPoints;
            ResetPlayersHealth();
        }
    }
    public int MaxPointsPerSquare = 2;
    public void IncreasePoints()
    {
        if (player1.Player1Dead == true)
        {
            ResetPlayersHealth();

            player1.Player1Dead = false;
            Player2Won = true;

            TotalPlayer2Points += 1;
            UpdatePoints();
            if (TotalPlayer2Points % MaxPointsPerSquare == 0)
            {
                cardSystemSpawner.DoSpawnCards();
                if (TotalPlayer1Points % MaxPointsPerSquare != 0)
                {
                    TotalPlayer1Points -= (TotalPlayer1Points % MaxPointsPerSquare);
                    ResetPlayersHealth();
                }
            }
            else
            {
                levelManager.StartSquareOff();
                Player2Won = false;
                ResetPlayersHealth();
            }
            UpdatePoints();
        }
        if (player2.Player2Dead == true)
        {
            ResetPlayersHealth();

            player2.Player2Dead = false;
            Player1Won = true;

            TotalPlayer1Points += 1;
            UpdatePoints();
            if (TotalPlayer1Points % MaxPointsPerSquare == 0)
            {
                cardSystemSpawner.DoSpawnCards();
                if (TotalPlayer2Points % MaxPointsPerSquare != 0)
                {
                    TotalPlayer2Points -= (TotalPlayer2Points % MaxPointsPerSquare);
                    ResetPlayersHealth();
                }
            }
            else
            {
                levelManager.StartSquareOff();
                Player1Won = false;
                ResetPlayersHealth();
            }
            UpdatePoints();
        }
    }
    private void FixedUpdate()
    {
        if (player1.Player1Dead || player2.Player2Dead == true)
        {
            IncreasePoints();
        }
    }
    private void GetReferences()
    {
        levelManager = GameObject.FindWithTag("Level Manager").GetComponent<LevelManager>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        cardSystemSpawner = GameObject.FindWithTag("Cards System").GetComponent<CardSystemSpawner>();
        PointsToWinSlider = GameObject.FindWithTag("Points To Win Slider");
    }
    private void ResetPlayersHealth()
    {
        player1.currentHealth = player1.maxHealth;
        player2.currentHealth = player2.maxHealth;
    }
}