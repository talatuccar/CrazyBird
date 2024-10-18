using UnityEngine;
using UnityEngine.InputSystem;
public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [Header("Bird Sprites")]
    public Sprite flyingSprite;
    public Sprite restingSprite;

    private float rotationSpeed = 5f;
    public float jumpForce = 5f;

    private PlayerInputActions inputActions;

    private bool gameStarted = false;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 0f;

    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * rotationSpeed);
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if (!gameStarted)
        {
            StartGame();
            Time.timeScale = 1f;
        }
        else
        {
            Jump();
        }
    }
    private void StartGame()
    {
        gameStarted = true;
        Jump();
    }
    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = Vector2.up * jumpForce;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.IncreaseScore();
    }
    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Jump.performed += OnJump; // Zýplama aksiyonunu dinliyor
    }
    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Jump.performed -= OnJump;
    }
}