using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    public float WalkSpeed = 5f;   // Adjust to desired speed
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
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;

    void Start()
    {
        currentHealth = maxHealth;
        // Initialize Rigidbody2D
        Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Jumping logic
        if (Input.GetKeyDown(jumpKey) && IsOnGround)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce); // Apply jump velocity
            IsOnGround = false;
        }

        // Horizontal movement logic (no forces, just setting velocity)
        float moveInput = 0f;

        if (Input.GetKey(moveLeftKey))
        {
            moveInput = -1f; // Move left
        }
        else if (Input.GetKey(moveRightKey))
        {
            moveInput = 1f; // Move right
        }

        // Set horizontal velocity directly (keeping the vertical velocity intact)
        Rb.velocity = new Vector2(moveInput * WalkSpeed, Rb.velocity.y);

        // Invincibility timer
        if (Invincible)
        {
            invinvibleTimer -= Time.deltaTime;
            if (invinvibleTimer < 0)
            {
                Invincible = false;
                invinvibleTimer = 2f;
            }
        }

        // Game over condition
        if (health < 1)
        {
            GameOver = true;
        }

        // Handle Game Over state
        if (GameOver)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(0); // Reload the scene to restart the game
        }
    }

    // Handle collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && !Invincible)
        {
            health -= 1f;
            Debug.Log("Health is " + health);
            Invincible = true;
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            GameOver = true;
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);


    }
}