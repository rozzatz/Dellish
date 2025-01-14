using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    public int coin;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Playa"))
        {
            coin += 1;

            Debug.Log(coin);

            Destroy(gameObject);
        }



    }
}