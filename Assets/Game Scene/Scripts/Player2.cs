using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 10f;
    public GameObject weapon;

    private Vector2 aimDirection;
    private PlayerInputs input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    private void Awake()
    {
        currentHealth = maxHealth;
        input = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player2.Movement.performed += OnMovementPerformed;
        input.Player2.Movement.canceled += OnMovementCancelled;

        input.Player2.Aim.performed += OnAimPerformed;
        input.Player2.Aim.canceled += OnAimCanceled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player2.Movement.performed -= OnMovementPerformed;
        input.Player2.Movement.canceled -= OnMovementCancelled;

        //input.Player2.ShootingShield.performed
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveVector * moveSpeed;
    }


    private void OnAimPerformed(InputAction.CallbackContext value)
    {
       aimDirection = value.ReadValue<Vector2>();
        Debug.Log("Aim Direction: " + aimDirection);

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
        Debug.Log("Player took" + amount + "damage. health: " + currentHealth);
    }
}