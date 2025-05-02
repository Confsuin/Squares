using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlipaCard : MonoBehaviour
{
    // References
    public List<Card> SpawnedCards;

    // Floats
    private float NextCardToBeFlipped;

    public void FlipCard()
    {
        
    }
    public void IncreaseCardSize()
    {
        StartCoroutine(IncreaseCardSizeCoroutine());
    }
    public void DecreaseCardSize()
    {
        StartCoroutine(DecreaseCardSizeCoroutine());
    }
    IEnumerator IncreaseCardSizeCoroutine()
    {
        transform.localScale = new Vector2(1.066f, 1.066f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.132f, 1.132f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.2f, 1.2f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.266f, 1.266f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.32f, 1.32f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.4f, 1.4f);
    }
    IEnumerator DecreaseCardSizeCoroutine()
    {
        transform.localScale = new Vector2(1.332f, 1.332f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.266f, 1.266f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.2f, 1.2f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.132f, 1.132f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.066f, 1.066f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1f, 1f);
    }
    public void GetCards()
    {

    }
}
