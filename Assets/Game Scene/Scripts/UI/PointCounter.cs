using UnityEngine;

public class PointCounter : MonoBehaviour
{
    public int CurrentPoints = 0;
    public int MaxPoints = 2;
    public int PointNumber;

    public PlayerPoints1 playerPoints1;
    public PointSystem pointSystem;
    void Awake()
    {
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
    }
}
