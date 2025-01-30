using UnityEngine;

public class FireZone : MonoBehaviour
{
    public Player1 player1;
    public Player2 player2;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            player1.TakeDamage(1);
            Debug.Log("fire1");
        }
        if (other.tag == ("PlayerAlt"))
        {
            player2.TakeDamage(1);
            Debug.Log("fire2");
        }
    }
}
