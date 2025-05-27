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
    public int timesBounced;
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
    private TrainingDummy trainingDummy;

    private PointSystem pointSystem;

    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetRefrences();

        pointSystem.BulletAndEffectsInWorld.Add(gameObject);

        //Get Stats From Player2
        damage = player2.Damage;
        lifesteal = player2.BulletLifeSteal;
        poison = player2.BulletPoison;
        speed = player2.BulletSpeed;
        size = player2.BulletSize;
        slow = player2.BulletSlow;
        bounce = player2.BulletBounces;
        fire = player2.FireZoneDamage;
        explosion = player2.ExplosionDMG;
        range = player2.Range;

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
            OnKill();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player1.TakeDamage(damage);

            Destroy(gameObject);
            OnKill();
            if (poison > 0)
            {
                player1.StartPoisonTimerP2DMG();
            }
        }
        if (other.gameObject.tag == "PlayerAlt")
        {
            player2.TakeDamage(damage);

            Destroy(gameObject);
            OnKill();
            if (poison > 0)
            {
                player2.StartPoisonTimerP2DMG();
            }
        }
        if (other.gameObject.tag == "Training Dummy")
        {
            trainingDummy.TakeDamage(damage);

            Destroy(gameObject);
            OnKill();
            if (poison > 0)
            {
                trainingDummy.StartPoisonTimerP2DMG();
            }
        }
        if (other.gameObject.tag == "Wall")
        {
            rangeTime = range;

            if (TimesBouncedTimer <= 0)
            {
                isBounced = true;
                TimesBouncedTimer = BouncedTimerSpeed;
                timesBounced += 1;
            }
        }
        if (other.gameObject.tag == "Wall" && timesBounced >= bounce)
        {
            Destroy(gameObject);
            OnKill();
        }



        if (explosion > 0 && other.gameObject.tag == "Wall")
        {
            SpawnExplosion();
        }
    }
    public void OnKill()
    {
        if (explosion > 0)
        {
            SpawnExplosion();
        }
        if (fire > 0)
        {
            SpawnFireZone();
        }
        pointSystem.BulletAndEffectsInWorld.Remove(gameObject);
    }
    private void SpawnExplosion()
    {
        GameObject g = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        g.GetComponent<Explosion>().ExplosionDamagePlayer(2);
        pointSystem.BulletAndEffectsInWorld.Add(g);
    }
    private void SpawnFireZone()
    {
        GameObject g = Instantiate(fireZoneEffect, transform.position, Quaternion.identity);
        g.GetComponent<FireZone>().FireZoneDamagePlayer(2);
        pointSystem.BulletAndEffectsInWorld.Add(g);
    }




    private void GetRefrences()
    {
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        trainingDummy = GameObject.FindWithTag("Training Dummy").GetComponent<TrainingDummy>();
    }
}