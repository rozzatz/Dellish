using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;  // Reference to the player object
    public float moveSpeed = 5f;  // Speed at which the enemy moves toward the player
    public float detectionRange = 5f;  // Range within which the enemy detects the player
    private Vector3 startPosition;  // The original position of the enemy
    private bool playerIsMoving = false;  // Flag to check if player is moving
    private Rigidbody2D rb;  // Enemy's Rigidbody2D for movement


    // Start is called before the first frame update
    void Start()
    {
        //moveSpeed = moveSpeed * 10000;
        player = GameObject.FindGameObjectWithTag("Playa").transform;
        rb = GetComponent<Rigidbody2D>();  // Initialize the Rigidbody2D
        startPosition = transform.position;  // Save the enemy's starting position
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is moving
        playerIsMoving = Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > 0.1f || Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.y) > 0.1f;

        // If the player is moving, the enemy doesn't follow
        if (!playerIsMoving)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Check if the player is within detection range
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            // Return to the starting position if the player is out of range
            Vector2 direction = (startPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

   
    
    }