using UnityEngine;
using System.Collections;

public class Player1Bullet : MonoBehaviour
{
    public float damage;
    public float lifesteal;
    public float poison;
    public float speed;
    public float size;
    public float slow;
    public int timesBounced;
    public float bounce;
    public float fire;
    public float explosion;

    public GameObject explosionEffect;
    public GameObject fireZoneEffect;

    private Player1 player1;
    private Player2 player2;

    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        

        //Get Stats From player 1
        damage = player1.Damage;
        lifesteal = player1.BulletLifeSteal;
        poison = player1.BulletPoison;
        speed = player1.BulletSpeed;
        size = player1.BulletSize;
        slow = player1.BulletSlow;
        bounce = player1.BulletBounces;
        fire = player1.FireRadius;
        explosion = player1.ExplosionDMG;

        rb2d.AddForce(transform.right * speed);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player1.TakeDamage(damage);
            Debug.Log("hit Player");
            Destroy(gameObject);
            if (poison > 0)
            {
                player1.IsPoisoned = true;
                player1.StartPoisonTimer();
            }
        }
        if (other.gameObject.tag == "PlayerAlt")
        {
            player2.TakeDamage(damage);
            Debug.Log("hit PlayerAlt");
            Destroy(gameObject);
            if (poison > 0)
            {
                player2.IsPoisoned = true;
                player2.StartPoisonTimer();
            }
        }
        if (other.gameObject.tag == "Wall")
        {
            timesBounced += 1;
        }
        if (other.gameObject.tag == "Wall" && timesBounced >= bounce)
        {
            Destroy(gameObject);
            Debug.Log("Bounce depleted");
        }
    }

    public void OnDestroy()
    {
        if (explosion > 0)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        if (fire > 0)
        {
            Instantiate(fireZoneEffect, transform.position, Quaternion.identity);
        }
    }
}