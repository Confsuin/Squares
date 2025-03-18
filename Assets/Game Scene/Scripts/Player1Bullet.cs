using UnityEngine;

public class Player1Bullet : MonoBehaviour
{
    public float Damage = 25;

    public float BulletLifeSteal;
    public float BulletPoison;
    public float BulletSpeed = 30;
    public float BulletSize = 1;
    public float BulletSlow = 1;
    public float BulletBounces;

    public Player1 player1;
    public Player2 player2;

    Rigidbody2D rb2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.linearVelocity = transform.right * BulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player1.TakeDamage(Damage);
            Debug.Log("hit Player");
        }

        if (other.gameObject.tag == "PlayerAlt")
        {
            player2.TakeDamage(Damage);
            Debug.Log("hit PlayerAlt");
        }
    }
}
