using UnityEngine;
using System.Collections;

public class Explosion1 : MonoBehaviour
{
    public float ExplosionDMG;

    public Player1 player1;
    public Player2 player2;

    private void Start()
    {
        Destroy(gameObject, 0.5f);
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();

        ExplosionDMG = player2.ExplosionDMG;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == ("Player"))
        {
            player1.TakeDamage(ExplosionDMG);
        }
        if (other.tag == ("PlayerAlt"))
        {
            player2.TakeDamage(ExplosionDMG);
        }
    }
}
