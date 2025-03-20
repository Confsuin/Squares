using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints2 : MonoBehaviour
{
    public Slider Player2Point;
    public PointCounter pointCounter;
    void Start()
    {
        pointCounter = GetComponentInChildren<PointCounter>();
        Player2Point = GetComponent<Slider>();
        Player2Point.maxValue = pointCounter.MaxPoints;
        Player2Point.value = pointCounter.CurrentPoints;
    }
    public void SetPoints(int Points)
    {
        Player2Point.value = Points;
    }
}