using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    private Vector3 playerPosition;

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, smoothSpeed);
    }
}
