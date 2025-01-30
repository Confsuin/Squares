using UnityEngine;

public class ParticleCollisionDetection : MonoBehaviour
{
    public Player1 player1;
    public Player2 player2;

    private void OnParticleCollision (GameObject other)
    {
        if(other.tag == ("Player"))
        {
            player1.TakeDamage(25);
            Debug.Log("hit");
        }
        if (other.tag == ("PlayerAlt"))
        {
            player2.TakeDamage(25);
            Debug.Log("hit");
        }
    }
}