using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Player2 : MonoBehaviour
{
    //Stats
    public float moveSpeed;
    //Shooting
    public float BulletCount;
    public float ShotgunCount;
    public float GunInaccuracy;
    public float AttackSpeed = 1;
    public float Range = 1;
    public float ReloadSpeed;
    public float Ammo;
    public float StartingAmmo = 4;
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
    //Health
    public float maxHealth;
    public float currentHealth;
    public float MissingHealth = 0;
    private PlayerHealthBar playerHealthBar;
    public bool Player2Dead = false;

    //Status Effects
    public bool IsPoisoned = false;

    //Refrences
    public Player1 player1;


    //public List<GameObject> BulletSpawnPoint = new();
    public GameObject bulpos;


    public void BulletSpawner()
    {
        if (Ammo > 0 && shootTimer >= AttackSpeed)
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
                bulletRb.linearVelocity = inaccuracyDirection * bullet.GetComponent<Player1Bullet>().speed;
            }

            Destroy(spawnedBullet, Range);
            //Shotgun changes the "Range" to ".35f"
        }
    }



    private void Start()
    {
        currentHealth = maxHealth;
        Ammo = StartingAmmo;
        GetRefrences();
    }


    private void Awake()
    {
        input = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        //var inputActions = new InputAction();
        shootAction = input.Player2.Shoot;
        shootAction.Enable();
    }

    private void OnEnable()
    {
        //Movement
        input.Enable();
        input.Player2.Movement.performed += OnMovementPerformed;
        input.Player2.Movement.canceled += OnMovementCancelled;


        //Aiming
        input.Player2.Aim.performed += OnAimPerformed;
        input.Player2.Aim.canceled += OnAimCanceled;

        //Shooting
        shootAction.performed += _ => BulletSpawner();
    }

    private void OnDisable()
    {
        //Movement
        input.Disable();
        input.Player2.Movement.performed -= OnMovementPerformed;
        input.Player2.Movement.canceled -= OnMovementCancelled;

        //Shooting
        shootAction.performed -= _ => BulletSpawner();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveVector * moveSpeed;

        if (Player2Dead == false && currentHealth <= 0)
        {
            Player2Dead = true;
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

    public IEnumerator PoisonTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMG();
        IsPoisoned = false;
    }
    

    public void PoisonDMG()
    {
        if (IsPoisoned == true)
        {
            BulletPoisonDamage = MissingHealth * player1.BulletPoison;
            TakeDamage(BulletPoisonDamage);
        }
    }
    public void StartPoisonTimer()
    {
        StartCoroutine(PoisonTimer());
    }

    public void TakeDamage(float DMG)
    {
        currentHealth -= DMG;
        MissingHealth = maxHealth - currentHealth;
        Debug.Log("Player took" + DMG + "damage. health: " + currentHealth);
        playerHealthBar.UpdateHealthBar();
    }































    private void GetRefrences()
    {
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        playerHealthBar = GameObject.FindWithTag("Player 2 Health Bar").GetComponent<PlayerHealthBar>();
    }
}