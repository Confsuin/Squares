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
    public float timesBounced;
    public float bounce;
    public float fire;
    public float explosion;
    public float range;
    public float rangeTime;

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
        range = player1.Range;

        rb2d.AddForce(transform.right * speed);
        rangeTime = range;
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
        rangeTime -= Time.deltaTime;

        if (rangeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player1.TakeDamage(damage);

            Destroy(gameObject);
            if (poison > 0)
            {
                player1.StartPoisonTimerP1DMG();
            }
        }
        if (other.gameObject.tag == "PlayerAlt")
        {
            player2.TakeDamage(damage);

            Destroy(gameObject);
            if (poison > 0)
            {
                player2.StartPoisonTimerP1DMG();
            }
        }
        if (other.gameObject.tag == "Training Dummy")
        {
            trainingDummy.TakeDamage(damage);

            Destroy(gameObject);
            if (poison > 0)
            {
                trainingDummy.StartPoisonTimerP1DMG();
            }
        }
        if (other.gameObject.tag == "Wall")
        {

            timesBounced += 1;
            rangeTime = range;

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