using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowXonly : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 1f;
    public Transform target;
    private playercontroller Script;
    void Update()
    {


        Vector3 newPos = new Vector3(target.position.x + xOffset,  yOffset,-10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

    }
}