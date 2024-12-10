using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public float spinSpeed = 100f;  // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the sprite around the Z-axis (2D rotation)
        transform.Rotate(0, spinSpeed * Time.deltaTime,0);
    }

 
}
