using UnityEngine;
using System.Collections;

public class Player2Bullet : MonoBehaviour
{
    public float damage;
    public float lifesteal;
    public float poison;
    public float speed;
    public float size;
    public float slow;
    public float timesBounced;
    public float bounce;
    public float fire;
    public float explosion;

    public bool isBounced = false;
    public float TimesBouncedTimer = 0f;
    public float BouncedTimerSpeed = 0.005f;

    public GameObject explosionEffect;
    public GameObject fireZoneEffect;

    private Player1 player1;
    private Player2 player2;
    public TrainingDummy trainingDummy;

    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetRefrences();

        //Get Stats From Player2
        damage = player2.Damage;
        lifesteal = player2.BulletLifeSteal;
        poison = player2.BulletPoison;
        speed = player2.BulletSpeed;
        size = player2.BulletSize;
        slow = player2.BulletSlow;
        bounce = player2.BulletBounces;
        fire = player2.FireRadius;
        explosion = player2.ExplosionDMG;

        rb2d.AddForce(transform.right * speed);
    }
    private void FixedUpdate()
    {
        if (isBounced == true)
        {
            TimesBouncedTimer -= Time.deltaTime;

            if (TimesBouncedTimer >= BouncedTimerSpeed)
            {
                isBounced = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player1.TakeDamage(damage);
            Debug.Log("Hit Player");
            Destroy(gameObject);
            if (poison > 0)
            {
                player1.StartPoisonTimerP2DMG();
            }
        }
        if (other.gameObject.tag == "PlayerAlt")
        {
            player2.TakeDamage(damage);
            Debug.Log("Hit PlayerAlt");
            Destroy(gameObject);
            if (poison > 0)
            {
                player2.StartPoisonTimerP2DMG();
            }
        }
        if (other.gameObject.tag == "Training Dummy")
        {
            trainingDummy.TakeDamage(damage);
            Debug.Log("Hit TrainingDummy");
            Destroy(gameObject);
            if (poison > 0)
            {
                trainingDummy.StartPoisonTimerP2DMG();
            }
        }
        if (other.gameObject.tag == "Wall")
        {
            if (TimesBouncedTimer <= 0)
            {
                isBounced = true;
                TimesBouncedTimer = BouncedTimerSpeed;
                timesBounced += 1f;
            }
        }
        if (other.gameObject.tag == "Wall" && timesBounced >= bounce)
        {
            Destroy(gameObject);
            Debug.Log("Bounce depleted");
        }



        if (explosion > 0 && other.gameObject.tag == "Wall")
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
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




    private void GetRefrences()
    {
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        trainingDummy = GameObject.FindWithTag("Training Dummy").GetComponent<TrainingDummy>();
    }
}