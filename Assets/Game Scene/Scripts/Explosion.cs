using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public Player1 player1;
    public Player2 player2;

    private void Start()
    {
        Destroy(gameObject, 1);
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            player1.TakeDamage(10);
            Debug.Log("Boom1");
        }
        if (other.tag == ("PlayerAlt"))
        {
            player2.TakeDamage(10);
            Debug.Log("Boom2");
        }
    }

    IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
