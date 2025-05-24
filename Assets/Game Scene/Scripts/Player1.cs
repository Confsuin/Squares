using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player1 : MonoBehaviour
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

    //InputSystem
    private InputAction shootAction;
    private InputAction teleportAction;
    private InputAction escapeAction;
    private InputAction debugAction;

    //Health
    public float maxHealth;
    public float currentHealth;
    public float MissingHealth = 0;
    public PlayerHealthBar playerHealthBar;
    public bool Player1Dead = false;

    //Status Effects
    public bool canDoActions = false;
    public bool IsInFireZone = false;

    //Refrences
    public Player2 player2;
    private GameObject hitIndicator;
    // Menus
    private EscapeMenuButtons escapeMenu;
    private DebugMenuButtons debugMenu;

    //public List<GameObject> BulletSpawnPoint = new();
    public GameObject bulpos;


    public void BulletSpawner()
    {
        if (Ammo > 0 && shootTimer >= AttackSpeed && canDoActions == true)
        {            
            shootTimer = 0f;

            Ammo--; //Reduces the ammo amount
            UpdateAmmoCounter();

            Vector2 direction = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - weapon.transform.position).normalized;

            float inaccuracy = Random.Range(-GunInaccuracy, GunInaccuracy); // Random inaccuracy angle in degrees
            float angleWithInaccuracy = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + inaccuracy;

            Vector2 inaccuracyDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angleWithInaccuracy), Mathf.Sin(Mathf.Deg2Rad * angleWithInaccuracy));

            GameObject spawnedBullet = Instantiate(bullet, bulpos.transform.position, Quaternion.Euler(0f, 0f, angleWithInaccuracy));
            Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = inaccuracyDirection * bullet.GetComponent<Player1Bullet>().speed;
            }
            if (ShotgunCount >= 1)
            {
                for (int i = 0; i < ShotgunCount - 1; i++) // -1 because one was already fired
                {
                    // Recalculate fresh direction and inaccuracy for this pellet
                    Vector2 randomDirection = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - weapon.transform.position).normalized;

                    float randomInaccuracy = Random.Range(-GunInaccuracy, GunInaccuracy);
                    float randomAngle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg + randomInaccuracy;

                    Vector2 directionWithInaccuracy = new Vector2(Mathf.Cos(Mathf.Deg2Rad * randomAngle), Mathf.Sin(Mathf.Deg2Rad * randomAngle));

                    GameObject extraBullet = Instantiate(bullet, bulpos.transform.position, Quaternion.Euler(0f, 0f, randomAngle));
                    Rigidbody2D extraRb = extraBullet.GetComponent<Rigidbody2D>();
                    if (extraRb != null)
                    {
                        extraRb.linearVelocity = directionWithInaccuracy * bullet.GetComponent<Player1Bullet>().speed;
                    }
                }
            }
        }
        else if (Ammo == 0)
        {
            Debug.Log("Out Of Ammo!");
        }
    }


    public void Teleport()
    {
        Debug.Log("AltFire Clicked");
        if (teleportDistance > 0 && canDoActions == true)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Vector2 aimDirection = mousePosition - (Vector2)weapon.transform.position;

            Vector2 RotationPointP = weapon.transform.position;

            Ray2D ray = new Ray2D(RotationPointP, aimDirection);
            Vector2 teleportTarget = ray.origin + ray.direction.normalized * teleportDistance;
            transform.position = teleportTarget;
        }
    }

    public void Escape()
    {
        escapeMenu.ToggleEscapeMenu();
    }
    public bool OpenedByPlayer1 = true;
    public void DebugMenu()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SandBox"))
        {
            debugMenu.ToggleDebugMenu();
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
        shootAction = input.Player1.Shoot;
        shootAction.Enable();
        
        teleportAction = input.Player1.Teleport;
        teleportAction.Enable();

        escapeAction = input.Player1.Escape; ;
        escapeAction.Enable();

        debugAction = input.Player1.Debug;
        debugAction.Enable();
    }

    private void OnEnable()
    {
        if (canDoActions == true)
        {
            //Movement
            input.Enable();
            input.Player1.Movement.performed += OnMovementPerformed;
            input.Player1.Movement.canceled += OnMovementCancelled;

            //Aiming
            input.Player1.Aim.performed += OnAimPerformed;
            input.Player1.Aim.canceled += OnAimCanceled;

            //Teleport
            teleportAction.performed += _ => Teleport();

            //Escape
            escapeAction.performed += _ => Escape();

            //Debug
            debugAction.performed += _ => DebugMenu();

            //Shooting
            shootAction.performed += _ => BulletSpawner();
        }
    }

    private void OnDisable()
    {
        if (canDoActions == true)
        {
            //Movement
            input.Disable();
            input.Player1.Movement.performed -= OnMovementPerformed;
            input.Player1.Movement.canceled -= OnMovementCancelled;

            //Teleport
            teleportAction.performed -= _ => Teleport();

            //Escape
            escapeAction.performed -= _ => Escape();

            //Debug
            debugAction.performed -= _ => DebugMenu();

            //Shooting
            shootAction.performed -= _ => BulletSpawner();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveVector * moveSpeed;

        if (Player1Dead == false && currentHealth <= 0)
        {
            Player1Dead = true;
            Debug.Log("Player2 Died");
        }



        if (isReloading)
        {
            ReloadTimer += Time.deltaTime;

            if (ReloadTimer >= ReloadSpeed)
            {
                FinishReload();
            }
        }

        //Starts reload automaticly after 2 seconds of not shooting or when ammo hits 0
        shootTimer += Time.deltaTime;
        if (shootTimer >= 2f && Ammo > 0 && !isReloading)
        {
            StartReload();
        }
        if (Ammo <= 0 && !isReloading)
        {
            StartReload();
        }
    }


    private void OnAimPerformed(InputAction.CallbackContext value)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 aimDirection = mousePosition - (Vector2)weapon.transform.position;

        //Rotation of Gun
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
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

    public IEnumerator PoisonTimerP1DMG()
    {

        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP1DMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP1DMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP1DMG();
    }
    public IEnumerator PoisonTimerP2DMG()
    {

        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP2DMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP2DMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP2DMG();
    }


    public void PoisonDMGP1DMG()
    {

        BulletPoisonDamage = MissingHealth * BulletPoison;
        TakeDamage(BulletPoisonDamage);
    }
    public void PoisonDMGP2DMG()
    {

        BulletPoisonDamage = MissingHealth * player2.BulletPoison;
        TakeDamage(BulletPoisonDamage);
    }
    public void StartPoisonTimerP1DMG()
    {
        StartCoroutine(PoisonTimerP1DMG());
    }
    public void StartPoisonTimerP2DMG()
    {
        StartCoroutine(PoisonTimerP2DMG());
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
        if (player2.BulletLifeSteal > 0)
        {
            player2.currentHealth += DMG * player2.BulletLifeSteal;
            player2.MissingHealth = player2.maxHealth - player2.currentHealth;
            if (player2.currentHealth > player2.maxHealth)
            {
                player2.currentHealth = player2.maxHealth;
            }
        }
        playerHealthBar.UpdateHealthBar();
        player2.playerHealthBar.UpdateHealthBar();
    }
























    private void GetRefrences()
    {
        escapeMenu = gameObject.transform.GetComponentInChildren<EscapeMenuButtons>();
        debugMenu = gameObject.transform.GetComponentInChildren<DebugMenuButtons>();
        hitIndicator = GameObject.FindWithTag("Player 1 Hit Indicator");
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        playerHealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
    }
}