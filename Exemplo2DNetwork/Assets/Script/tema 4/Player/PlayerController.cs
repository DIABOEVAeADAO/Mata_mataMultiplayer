using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [Header("Speed settings")]
    [SerializeField] private InputActionReference _moveAction;
    public float speed = 5f;
    
    [Header("Jump settings")]
    [SerializeField] private InputActionReference _jumpAction;
    public float jumpForce = 7f;
    
    [Header("Shoot Settings")]
    [SerializeField] private InputActionReference _ShootAction;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    private void OnEnable()
    {
        _moveAction.action.performed += OnMove;
        _moveAction.action.canceled += OnMove;

        _jumpAction.action.started += OnJump;
        _ShootAction.action.started += OnFire;
    }

    private void OnDisable()
    {
        _moveAction.action.performed -= OnMove;
        _moveAction.action.canceled -= OnMove;

        _jumpAction.action.started -= OnJump;
        _ShootAction.action.started -= OnFire;
    }

    void Update()
    {
        if (!IsOwner) return;
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}