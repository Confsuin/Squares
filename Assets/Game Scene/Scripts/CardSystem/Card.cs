using UnityEngine;

public class Card : MonoBehaviour
{
    // GameObjects
    private GameObject CardsPickerBackground;

    // References
    private CardSystemSpawner cardSystemSpawner;
    private LevelManager levelManager;
    private PointSystem pointSystem;
    private Player1 player1;
    private Player2 player2;
    private PlayerHealthBar player1HealthBar;
    private PlayerHealthBar player2HealthBar;

    // Floats. Numbers in Decimal form.
    public float MovementSpeed = 1;
    public float Health = 1;
    public float Damage = 1;

    public float BulletLifeSteal;
    public float BulletPoison;
    public float BulletSpeed = 1;
    public float BulletGrowth;
    public float BulletSlow;

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
        GetReferences();
    }
    public void DoSelectCard()
    {
        DoAddStatsToPlayer();
        levelManager.StartSquareOff();
        Destroy(GameObject.FindWithTag("Cards Menu"));
        Debug.Log("Card Clicked");
    }
    void DoAddStatsToPlayer()
    {
        if (pointSystem.Player2Won == true)
        {
            // Adds Base Player Stats
            player1.maxHealth = Health * player1.maxHealth;
            player1.currentHealth = player1.maxHealth;
            player1.moveSpeed = MovementSpeed * player1.moveSpeed;

            // Adds On Shot Bullet Stats
            player1.BulletCount = player1.BulletCount + BulletCount;
            player1.ShotgunCount = player1.ShotgunCount + ShotgunCount;
            player1.GunInaccuracy = player1.GunInaccuracy + GunInaccuracy;
            player1.AttackSpeed = player1.AttackSpeed * AttackSpeed;
            player1.Range = player1.Range * Range;
            player1.ReloadSpeed = player1.ReloadSpeed + ReloadSpeed;
            player1.StartingAmmo = player1.StartingAmmo + Ammo;
            if (player1.StartingAmmo < 1)
            {
                player1.StartingAmmo = 1;
            }
            player1.Ammo = player1.StartingAmmo;

            // Adds Core Bullet Stats
            player1.Damage = player1.Damage * Damage;
            player1.BulletLifeSteal = player1.BulletLifeSteal + BulletLifeSteal;
            player1.BulletPoison = player1.BulletPoison + BulletPoison;
            player1.BulletGrowth = player1.BulletGrowth + BulletGrowth;
            player1.BulletSpeed = player1.BulletSpeed * BulletSpeed;
            player1.BulletSlow = player1.BulletSlow + BulletSlow;
            player1.BulletBounces = player1.BulletBounces + BulletBounces;

            // Adds Bullet Effect Stats
            player1.FireRadius = player1.FireRadius + FireRadius;
            player1.ExplosionDMG = player1.ExplosionDMG + ExplosionRadius;

            pointSystem.Player2Won = false;

            cardSystemSpawner.CardsSpawned = false;

            player1HealthBar.UpdateHealthBar();
            player2HealthBar.UpdateHealthBar();

            Debug.Log("Stats added to Player 1");
        }
        if (pointSystem.Player1Won == true)
        {
            // Adds Base Player Stats
            player2.maxHealth = Health * player2.maxHealth;
            player2.currentHealth = player2.maxHealth;
            player2.moveSpeed = MovementSpeed * player2.moveSpeed;

            // Adds On Shot Bullet Stats
            player2.BulletCount = player2.BulletCount + BulletCount;
            player2.ShotgunCount = player2.ShotgunCount + ShotgunCount;
            player2.GunInaccuracy = player2.GunInaccuracy + GunInaccuracy;
            player2.AttackSpeed = player2.AttackSpeed * AttackSpeed;
            player2.Range = player2.Range * Range;
            player2.ReloadSpeed = player2.ReloadSpeed + ReloadSpeed;
            player2.StartingAmmo = player2.StartingAmmo + Ammo;
            if (player2.StartingAmmo < 1)
            {
                player2.StartingAmmo = 1;
            }
            player2.Ammo = player2.StartingAmmo;

            // Adds Core Bullet Stats
            player2.Damage = player2.Damage * Damage;
            player2.BulletLifeSteal = player2.BulletLifeSteal + BulletLifeSteal;
            player2.BulletPoison = player2.BulletPoison + BulletPoison;
            player2.BulletGrowth = player2.BulletGrowth + BulletGrowth;
            player2.BulletSpeed = player2.BulletSpeed * BulletSpeed;
            player2.BulletSlow = player2.BulletSlow + BulletSlow;
            player2.BulletBounces = player2.BulletBounces + BulletBounces;

            // Adds Bullet Effect Stats
            player2.FireRadius = player2.FireRadius + FireRadius;
            player2.ExplosionDMG = player2.ExplosionDMG + ExplosionRadius;

            pointSystem.Player1Won = false;

            cardSystemSpawner.CardsSpawned = false;

            player1HealthBar.UpdateHealthBar();
            player2HealthBar.UpdateHealthBar();

            Debug.Log("Stats added to Player 2");
        }
    }
    private void GetReferences()
    {
        cardSystemSpawner = GameObject.FindWithTag("Cards System").GetComponent<CardSystemSpawner>();
        levelManager = GameObject.FindWithTag("Level Manager").GetComponent<LevelManager>();
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1HealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
        player2HealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
    }
}