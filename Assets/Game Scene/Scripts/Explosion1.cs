using UnityEngine;
using System.Collections;

public class Explosion1 : MonoBehaviour
{
    public float ExplosionDMG;

    public Player1 player1;
    public Player2 player2;
    public Player2Bullet bullet;

    private void Start()
    {
        Destroy(gameObject, 1);
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        bullet = GameObject.FindWithTag("BulletAlt").GetComponent<Player2Bullet>();

        ExplosionDMG = player2.ExplosionDMG;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            player1.TakeDamage(ExplosionDMG);
            Debug.Log("Boom1");
        }
        if (other.tag == ("PlayerAlt"))
        {
            player2.TakeDamage(ExplosionDMG);
            Debug.Log("Boom2");
        }
    }

    IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
