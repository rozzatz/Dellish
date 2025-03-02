using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float JumpForce = 10f;
    bool CanJump = true;
    public Rigidbody2D Rb;
    public float health = 3f;
    bool Invincible = false;
    float invinvibleTimer = 2f;
    public bool GameOver = false;
    public int maxHealth = 3;
    int currentHealth;
    public int coin;
    public int sceneLose = 1;
    public int sceneWin = 4;
    public int SceneCurrent;

    public float normalDrag = 0f;
    private bool WDown;
    public float IncreasedDrag = 5f;

    // Customizable key bindings
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode ResetKey = KeyCode.R;
    public KeyCode FloatKey = KeyCode.W;
    public KeyCode MenuKey = KeyCode.Escape;

    


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

        if (Input.GetKeyDown(jumpKey) && CanJump)
        {
            Rb.drag = normalDrag;
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce); // Apply jump velocity
            CanJump = false;
        }

        if (Input.GetKeyDown(ResetKey))
        {
            SceneManager.LoadScene(SceneCurrent);
        }

        if (Input.GetKeyDown(MenuKey))
        {
            SceneManager.LoadScene(0);
        }

         if (Input.GetKeyDown(FloatKey))
        {
            Rb.drag = IncreasedDrag;
            WDown = true;
        }
        else if  (Input.GetKeyUp(FloatKey) && WDown == true )
        {
            Rb.drag = normalDrag;
            WDown = false;
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

        if (currentHealth < 1)
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
        SceneManager.LoadScene(sceneLose); // Reload the scene to restart the game
    }

    void HandleGameOverW()
    {
        GameOver = true;
        Debug.Log("Game Win");
        SceneManager.LoadScene(sceneWin); // Reload the scene to restart the game
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpad"))
        {
            CanJump = true;
        }

        if (collision.gameObject.CompareTag("!Jumpad"))
        {
            CanJump = false;
        }

        if (collision.gameObject.CompareTag("Win"))
        {
            HandleGameOverW();
        }


        if (collision.gameObject.CompareTag("Enemy") && !Invincible)
        {
            ChangeHealth(-1);
            Invincible = true;
            invinvibleTimer = 2f; // Reset invincibility timer
            Debug.Log("health is" + currentHealth);
            colorchange();

        }

        if (collision.gameObject.CompareTag("Death") && !Invincible)
        {
            HandleGameOver();
        }

        if (collision.gameObject.CompareTag("coin"))
        {
            coin += 1;

            Debug.Log(coin);
            

        }

    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.CompareTag("Jumpad"))
        {
            CanJump = true;
        }

        if (collide.gameObject.CompareTag("Enemy") && !Invincible)
        {
            ChangeHealth(-1);
            Invincible = true;
            invinvibleTimer = 2f; // Reset invincibility timer
            Debug.Log("health is" + currentHealth);
            colorchange();

        }

        if (collide.gameObject.CompareTag("Death") && !Invincible)
        {
            HandleGameOver();
        }

        if (collide.gameObject.CompareTag("coin"))
        {
            coin += 1;

            Debug.Log(coin);


        }

    }

        public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    IEnumerator colorchange()
        {
    Color originalcolor = spriteRenderer.color;
    spriteRenderer.color = Color.blue;
    yield return new WaitForSeconds(.5f);
    spriteRenderer.color = originalcolor;
        }
}