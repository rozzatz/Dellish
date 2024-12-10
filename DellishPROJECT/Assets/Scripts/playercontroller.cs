using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float JumpForce = 10f;
    bool IsOnGround = true;
    public Rigidbody2D Rb;
    public float health = 5;
    bool Invincible = false;
    float invinvibleTimer = 2f;
    public bool GameOver = false;
    public int maxHealth = 3;
    int currentHealth;

    // Customizable key bindings
    public KeyCode jumpKey = KeyCode.Space;

    // Reference to the sprite renderer to flip the sprite
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        // Initialize Rigidbody2D and SpriteRenderer
        Rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();

        if (transform.position.y < -30)
        {
            HandleGameOver();
        }

        if (Input.GetKeyDown(jumpKey) && IsOnGround)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce); // Apply jump velocity
            IsOnGround = false;
        }

        // Invincibility timer
        if (Invincible)
        {
            invinvibleTimer -= Time.deltaTime;
            if (invinvibleTimer <= 0)
            {
                Invincible = false;
                invinvibleTimer = 2f;
            }
        }

        if (health < 1)
        {
            HandleGameOver();
        }
    }

    void HandleMovement()
    {
        // Using Input.GetAxis for smoother movement
        float moveInput = Input.GetAxis("Horizontal");  // -1 for left, 1 for right

        // Set velocity based on input
        Rb.velocity = new Vector2(moveInput * WalkSpeed, Rb.velocity.y);

        // Flip sprite based on direction of movement
        if (moveInput > 0)
        {
            spriteRenderer.flipX = true;  // Facing right
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = false;   // Facing left
        }
    }

    void HandleGameOver()
    {
        GameOver = true;
        Debug.Log("Game Over");
        SceneManager.LoadScene(0); // Reload the scene to restart the game
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && !Invincible)
        {
            ChangeHealth(-1);
            Invincible = true;
            invinvibleTimer = 2f; // Reset invincibility timer
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
