using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float ExplosionDMG;

    public Player1 player1;
    public Player2 player2;
    public TrainingDummy trainingDummy;

    private void Awake()
    {
        GetReferences();
        Destroy(gameObject, 0.5f);
    }

    public void ExplosionDamagePlayer(int Player)
    {
        if (Player == 1)
        {
            ExplosionDMG = player1.Damage * player1.ExplosionDMG;
        }
        if (Player == 2)
        {
            ExplosionDMG = player2.Damage * player2.ExplosionDMG;
        }
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
        if (other.tag == ("Training Dummy"))
        {
            trainingDummy.TakeDamage(ExplosionDMG);
        }
    }
    private void GetReferences()
    {
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        trainingDummy = GameObject.FindWithTag("Training Dummy").GetComponent<TrainingDummy>();
    }
}