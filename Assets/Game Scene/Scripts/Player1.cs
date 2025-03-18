using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Player1 : MonoBehaviour
{
    // Intergers
    public int maxHealth = 10;
    public int currentHealth;
    public int missingHealth = 0;

    public float moveSpeed = 10f;



    public GameObject weapon;
    public GameObject bullet;

    private Vector2 aimDirection;
    private PlayerInputs input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    private InputAction shootAction;




    //public List<GameObject> BulletSpawnPoint = new();
    public GameObject bulpos;


    public void BulletSpawner()
    {
        GameObject spawnedBullet = Instantiate(bullet, bulpos.transform.position, bulpos.transform.rotation);
        Destroy(spawnedBullet, 1f);
    }



    private void Start()
    {
        currentHealth = maxHealth;
        UpdateMissingHealth();
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





    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateMissingHealth();
        Debug.Log("Player took" + amount + "damage. health: " + currentHealth);
    }

    public void UpdateMissingHealth()
    {
        missingHealth = currentHealth - maxHealth;
    }
}