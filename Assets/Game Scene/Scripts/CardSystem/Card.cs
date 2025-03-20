using UnityEngine;

public class Card : MonoBehaviour
{
    // GameObjects
    public GameObject CardsPickerBackground;
    public Player1 player1;
    public Player2 player2;

    // Floats. Numbers in Decimal form.
    public float MovementSpeed = 1;
    public float Health = 1;
    public float Damage = 1;

    public float BulletLifeSteal;
    public float BulletPoison;
    public float BulletSpeed = 1;
    public float BulletSize = 1;
    public float BulletSlow = 1;

    public float AttackSpeed = 1;

    public float Range = 1;

    // Floats. Numbers in flat amount form.
    public float TeleportDistance;

    public float BulletBounces;
    public float BulletCount;
    public float ShotgunCount;

    public float FireRadius;
    public float ExplosionRadius;
    public float GunInaccuracy;

    // Reload Speed is in 0.25 second intervals.
    public float ReloadSpeed;
    public float Ammo;
    
    public float BlockCoolDown;
    public float BlockCount;

    void Awake()
    {
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
    }
    public void DoSelectCard()
    {
        DoAddStatsToPlayer();
        Destroy(GameObject.FindWithTag("Cards Menu"));
        Debug.Log("Card Clicked");
    }
    void DoAddStatsToPlayer()
    {
        player1.maxHealth = Health * player1.maxHealth;
        player1.currentHealth = player1.maxHealth;
        player1.moveSpeed = MovementSpeed * player1.moveSpeed;

        player1.BulletCount = player1.BulletCount + BulletCount;
        player1.ShotgunCount = player1.ShotgunCount + ShotgunCount;
        player1.GunInaccuracy = player1.GunInaccuracy + GunInaccuracy;
        player1.AttackSpeed = player1.AttackSpeed * AttackSpeed;
        player1.Range = player1.Range * Range;
        player1.ReloadSpeed = player1.ReloadSpeed + ReloadSpeed;
        player1.StartingAmmo = player1.StartingAmmo + Ammo;
        Debug.Log("Stats added");
    }
}
