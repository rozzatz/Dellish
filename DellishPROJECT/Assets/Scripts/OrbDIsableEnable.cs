using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDIsableEnable : MonoBehaviour
{

    public float timerDuration = 3f;

    private void Start()
    {
        timerDuration = timerDuration *100;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Playa")) // Check if the colliding object has the tag "Playa"
        {
            StartCoroutine(DeactivateAndReactivate());
        }
    }

    IEnumerator DeactivateAndReactivate()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;// Deactivate this object
        yield return new WaitForSeconds(timerDuration * Time.deltaTime); // Wait for the set duration
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}