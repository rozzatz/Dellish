using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    public float coin;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Playa"))
        {
            coin = 1 +coin;

            Debug.Log(coin);

            Destroy(gameObject);
        }



    }
}