using UnityEngine;
using System.Collections;

public class PlaySettingsButtons : MonoBehaviour
{
    public GameObject PlaySettings;
    public GameObject PointsToWinSlider;
    void Awake()
    {
        GetReferences();
    }
    public void SelectSlider()
    {
        StartCoroutine(IncreaseSliderSizeCoroutine());
    }
    public void DeSelectSlider()
    {
        StartCoroutine(DecreaseSliderSizeCoroutine());
    }
    public void StartGame()
    {
        PlaySettings.SetActive(false);
    }
    IEnumerator IncreaseSliderSizeCoroutine()
    {
        PointsToWinSlider.transform.localScale = new Vector2(3.2f, 3.2f);
        yield return new WaitForSecondsRealtime(0.02f);
        PointsToWinSlider.transform.localScale = new Vector2(3.4f, 3.4f);
        yield return new WaitForSecondsRealtime(0.02f);
        PointsToWinSlider.transform.localScale = new Vector2(3.6f, 3.8f);
        yield return new WaitForSecondsRealtime(0.02f);
        PointsToWinSlider.transform.localScale = new Vector2(3.8f, 3.8f);
        yield return new WaitForSecondsRealtime(0.02f);
        PointsToWinSlider.transform.localScale = new Vector2(4f, 4f);
    }
    IEnumerator DecreaseSliderSizeCoroutine()
    {
        PointsToWinSlider.transform.localScale = new Vector2(3.8f, 3.8f);
        yield return new WaitForSecondsRealtime(0.02f);
        PointsToWinSlider.transform.localScale = new Vector2(3.6f, 3.6f);
        yield return new WaitForSecondsRealtime(0.02f);
        PointsToWinSlider.transform.localScale = new Vector2(3.4f, 3.4f);
        yield return new WaitForSecondsRealtime(0.02f);
        PointsToWinSlider.transform.localScale = new Vector2(3.2f, 3.4f);
        yield return new WaitForSecondsRealtime(0.02f);
        PointsToWinSlider.transform.localScale = new Vector2(3f, 3f);
    }
    private void GetReferences()
    {
        PointsToWinSlider = GameObject.FindWithTag("Points To Win Slider");
    }
}
