using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsToWinTextScript : MonoBehaviour
{
    TMP_Text PointsToWinText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointsToWinText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetSliderValue(float sliderValue)
    {
        PointsToWinText.text = Mathf.Round(sliderValue).ToString();
    }
}
