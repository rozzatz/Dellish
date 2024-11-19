using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OswaldController : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float JumpForce = 10f;
    bool somersault = false;
    bool somersaultKick = false;
    bool IsOnGround = true;
    public Rigidbody2D Rb;
    public float health = 5;
    bool Invincible = false;
    float invinvibleTimer = 2f;
    public bool GameOver = false;


    // change all key codes into the thing that universal for unity  i foget what its called.
    void Start()
    {
        // lets me use my rigigf body in the script
        Rb = GetComponent<Rigidbody2D>();
        WalkSpeed = WalkSpeed * 1000;
    }

    // Update is called once per frame
    void Update()
    {
        //if you press spacebar you can jump
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround == true)
        {
            Rb.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
            IsOnGround = false;
        }
        // detecting wether the left key is pressed so i can add continuious movement
        if (Input.GetKey(KeyCode.A))
        {
            Rb.AddForce(Vector3.left * Time.deltaTime * WalkSpeed, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Rb.AddForce(Vector3.right * Time.deltaTime * WalkSpeed, ForceMode2D.Force);
        }

        if (Invincible == true)
        {
            invinvibleTimer -= Time.deltaTime;
            if (invinvibleTimer < 0)
            {
                Invincible = false;
                invinvibleTimer = 2;

            }
        }
        if (health < 1)
        {

            GameOver = true;

        }

        if (GameOver == true)
        {
            Debug.Log("game over");
            //Destroy(gameObject);
            //transform.position = new Vector3(-30, 1, 0);
            SceneManager.LoadScene(0);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && Invincible == false)
        {
            health -= 6f;
            Debug.Log("healt is " + health);
            Invincible = true;
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            GameOver = true;
        }
    }
}
