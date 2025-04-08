using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Threading;

public class Player1 : MonoBehaviour
{
    //Stats
    public float maxHealth;
    public float currentHealth;
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
    public float BulletGrowth;
    public float BulletSpeed = 30;
    public float BulletSize = 1;
    public float BulletSlow = 1;
    public float BulletBounces;
    //BulletEffects
    public float FireRadius;
    public float ExplosionRadius;


    private bool isReloading = false;
    private float ReloadTimer = 0f;
    private float shootTimer = 0f;


    public GameObject weapon;
    public GameObject bullet;


    private Vector2 aimDirection;
    private PlayerInputs input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;


    private InputAction shootAction;

    private PlayerHealthBar playerHealthBar;
    public bool Player1Dead = false;


    //public List<GameObject> BulletSpawnPoint = new();
    public GameObject bulpos;


    public void BulletSpawner()
    {
        if (Ammo > 0 && shootTimer >= AttackSpeed)
        {
            shootTimer = 0f;

            Ammo--; //Reduces the ammo amount

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

            Destroy(spawnedBullet, Range);
            //Shotgun changes the "Range" to ".35f"
        }
        else if (Ammo == 0)
        {
            Debug.Log("Out Of Ammo!");
        }
    }



    private void Start()
    {
        currentHealth = maxHealth;
        Ammo = StartingAmmo;
        playerHealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
    }


    private void Awake()
    {

        input = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();

        //var inputActions = new InputAction();
        shootAction = input.Player1.Shoot;
        shootAction.Enable();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player1.Movement.performed += OnMovementPerformed;
        input.Player1.Movement.canceled += OnMovementCancelled;

        //Aiming
        input.Player1.Aim.performed += OnAimPerformed;
        input.Player1.Aim.canceled += OnAimCanceled;

        //Shooting
        shootAction.performed += _ => BulletSpawner();
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player1.Movement.performed -= OnMovementPerformed;
        input.Player1.Movement.canceled -= OnMovementCancelled;

        //Shooting
        shootAction.performed -= _ => BulletSpawner();
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
            Debug.Log("Reloading.....");
        }
    }

    private void FinishReload()
    {
        Ammo = StartingAmmo;
        isReloading = false;
        ReloadTimer = 0f;
        Debug.Log("Reload complete!");
    }





    public void TakeDamage(float DMG)
    {
        currentHealth -= DMG;
        Debug.Log("Player took" + DMG + "damage. health: " + currentHealth);
        playerHealthBar.UpdateHealthBar();
    }
}