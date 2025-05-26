using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // GameObjects
    private GameObject CardsPickerBackground;
    public GameObject[] DebugMenu;
    public GameObject CardBody;
    private Transform Parent;
    public Sprite BackSide;
    public Sprite FrontSide;

    // References
    private CardSystemSpawner cardSystemSpawner;

    public GameBehaviour gameBehaviour;

    private LevelManager levelManager;

    private PointSystem pointSystem;

    private Player1 player1;
    private Player2 player2;

    private PlayerHealthBar player1HealthBar;
    private PlayerHealthBar player2HealthBar;

    private DebugMenuButtons debugMenuButtons;

    public TMP_Text[] CardText;

    // Bools
    public bool Flipped = false;

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

    public float FireZoneDamage;
    public float ExplosionDMG;
    public float GunInaccuracy;

    public float CardNumber;

    // Reload Speed is in 0.25 second intervals.
    public float ReloadSpeed;
    public float Ammo;

    public float BlockCoolDown;
    public float BlockCount;

    void Awake()
    {
        CardBody.transform.SetAsLastSibling();
        GetReferences();
        gameBehaviour.IsCardsMenuActive = true;
        StartCoroutine(WaitForCardsSpawned());
        foreach (TMP_Text text in CardText)
        {
            text.enabled = false;
        }
    }
    IEnumerator WaitForCardsSpawned()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SandBox"))
        {
            foreach (GameObject gameObject in DebugMenu)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void DoSelectCard()
    {
        DoAddStatsToPlayer();
        Destroy(GameObject.FindWithTag("Cards Menu"));
        if (pointSystem.FirstCardSpawn == true)
        {
            pointSystem.Player1Won = true;
            pointSystem.FirstCardSpawn = false;
            cardSystemSpawner.DoSpawnCards();
        }
        else if (pointSystem.FirstCardSpawn == false)
        {
            gameBehaviour.IsCardsMenuActive = false;
            levelManager.StartSquareOff();
            Debug.Log("Card Clicked");
        }
    }
    void DoAddStatsToPlayer()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SandBox"))
        {
            Time.timeScale = 1;
            foreach (GameObject gameObject in DebugMenu)
            {
                gameObject.SetActive(true);
            }
        }
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

            // Adds AltFire Stats
            player1.teleportDistance = player1.teleportDistance + TeleportDistance;

            // Adds Core Bullet Stats
            player1.Damage = player1.Damage * Damage;
            player1.BulletLifeSteal = player1.BulletLifeSteal + BulletLifeSteal;
            player1.BulletPoison = player1.BulletPoison + BulletPoison;
            player1.BulletGrowth = player1.BulletGrowth + BulletGrowth;
            player1.BulletSpeed = player1.BulletSpeed * BulletSpeed;
            player1.BulletSlow = player1.BulletSlow + BulletSlow;
            player1.BulletBounces = player1.BulletBounces + BulletBounces;

            // Adds Bullet Effect Stats
            player1.FireZoneDamage = player1.FireZoneDamage + FireZoneDamage;
            player1.ExplosionDMG = player1.ExplosionDMG + ExplosionDMG;

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

            // Adds AltFire Stats
            player2.teleportDistance = player2.teleportDistance + TeleportDistance;

            // Adds Core Bullet Stats
            player2.Damage = player2.Damage * Damage;
            player2.BulletLifeSteal = player2.BulletLifeSteal + BulletLifeSteal;
            player2.BulletPoison = player2.BulletPoison + BulletPoison;
            player2.BulletGrowth = player2.BulletGrowth + BulletGrowth;
            player2.BulletSpeed = player2.BulletSpeed * BulletSpeed;
            player2.BulletSlow = player2.BulletSlow + BulletSlow;
            player2.BulletBounces = player2.BulletBounces + BulletBounces;

            // Adds Bullet Effect Stats
            player2.FireZoneDamage = player2.FireZoneDamage + FireZoneDamage;
            player2.ExplosionDMG = player2.ExplosionDMG + ExplosionDMG;

            pointSystem.Player1Won = false;

            cardSystemSpawner.CardsSpawned = false;

            player1HealthBar.UpdateHealthBar();
            player2HealthBar.UpdateHealthBar();

            Debug.Log("Stats added to Player 2");
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void FlipCard()
    {
        if (Flipped == false)
        {
            Flipped = true;
            StartCoroutine(FlipCardCoroutine());
        }
    }
    IEnumerator FlipCardCoroutine()
    {
        transform.localRotation = Quaternion.Euler(0, 165, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 150, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 135, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 120, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 105, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 90, 0);
        CardBody.transform.SetAsFirstSibling();
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 75, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 60, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 45, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 30, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 15, 0);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        foreach (TMP_Text text in CardText)
        {
            text.enabled = true;
        }
    }
    public void IncreaseCardSize()
    {
        FlipCard();
        StartCoroutine(IncreaseCardSizeCoroutine());
    }
    public void DecreaseCardSize()
    {
        StartCoroutine(DecreaseCardSizeCoroutine());
    }
    IEnumerator IncreaseCardSizeCoroutine()
    {
        transform.localScale = new Vector2(1.066f, 1.066f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.132f, 1.132f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.2f, 1.2f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.266f, 1.266f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.32f, 1.32f);
    }
    IEnumerator DecreaseCardSizeCoroutine()
    {
        transform.localScale = new Vector2(1.266f, 1.266f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.2f, 1.2f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.132f, 1.132f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1.066f, 1.066f);
        yield return new WaitForSecondsRealtime(0.02f);
        transform.localScale = new Vector2(1f, 1f);
    }
    private void GetReferences()
    {
        CardText = GetComponentsInChildren<TMP_Text>();

        Parent = this.gameObject.transform.parent;
        CardsPickerBackground = GameObject.FindWithTag("Cards Picker Background");
        cardSystemSpawner = GameObject.FindWithTag("Cards System").GetComponent<CardSystemSpawner>();
        gameBehaviour = GameObject.FindWithTag("Game Behaviour").GetComponent<GameBehaviour>();
        levelManager = GameObject.FindWithTag("Level Manager").GetComponent<LevelManager>();
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        player1HealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
        player2HealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SandBox"))
        {
            var parentGameObject = this.transform.root.gameObject;
            if (parentGameObject.tag == ("Player"))
            {
                debugMenuButtons = GameObject.FindWithTag("Player 1 Debug Menu Parent").GetComponent<DebugMenuButtons>(); ;
                DebugMenu = GameObject.FindGameObjectsWithTag("Player 1 Debug Menu");
            }
            else if (parentGameObject.tag == ("PlayerAlt"))
            {
                debugMenuButtons = GameObject.FindWithTag("Player 2 Debug Menu Parent").GetComponent<DebugMenuButtons>();
                DebugMenu = GameObject.FindGameObjectsWithTag("Player 2 Debug Menu");
            }
        }
    }
}