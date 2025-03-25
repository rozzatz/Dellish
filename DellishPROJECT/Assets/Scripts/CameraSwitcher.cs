using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private CAMERAFOLLOW mainCam;
    public float xMoveAdd;
    public float yMoveAdd;
    public float xMoveOffset;
    public float yMoveOffset;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CAMERAFOLLOW>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Playa"))
        {
            mainCam.xMove += xMoveAdd;
            mainCam.yMove += yMoveAdd;
            mainCam.xOffset += xMoveOffset;
            mainCam.yOffset += yMoveOffset;
        }
    }
}
