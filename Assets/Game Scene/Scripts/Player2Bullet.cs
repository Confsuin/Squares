using UnityEngine;

public class Player2Bullet : MonoBehaviour
{
    public float damage;
    public float lifesteal;
    public float poison;
    public float speed;
    public float size;
    public float slow;
    public float bounce;
    public float fire;
    public float explosion;

    public Player1 player1;
    public Player2 player2;

    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();

        //Get Stats From Player2
        damage = player2.Damage;
        lifesteal = player2.BulletLifeSteal;
        poison = player2.BulletPoison;
        speed = player2.BulletSpeed;
        size = player2.BulletSize;
        slow = player2.BulletSlow;
        bounce = player2.BulletBounces;
        fire = player2.FireRadius;
        explosion = player2.ExplosionRadius;
    }

    void Update()
    {
        rb2d.linearVelocity = transform.right * speed;
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
                player2.Hitself = true;
                player2.IsPoisoned = true;
                player2.StartPoisonTimer();
            }
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
