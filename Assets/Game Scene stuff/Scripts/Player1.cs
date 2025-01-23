using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 10f;


    private BasicShit input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    private void Awake()
    {
        currentHealth = maxHealth;
        input = new BasicShit();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player1.Movement.performed += OnMovementPerformed;
        input.Player1.Movement.canceled += OnMovementCancelled;

        //input.Player1.ShootingShield.started += ShootPressed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player1.Movement.performed -= OnMovementPerformed;
        input.Player1.Movement.canceled -= OnMovementCancelled;

        //input.Player1.ShootingShield.started -= ShootPressed;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveVector * moveSpeed;
    }

    /*
    private void ShootPressed(InputAction.CallbackContext obj)
    {
        // 1. Instantiate a projectile prefab
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // 2. Add force to the projectile (optional, but usually needed)
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
    }
    */

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
        Debug.Log("Player took" + amount + "damage. health: " + currentHealth);
    }
}