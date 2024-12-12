using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public float spinSpeed = 100f;  // Speed of rotation in degrees per second
    public int yspin;
    public int xspin;
    public int zspin;

    void Update()
    {
        // Rotate the sprite around the Z-axis (2D rotation)
        transform.Rotate(spinSpeed * Time.deltaTime * xspin, spinSpeed * Time.deltaTime * yspin, spinSpeed * Time.deltaTime * zspin);
    }

 
}
