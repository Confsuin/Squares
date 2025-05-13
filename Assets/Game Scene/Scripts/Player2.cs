using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public float FireRadius;
    public float ExplosionDMG;

    //Reloading
    private bool isReloading = false;
    private float ReloadTimer = 0f;
    private float shootTimer = 0f;

    //Weapon/Bullet
    public GameObject weapon;
    public GameObject bullet;

    //Movement/Aiming
    private Vector2 aimDirection;
    private PlayerInputs input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    //Shoot
    private InputAction shootAction;
    private InputAction teleportAction;
    private InputAction escapeAction;

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


    //public List<GameObject> BulletSpawnPoint = new();
    public GameObject bulpos;


    public void BulletSpawner()
    {
        if (Ammo > 0 && shootTimer >= AttackSpeed && canDoActions == true)
        {

            shootTimer = 0f;

            Ammo--; //Reduces the ammo amount

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

    }

    private void Start()
    {
        currentHealth = maxHealth;
        Ammo = StartingAmmo;
        GetRefrences();
        hitIndicator.SetActive(false);
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

        if (ShotgunCount > 0)
        {
            GunInaccuracy = 5;
        }
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
            input.Player2.Movement.performed -= OnMovementPerformed;
            input.Player2.Movement.canceled -= OnMovementCancelled;

            //Teleport
            teleportAction.performed -= _ => Teleport();

            //Escape
            escapeAction.performed -= _ => Escape();

            //Shooting
            shootAction.performed -= _ => BulletSpawner();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveVector * moveSpeed;

        if (Player2Dead == false && currentHealth <= 0)
        {
            Player2Dead = true;
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
        aimDirection = value.ReadValue<Vector2>();

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
        moveVector = value.ReadValue<Vector2>();
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

        BulletPoisonDamage = MissingHealth * player1.BulletPoison;
        TakeDamage(BulletPoisonDamage);
    }
    public void PoisonDMGP2DMG()
    {

        BulletPoisonDamage = MissingHealth * BulletPoison;
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
        hitIndicator = GameObject.FindWithTag("Player 2 Hit Indicator");
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        playerHealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
    }
}