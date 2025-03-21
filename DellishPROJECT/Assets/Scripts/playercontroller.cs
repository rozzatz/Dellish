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
    public Animator Anim;
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
    private bool SDown;
    public float IncreasedDrag = 5f;

    public float normalGrav = 2f;
    public float IncreasedGrav = 5f;
    // Customizable key bindings
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode ResetKey = KeyCode.R;
    public KeyCode FloatKey = KeyCode.W;
    public KeyCode SlamKey = KeyCode.S;
    public KeyCode MenuKey = KeyCode.Escape;

    //anim bools remove public eventuslly



    


    // Reference to the sprite renderer to flip the sprite
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        // Initialize Rigidbody2D and SpriteRenderer
        Rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
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
           
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce); // Apply jump velocity
            CanJump = false;
            Anim.SetBool("IsJumping",true);
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
           
            Anim.SetBool("Isfloating", true);
        }
        else if  (Input.GetKeyUp(FloatKey) && WDown == true )
        {
            Rb.drag = normalDrag;
            WDown = false;
           
            Anim.SetBool("Isfloating", false);
        }

        if (Input.GetKeyDown(SlamKey))
        {
            Rb.gravityScale = IncreasedGrav;
            SDown = true;
            Anim.SetBool("IsDiving", true);
        }
        else if (Input.GetKeyUp(SlamKey) && SDown == true)
        {
            Rb.gravityScale = normalGrav;
            SDown = false;
            Anim.SetBool("IsDiving", false);
        }



        // Invincibility timer
        if (Invincible)
        {
            invinvibleTimer -= Time.deltaTime;
            if (invinvibleTimer <= 0)
            {
                Invincible = false;
                invinvibleTimer = .01f;
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
            spriteRenderer.flipX = false;  // Facing right

         
            Anim.SetBool("IsRunning", true);
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;   // Facing left
            Anim.SetBool("IsRunning", true);
        }
        else
        {
            Anim.SetBool("IsRunning", false);
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
            Anim.SetBool("CanJump", true);
            Anim.SetBool("IsJumping", false);
        }

        if (collision.gameObject.CompareTag("!Jumpad"))
        {
            CanJump = false;
            Anim.SetBool("CanJump", false);
            Anim.SetBool("IsJumping", false);
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
           StartCoroutine(Colorchange());

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
            Anim.SetBool("CanJump", true);

        }

        if (collide.gameObject.CompareTag("Enemy") && !Invincible)
        {
            ChangeHealth(-1);
            Invincible = true;
            invinvibleTimer = 2f; // Reset invincibility timer
            Debug.Log("health is" + currentHealth);
            Colorchange();

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

    IEnumerator Colorchange()
        {
    Color originalcolor = spriteRenderer.color;
    spriteRenderer.color = Color.red;
        GetComponent<Renderer>().material.color = Color.red;
        Anim.SetBool("IsHurt", true);
        yield return new WaitForSeconds(.2f);
    spriteRenderer.color = originalcolor;
        GetComponent<Renderer>().material.color = originalcolor;
        Anim.SetBool("IsHurt", false);
    }
}