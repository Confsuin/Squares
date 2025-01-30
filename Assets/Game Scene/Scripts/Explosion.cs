using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Player1 player1;
    public Player2 player2;

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
}
