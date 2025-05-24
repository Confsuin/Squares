using UnityEngine;

public class FireZone : MonoBehaviour
{
    public float FireZoneDamage;
    public float FireZoneTickDelay = 0.15f;
    public float Duration = 5f;

    public bool Player1IsInFireZone = false;
    public bool Player2IsInFireZone = false;
    public bool TrainingDummyIsInFireZone = false;

    public Player1 player1;
    public Player2 player2;
    public TrainingDummy trainingDummy;

    private PointSystem pointSystem;
    private void Awake()
    {
        GetReferences();
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            Player1IsInFireZone = true;
        }
        if (other.tag == ("PlayerAlt"))
        {
            Player2IsInFireZone = true;
        }
        if (other.tag == ("Training Dummy"))
        {
            TrainingDummyIsInFireZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            Player1IsInFireZone = false;
        }
        if (other.tag == ("PlayerAlt"))
        {
            Player2IsInFireZone = false;
        }
        if (other.tag == ("Training Dummy"))
        {
            TrainingDummyIsInFireZone = false;
        }
    }
    private void FixedUpdate()
    {
        FireZoneTickDelay -= Time.deltaTime;
        Duration -= Time.deltaTime;
        if (FireZoneTickDelay <= 0)
        {
            if (Player1IsInFireZone)
            {
                player1.TakeDamage(FireZoneDamage);
            }
            if (Player2IsInFireZone)
            {
                player2.TakeDamage(FireZoneDamage);
            }
            if (TrainingDummyIsInFireZone)
            {
                trainingDummy.TakeDamage(FireZoneDamage);
            }
            FireZoneTickDelay = 0.15f;
        }
        if (Duration <= 0)
        {
            Destroy(gameObject);
            OnKill();
        }
    }
    private void OnKill()
    {
        pointSystem.BulletAndEffectsInWorld.Remove(gameObject);
    }
    private void GetReferences()
    {
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        trainingDummy = GameObject.FindWithTag("Training Dummy").GetComponent<TrainingDummy>();
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
    }
}
