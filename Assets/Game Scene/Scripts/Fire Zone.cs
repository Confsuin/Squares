using UnityEngine;

public class FireZone : MonoBehaviour
{
    public float FireZoneDamage;
    public float FireZoneTickDelay = 0.15f;

    public Player1 player1;
    public Player2 player2;
    public TrainingDummy trainingDummy;

    private void Awake()
    {
        GetReferences();
        Destroy(gameObject, 5);
    }

    public void FireZoneDamagePlayer(int Player)
    {
        if (Player == 1)
        {
            FireZoneDamage = player1.Damage * player1.FireZoneDamage;
        }
        if (Player == 2)
        {
            FireZoneDamage = player2.Damage * player2.FireZoneDamage;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (FireZoneTickDelay <= 0)
        {
            if (other.tag == ("Player"))
            {
                player1.TakeDamage(FireZoneDamage);
            }
            if (other.tag == ("PlayerAlt"))
            {
                player2.TakeDamage(FireZoneDamage);
            }
            if (other.tag == ("Training Dummy"))
            {
                trainingDummy.TakeDamage(FireZoneDamage);
            }
            FireZoneTickDelay = 0.15f;
        }
    }
    private void FixedUpdate()
    {
        FireZoneTickDelay -= Time.deltaTime;
    }
    private void GetReferences()
    {
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        trainingDummy = GameObject.FindWithTag("Training Dummy").GetComponent<TrainingDummy>();
    }
}
