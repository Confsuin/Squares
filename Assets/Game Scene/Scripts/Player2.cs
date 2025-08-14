using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    //Stats
    public float moveSpeed;
    public float teleportDistance;
    //Shooting
    public float BulletCount;
    public float ShotgunCount;
    public float Ammo;
    public float StartingAmmo = 4;
    public float GunInaccuracy;
    public float AttackSpeed = 1;
    public float Range = 1;
    public float ReloadSpeed;
    //Bullet
    public float Damage = 25;
    public float BulletLifeSteal;
    public float BulletPoison;
    public float BulletPoisonDamage;
    public float BulletGrowth;
    public float BulletSpeed = 30;
    public float BulletSize = 1;
    public float BulletSlow = 1;
    public float BulletBounces;
    //BulletSpawn
    public float FireZoneDamage;
    public float ExplosionDMG;

    //Reloading
    private bool isReloading = false;
    private float ReloadTimer = 0f;
    private float shootTimer = 0f;

    //Weapon/Bullet
    public GameObject weapon;
    public GameObject bullet;
    public GameObject ammoCounter;
    public GameObject ammoCounterBullet;

    //Movement/Aiming
    private Vector2 aimDirection;
    private PlayerInputs input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    //Shoot
    private InputAction shootAction;
    private InputAction teleportAction;
    private InputAction escapeAction;
    private InputAction debugAction;
    private InputAction closeCardsMenuAction;

    //Health
    public float maxHealth;
    public float currentHealth;
    public float MissingHealth = 0;
    public PlayerHealthBar playerHealthBar;
    public bool Player2Dead = false;

    //Status Effects
    public bool canDoActions = false;

    //Refrences
    public Player1 player1;
    private GameObject hitIndicator;
    // Menus
    private EscapeMenuButtons escapeMenu;
    private DebugMenuButtons debugMenu;

    //public List<GameObject> BulletSpawnPoint = new();
    public GameObject bulpos;



    public bool isShooting = false;
    IEnumerator BulletSpawner()
    {
        if (Ammo > 0 && shootTimer >= AttackSpeed && canDoActions == true)
        {
            for (int b = 0; b < BulletCount; b++)
            {
                shootTimer = 0f;

                Ammo--; //Reduces the ammo amount
                UpdateAmmoCounter();

                Vector2 direction = weapon.transform.right.normalized;

                float inaccuracy = Random.Range(-GunInaccuracy, GunInaccuracy); // Random inaccuracy angle in degrees
                float angleWithInaccuracy = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + inaccuracy;

                Vector2 inaccuracyDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angleWithInaccuracy), Mathf.Sin(Mathf.Deg2Rad * angleWithInaccuracy));

                GameObject spawnedBullet = Instantiate(bullet, bulpos.transform.position, Quaternion.Euler(0f, 0f, angleWithInaccuracy));
                Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
                if (bulletRb != null)
                {
                    bulletRb.linearVelocity = inaccuracyDirection * bullet.GetComponent<Player2Bullet>().speed;
                }
                if (ShotgunCount >= 1)
                {
                    for (int i = 0; i < ShotgunCount - 1; i++) // -1 because one was already fired
                    {
                        // Recalculate fresh direction and inaccuracy for this pellet
                        Vector2 randomDirection = weapon.transform.right.normalized;

                        float randomInaccuracy = Random.Range(-GunInaccuracy, GunInaccuracy);
                        float randomAngle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg + randomInaccuracy;

                        Vector2 directionWithInaccuracy = new Vector2(Mathf.Cos(Mathf.Deg2Rad * randomAngle), Mathf.Sin(Mathf.Deg2Rad * randomAngle));

                        GameObject extraBullet = Instantiate(bullet, bulpos.transform.position, Quaternion.Euler(0f, 0f, randomAngle));
                        Rigidbody2D extraRb = extraBullet.GetComponent<Rigidbody2D>();
                        if (extraRb != null)
                        {
                            extraRb.linearVelocity = directionWithInaccuracy * bullet.GetComponent<Player2Bullet>().speed;
                        }
                    }
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        else if (Ammo == 0)
        {
            Debug.Log("Out Of Ammo!");
        }
    }

    public float AltFireCoolDown = 3f;
    public float AltFireCurrentCooldown;
    public void Teleport()
    {
        Debug.Log("AltFire Clicked");
        if (teleportDistance > 0 && canDoActions == true && AltFireCurrentCooldown >= AltFireCoolDown)
        {


            Vector2 teleportDirection = weapon.transform.right.normalized;

            Vector2 RotationPointP = weapon.transform.position;

            Ray2D ray = new Ray2D(RotationPointP, teleportDirection);
            Vector2 teleportTarget = ray.origin + ray.direction.normalized * teleportDistance;
            transform.position = teleportTarget;
            AltFireCurrentCooldown = 0;
        }
    }

    public bool OpenedByPlayer2 = false;
    public void DebugMenu()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SandBox"))
        {
            if (escapeMenu.EscapeMenuActive == false && debugMenu.IsMenuActive == false)
            {
                OpenedByPlayer2 = true;
            }
            debugMenu.ToggleDebugMenu();
        }
    }
    public void Escape()
    {
        if (escapeMenu.EscapeMenuActive == false && debugMenu.IsMenuActive == false)
        {
            OpenedByPlayer2 = true;
        }
        escapeMenu.ToggleEscapeMenu();
    }

    private GameBehaviour gameBehaviour;
    private PointSystem pointSystem;
    private CardSystemSpawner cardSystemSpawner;
    public void CloseCardsMenu()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SandBox"))
        {
            if (GameObject.FindWithTag("Cards Menu") != null)
            {
                var Cards = GameObject.FindWithTag("Card").GetComponent<Card>();
                Cards.ShowDebugMenu();
                Destroy(GameObject.FindWithTag("Cards Menu"));
                gameBehaviour.IsCardsMenuActive = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                pointSystem.Player1Won = false;
                pointSystem.Player2Won = false;

                cardSystemSpawner.CardsSpawned = false;
                Debug.Log("Close Cards Menu Pressed");
            }
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        Ammo = StartingAmmo;
        GetRefrences();
        hitIndicator.SetActive(false);
        SetAmmoCounter();
    }


    private void Awake()
    {
        input = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();

        //var inputActions = new InputAction();
        shootAction = input.Player2.Shoot;
        shootAction.Enable();

        teleportAction = input.Player2.Teleport;
        teleportAction.Enable();

        escapeAction = input.Player2.Escape;
        escapeAction.Enable();

        debugAction = input.Player2.Debug;
        debugAction.Enable();

        closeCardsMenuAction = input.Player2.CloseCardsMenu;
        closeCardsMenuAction.Enable();
    }

    private void OnEnable()
    {
        if (canDoActions == true)
        {
            //Movement
            input.Enable();
            input.Player2.Movement.performed += OnMovementPerformed;
            input.Player2.Movement.canceled += OnMovementCancelled;

            //Aiming
            input.Player2.Aim.performed += OnAimPerformed;
            input.Player2.Aim.canceled += OnAimCanceled;

            //Teleport
            teleportAction.performed += _ => Teleport();

            //Escape
            escapeAction.performed += _ => Escape();

            //Debug
            debugAction.performed += _ => DebugMenu();

            // Close Cards Menu
            closeCardsMenuAction.performed += _ => CloseCardsMenu();

            //Shooting
            shootAction.started += _ => isShooting = true;
            shootAction.canceled += _ => isShooting = false;
        }
    }

    private void OnDisable()
    {
        if (canDoActions == true)
        {
            //Movement
            input.Disable();
            input.Player2.Movement.performed -= OnMovementPerformed;
            input.Player2.Movement.canceled -= OnMovementCancelled;

            //Teleport
            teleportAction.performed -= _ => Teleport();

            //Escape
            escapeAction.performed -= _ => Escape();

            //Debug
            debugAction.performed -= _ => DebugMenu();

            // Close Cards Menu
            closeCardsMenuAction.performed -= _ => CloseCardsMenu();

            //Shooting
            shootAction.started -= _ => isShooting = true;
            shootAction.canceled -= _ => isShooting = false;
        }
    }

    private void FixedUpdate()
    {
        UpdateAltFireIndicator();

        poisonCurrentDuration -= Time.deltaTime;
        poisonTickTimer -= Time.deltaTime;

        if (poisonTickTimer <= 0 && poisonCurrentDuration > 0)
        {
            PoisonTick();
            poisonTickTimer = 1f;
        }
        if (poisonCurrentDuration <= 0)
        {
            poisonPercentDMG = 0;
        }

        AltFireCurrentCooldown += Time.deltaTime;
        rb.linearVelocity = moveVector * moveSpeed;

        if (Player2Dead == false && currentHealth <= 0)
        {
            Player2Dead = true;
            Debug.Log("Player2 Died");
        }

        if (isShooting == true)
        {
            StartCoroutine(BulletSpawner());
        }

        if (isReloading)
        {
            ReloadTimer += Time.deltaTime;

            if (ReloadTimer >= ReloadSpeed)
            {
                FinishReload();
            }
        }

        //Starts reload automaticly after Reload speed seconds of not shooting or when ammo hits 0
        shootTimer += Time.deltaTime;
        if (shootTimer >= ReloadSpeed && Ammo > 0 && !isReloading)
        {
            FinishReload();
        }
        if (Ammo <= 0 && !isReloading)
        {
            StartReload();
        }
    }


    private void OnAimPerformed(InputAction.CallbackContext value)
    {
        if (weapon != null)
        {


            aimDirection = value.ReadValue<Vector2>();

            //Rotation of Gun
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    private void OnAimCanceled(InputAction.CallbackContext value)
    {
        aimDirection = value.ReadValue<Vector2>();
    }





    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        if (canDoActions == true)
        {
            moveVector = value.ReadValue<Vector2>();
        }
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }



    private void StartReload()
    {
        if (Ammo < StartingAmmo && !isReloading)
        {
            isReloading = true;
            ReloadTimer = 0f;
        }
    }

    private void FinishReload()
    {
        Ammo = StartingAmmo;
        isReloading = false;
        ReloadTimer = 0f;
        SetAmmoCounter();
    }

    // Ammo Counter
    private float AmmoPosition = -0.21f;
    private float AmmoHeight = 0;
    public int AmmoCount = 0;
    public List<GameObject> AmmoCounterObjects;
    private void SetAmmoCounter()
    {
        if (AmmoCounterObjects.Count > 0)
        {
            foreach (GameObject AmmoCounterBullet in AmmoCounterObjects.ToList())
            {
                Destroy(AmmoCounterBullet);
                AmmoCounterObjects.Remove(AmmoCounterBullet);
            }
        }
        for (int i = 0; i < Ammo; i++)
        {
            GameObject g = Instantiate(ammoCounterBullet, ammoCounter.transform);
            AmmoCount = i;
            AmmoCounterObjects.Add(g);
            if (i % 4 == 0)
            {
                AmmoPosition = -0.21f;
                AmmoHeight = 0.035f * i;
            }
            else
            {
                AmmoPosition += 0.14f;
            }
            g.transform.localPosition = new Vector2(AmmoPosition, AmmoHeight);
        }
    }
    private void UpdateAmmoCounter()
    {
        if (AmmoCount >= 0)
        {
            GameObject.Destroy(AmmoCounterObjects[AmmoCount]);
            AmmoCount -= 1;
        }
    }

    [SerializeField] private Slider AltFireIndicator;
    [SerializeField] private Image AltFireIndicatorImage;
    private void UpdateAltFireIndicator()
    {
        AltFireIndicator.value = AltFireCurrentCooldown;
        AltFireIndicator.maxValue = AltFireCoolDown;
        if (AltFireIndicator.value == AltFireIndicator.maxValue)
        {
            AltFireIndicatorImage.color = new Color(0f, 0.827f, 1f);
        }
        else if (AltFireIndicator.value != AltFireIndicator.maxValue)
        {
            AltFireIndicatorImage.color = new Color(0.416f, 0.557f, 0.765f);
        }
    }

    [SerializeField] float poisonTickTimer = 1f;
    public float poisonMaxDuration, poisonCurrentDuration;
    public float poisonPercentDMG;

    public void RefreshPoisonTimer()
    {
        poisonCurrentDuration = poisonMaxDuration;
    }
    public void PoisonTick()
    {
        PoisonDMG();
    }

    public void PoisonDMG()
    {
        BulletPoisonDamage = MissingHealth * poisonPercentDMG;
        TakeDamage(BulletPoisonDamage);
    }

    IEnumerator HitIndicator()
    {
        hitIndicator.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        hitIndicator.SetActive(false);
    }
    public void TakeDamage(float DMG)
    {
        StartCoroutine(HitIndicator());
        currentHealth -= DMG;
        MissingHealth = maxHealth - currentHealth;
        Debug.Log("Player took" + DMG + "damage. health: " + currentHealth);
        if (player1.BulletLifeSteal > 0)
        {
            player1.currentHealth += DMG * player1.BulletLifeSteal;
            player1.MissingHealth = player1.maxHealth - player1.currentHealth;
            if (player1.currentHealth > player1.maxHealth)
            {
                player1.currentHealth = player1.maxHealth;
            }
        }
        playerHealthBar.UpdateHealthBar();
        player1.playerHealthBar.UpdateHealthBar();
    }
























    private void GetRefrences()
    {
        cardSystemSpawner = GameObject.FindWithTag("Cards System").GetComponent<CardSystemSpawner>();
        gameBehaviour = GameObject.FindWithTag("Game Behaviour").GetComponent<GameBehaviour>();
        pointSystem = GameObject.FindWithTag("Point System").GetComponent<PointSystem>();

        escapeMenu = gameObject.transform.GetComponentInChildren<EscapeMenuButtons>();
        debugMenu = gameObject.transform.GetComponentInChildren<DebugMenuButtons>();
        hitIndicator = GameObject.FindWithTag("Player 2 Hit Indicator");
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        playerHealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
    }
}