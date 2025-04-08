using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsToWinTextScript : MonoBehaviour
{
    TMP_Text PointsToWinText;
    
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
