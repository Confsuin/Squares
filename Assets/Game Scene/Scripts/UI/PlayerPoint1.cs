using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints1 : MonoBehaviour
{
    public Slider Player1Point;
    public PointCounter pointCounter;
    void Start()
    {
        pointCounter = GetComponentInChildren<PointCounter>();
        Player1Point = GetComponent<Slider>();
        Player1Point.maxValue = pointCounter.MaxPoints;
        Player1Point.value = pointCounter.CurrentPoints;
    }
    public void SetPoints(int Points)
    {
        Player1Point.value = Points;
    }
}