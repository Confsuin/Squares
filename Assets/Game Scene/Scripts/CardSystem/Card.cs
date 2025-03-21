using UnityEngine;

public class Card : MonoBehaviour
{
    // GameObjects
    public GameObject CardsPickerBackground;

    // References
    private PointSystem pointSystem;
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
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
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
        if (pointSystem.Player2Won == true)
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
            player1.StartingAmmo = player1.Ammo;

            pointSystem.Player2Won = false;

            Debug.Log("Stats added to Player 1");
        }
        if (pointSystem.Player1Won == true)
        {
            player2.maxHealth = Health * player2.maxHealth;
            player2.currentHealth = player2.maxHealth;
            player2.moveSpeed = MovementSpeed * player2.moveSpeed;

            player2.BulletCount = player2.BulletCount + BulletCount;
            player2.ShotgunCount = player2.ShotgunCount + ShotgunCount;
            player2.GunInaccuracy = player2.GunInaccuracy + GunInaccuracy;
            player2.AttackSpeed = player2.AttackSpeed * AttackSpeed;
            player2.Range = player2.Range * Range;
            player2.ReloadSpeed = player2.ReloadSpeed + ReloadSpeed;
            player2.StartingAmmo = player2.StartingAmmo + Ammo;
            player2.Ammo = player2.StartingAmmo;

            pointSystem.Player1Won = false;

            Debug.Log("Stats added to Player 2");
        }
    }
}
