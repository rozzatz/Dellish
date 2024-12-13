using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speed = 2f; // Movement speed of the enemy
    public float changeDirectionInterval = 3f; // Time interval to change direction in seconds
    private bool movingRight = true; // Direction the enemy is currently moving
    private float timer = 0f; // Timer to track time passed

    private Vector3 startPosition;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Store the initial position of the enemy and get the SpriteRenderer component to flip the sprite
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Change direction after the specified interval
        if (timer >= changeDirectionInterval)
        {
            movingRight = !movingRight; // Switch direction
            timer = 0f; // Reset the timer
        }

        // Move the enemy in the current direction
        MoveEnemy();
    }

    void MoveEnemy()
    {
        if (movingRight)
        {
            // Move right
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true; // Ensure sprite faces right
            }
        }
        else
        {
            // Move left
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false; // Flip sprite to face left
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Playa"))
        {
           
            Destroy(gameObject);
        }



    }
}
